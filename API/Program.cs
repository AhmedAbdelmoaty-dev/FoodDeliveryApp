using API.Endpoints;
using Application.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices().AddInfrastructureServices(builder.Configuration);

builder.Services.AddSwaggerGen().AddEndpointsApiExplorer();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapRestaurantsEndpoints();
app.MapTagsEndpoints();

app.Run();
