using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Swagger Servisleri
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 1. Global Exception Handling Middleware
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Unhandled exception: {ex.Message}");
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        var error = new { error = "An unexpected error occurred." };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(error));
    }
});

// 2. Performance Logging Middleware
app.Use(async (context, next) =>
{
    var sw = Stopwatch.StartNew();
    try
    {
        await next.Invoke();
    }
    finally
    {
        sw.Stop();
        var durationMs = sw.Elapsed.TotalMilliseconds;
        Console.WriteLine($"{context.Request.Method} {context.Request.Path} executed in {durationMs} ms");
    }
});

// 3. Swagger Middleware (Sadece Geliştirme Ortamında)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// In-Memory Veritabanı
var users = new List<User>();

// 4. API Endpoints (IResult ile Type Inference güvenceye alındı)
app.MapGet("/users", IResult () => TypedResults.Ok(users));

app.MapGet("/users/{id}", IResult (Guid id) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    return user is null ? TypedResults.NotFound() : TypedResults.Ok(user);
});

app.MapPost("/users", IResult (User user) =>
{
    var validationError = ValidateUserInput(user);
    if (validationError is not null)
    {
        return TypedResults.BadRequest(validationError);
    }

    var newUser = new User
    {
        Id = Guid.NewGuid(),
        FullName = user.FullName,
        Email = user.Email
    };

    users.Add(newUser);
    return TypedResults.Created($"/users/{newUser.Id}", newUser);
});

app.MapPut("/users/{id}", IResult (Guid id, User updatedUser) =>
{
    var validationError = ValidateUserInput(updatedUser);
    if (validationError is not null)
    {
        return TypedResults.BadRequest(validationError);
    }

    var existingUser = users.FirstOrDefault(u => u.Id == id);
    if (existingUser is null)
    {
        return TypedResults.NotFound();
    }

    existingUser.FullName = updatedUser.FullName;
    existingUser.Email = updatedUser.Email;

    return TypedResults.Ok(existingUser);
});

app.MapDelete("/users/{id}", IResult (Guid id) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user is null)
    {
        return TypedResults.NotFound();
    }

    users.Remove(user);
    return TypedResults.NoContent();
});

app.Run();

// 5. Validasyon ve Modeller
static string? ValidateUserInput(User user)
{
    if (string.IsNullOrWhiteSpace(user.FullName))
    {
        return "FullName is required and must not be empty.";
    }

    if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains('@'))
    {
        return "Email is required and must contain an '@' symbol.";
    }

    return null;
}

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}