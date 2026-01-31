using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using TodoApp.DTO;
using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoItemService
    {
        Task<List<TodoItemDto>> GetAll(bool? isCompleted, Priority? priority);
        Task<ErrorOr<TodoItemDto>> CreateAsync(TodoItemCreateDto entity);
        Task<ErrorOr<TodoItemDto>> GetBy(int id);
        Task<ErrorOr<TodoItemDto>> DeleteAsync(int id);
        Task<ErrorOr<TodoItemDto>> PatchAsync(int id);
        Task<ErrorOr<TodoItemDto>> UpdateAsync(TodoItemDto entity);

    }
}
