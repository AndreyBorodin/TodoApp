using TodoApp.DTO;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetF(bool? isCompleted, Priority? priority);
        Task<TodoItem> Create(TodoItem resource);

        Task<TodoItem?> GetBy(int id);

        Task<TodoItem?> DeleteAsync(TodoItem todoItem);

        Task<TodoItem?> UpdateAsync(TodoItem todoItem);
    }
}
