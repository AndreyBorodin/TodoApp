using ErrorOr;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using TodoApp.DataAccess;
using TodoApp.DTO;
using TodoApp.Models;
using TodoApp.Repositories;
using TodoApp.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TodoDbContext>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//app.UseHttpsRedirection();


app.MapPost("/api/todos", async (TodoItemCreateDto todoItemDto, ITodoItemService todoItemServic) =>
{
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(todoItemDto);

    // Проверка валидации модели  
    if (!Validator.TryValidateObject(todoItemDto, validationContext, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    ErrorOr<TodoItemDto> result = await todoItemServic.CreateAsync(todoItemDto);

    if (result.IsError && result.FirstError.Type == ErrorType.Validation)
        return Results.BadRequest(result.FirstError.Description);

    if (result.IsError && result.FirstError.Type == ErrorType.Conflict)
        return Results.Conflict(result.FirstError.Description);

    if (result.IsError)
        return Results.Problem(result.FirstError.Description);

    return Results.Ok(result.Value);
});


app.MapGet("/api/todos", async ( ITodoItemService todoItemServic, bool? isCompleted = null, Priority? priority = null) => await todoItemServic.GetAll(isCompleted, priority));

app.MapGet("/api/todos/{id:int}", async (int id, ITodoItemService todoItemServic) =>
{
    ErrorOr<TodoItemDto> result = await todoItemServic.GetBy(id);

    if (result.IsError && result.FirstError.Type == ErrorType.NotFound)
        return Results.NotFound();

    if (result.IsError)
        return Results.Problem(result.FirstError.Description);

    return Results.Ok(result.Value);
});

app.MapDelete("/api/todos/{id:int}", async (int id, ITodoItemService todoItemServic) =>
{ 
    
    ErrorOr<TodoItemDto> result = await todoItemServic.DeleteAsync(id);

    if (result.IsError && result.FirstError.Type == ErrorType.NotFound)
        return Results.NotFound();

    if (result.IsError && result.FirstError.Type == ErrorType.Conflict)
        return Results.Conflict(result.FirstError.Description);

    if (result.IsError)
        return Results.Problem(result.FirstError.Description);

    return Results.NoContent();
});

app.MapPatch("/api/todos/{id:int}/complete", async (int id, ITodoItemService todoItemServic) =>
{

    ErrorOr<TodoItemDto> result = await todoItemServic.PatchAsync(id);

    if (result.IsError && result.FirstError.Type == ErrorType.NotFound)
        return Results.NotFound();

    if (result.IsError)
        return Results.Problem(result.FirstError.Description);

    return Results.Ok(result.Value);
});


app.MapPut("/api/todos/{id:int}", async (int id, TodoItemDto todoItemDto, ITodoItemService todoItemService) =>
{
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(todoItemDto);

    // Проверка валидации модели  
    if (!Validator.TryValidateObject(todoItemDto, validationContext, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    // Обновление элемента  
    ErrorOr<TodoItemDto> result = await todoItemService.UpdateAsync(todoItemDto);
    return Results.Ok(result.Value);
});

app.Run();


