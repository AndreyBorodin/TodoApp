using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
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

app.UseHttpsRedirection();


app.MapPost("/api/todos", async (TodoItemCreateDto todoItemDto, ITodoItemService todoItemServic) => await todoItemServic.CreateAsync(todoItemDto));


app.MapGet("/api/todos", async ( ITodoItemService todoItemServic, bool? isCompleted = null, string? priority = null) => await todoItemServic.GetAll(isCompleted, priority));

app.MapGet("/api/todos/{id:int}", async (int id, ITodoItemService todoItemServic) => await todoItemServic.GetBy(id));

app.MapDelete("/api/todos/{id:int}", async (int id, ITodoItemService todoItemServic) => await todoItemServic.DeleteAsync(id));

app.MapPatch("/api/todos/{id:int}/complete", async (int id, ITodoItemService todoItemServic) => await todoItemServic.PatchAsync(id));

app.MapPut("/api/todos/{id:int}", async (int id, TodoItemDto todoItemDto, ITodoItemService todoItemServic) => await todoItemServic.UpdateAsync(todoItemDto));




app.Run();


