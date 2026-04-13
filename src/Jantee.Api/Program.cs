using Jantee.BuildingBlocks.Core.Cqrs;
using Jantee.BuildingBlocks.Core.Cqrs.Implements;
using Jantee.BuildingBlocks.Web;
using Jantee.Modules.Users.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Users Module
builder.AddUsersModule();

// CQRS Implement
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapMinimalEndpoints();

app.MapGet("/", () =>
{
    return Results.Ok(new { message = "Hello, World!"});
})
.WithName("HelloWorld");

app.Run();
