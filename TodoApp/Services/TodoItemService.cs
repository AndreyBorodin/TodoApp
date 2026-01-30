using Mapster;
using TodoApp.DTO;
using TodoApp.Models;
using TodoApp.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoApp.Services
{
    public class TodoItemService : ITodoItemService
    {
        private ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task<List<TodoItemDto>> GetAll(bool? isCompleted, string? priority)
        {
            var todoItemList = await _todoItemRepository.GetF(isCompleted, priority);

            return todoItemList.Adapt<List<TodoItemDto>>();
        }
        public async Task<TodoItemDto> CreateAsync(TodoItemCreateDto entity)
        {
            var todoItem = await _todoItemRepository.Create(entity.Adapt<TodoItem>());

            return todoItem.Adapt<TodoItemDto>();
        }
        public async Task<TodoItemDto> GetBy(int id)
        {
            TodoItem? todoItem = await _todoItemRepository.GetBy(id);

            return todoItem.Adapt<TodoItemDto>();
        }
        public async Task<TodoItemDto?> DeleteAsync(int id)
        {
            TodoItem? todoItem = await _todoItemRepository.GetBy(id);


            await _todoItemRepository.DeleteAsync(todoItem);

            return todoItem.Adapt<TodoItemDto>();
        }
        public async Task<TodoItemDto?> PatchAsync(int id)
        {
            TodoItem? todoItem = await _todoItemRepository.GetBy(id);

            todoItem.IsCompleted = true;

            await _todoItemRepository.UpdateAsync(todoItem);

            return todoItem.Adapt<TodoItemDto>();
        }
        public async Task<TodoItemDto> UpdateAsync(TodoItemDto entity)
        {
            TodoItem? todoItem = await _todoItemRepository.UpdateAsync(entity.Adapt<TodoItem>());

            return todoItem.Adapt<TodoItemDto>();
        }


    }
}
