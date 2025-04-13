using dotnetCrud.Models;
using dotnetCrud.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Config binding
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/api/users", async (UserService service) =>
{
    return Results.Ok(await service.GetAllAsync());
});

app.MapGet("/api/users/{id}", async (string id, UserService service) =>
{
    var user = await service.GetByIdAsync(id);
    return user is null ? Results.NotFound() : Results.Ok(user);
});

app.MapPost("/api/users", async (User user, UserService service) =>
{
    await service.CreateAsync(user);
    return Results.Created($"/api/users/{user.Id}", user);
});

app.MapPut("/api/users/{id}", async (string id, User updatedUser, UserService service) =>
{
    var existing = await service.GetByIdAsync(id);
    if (existing is null) return Results.NotFound();

    updatedUser.Id = id;
    await service.UpdateAsync(id, updatedUser);
    return Results.Ok(new { message = "User has been updated successfully." });
});

app.MapDelete("/api/users/{id}", async (string id, UserService service) =>
{
    var user = await service.GetByIdAsync(id);
    if (user is null) return Results.NotFound();

    await service.DeleteAsync(id);
    return Results.Ok(new { message = "User has been Deleted successfully." });
});


app.Run();
