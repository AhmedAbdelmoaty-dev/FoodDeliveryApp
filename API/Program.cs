using API.Endpoints;
using API.Extensions;
using API.Hubs;
using Application.Extensions;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Services.AddSwaggerGen().AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(builder.Configuration["AllowedOrigins"]?.Split(",") ?? ["http://localhost:4200"])
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database")
    .AddRedis(builder.Configuration["ConnectionStrings:Redis"] ?? "localhost:6379", name: "redis");

builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new Microsoft.AspNetCore.Http.Timeouts.RequestTimeoutPolicy
    {
        Timeout = TimeSpan.FromSeconds(30),
        TimeoutStatusCode = 503
    };
});

var app = builder.Build();

app.UseStaticFiles();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\":\"An unexpected error occurred\"}");
    });
});

app.UseRequestTimeouts();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowFrontend");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation("Applying database migrations...");
        db.Database.Migrate();
        logger.LogInformation("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying migrations");
    }
}

app.MapHub<OrdersHub>("/hubs/orders");

app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
    .WithTags("Health");

app.MapGet("/health/ready", async (AppDbContext db, CancellationToken ct) =>
{
    try
    {
        await db.Database.CanConnectAsync(ct);
        return Results.Ok(new { status = "ready", database = "connected" });
    }
    catch
    {
        return Results.Json(new { status = "unready", database = "disconnected" }, statusCode: 503);
    }
}).WithTags("Health");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapRestaurantsEndpoints();
app.MapTagsEndpoints();
app.MapAuthEndpoints();
app.MapOrderEndpoints();

app.Run();
