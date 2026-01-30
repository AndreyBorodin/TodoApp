using Microsoft.AspNetCore.Http.HttpResults;
using TodoApp.DTO;
using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoItemService
    {
        Task<List<TodoItemDto>> GetAll(bool? isCompleted, string? priority);
        Task<TodoItemDto> CreateAsync(TodoItemCreateDto entity);
        Task<TodoItemDto> GetBy(int id);
        Task<TodoItemDto?> DeleteAsync(int id);
        Task<TodoItemDto?> PatchAsync(int id);
        Task<TodoItemDto> UpdateAsync(TodoItemDto entity);

    }
}
