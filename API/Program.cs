using API.Endpoints;
using API.Extensions;
using API.Hubs;
using Application.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Services.AddSwaggerGen().AddEndpointsApiExplorer();

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<OrdersHub>("/hubs/orders");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapRestaurantsEndpoints();
app.MapTagsEndpoints();
app.MapAuthEndpoints();
app.MapOrderEndpoints();

app.Run();
