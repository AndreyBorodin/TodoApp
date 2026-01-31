using ErrorOr;
using Mapster;
using System.ComponentModel.DataAnnotations;
using TodoApp.DTO;
using TodoApp.Models;
using TodoApp.Repositories;


namespace TodoApp.Services
{
    public class TodoItemService : ITodoItemService
    {
        private ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task<List<TodoItemDto>> GetAll(bool? isCompleted, Priority? priority)
        {
            var todoItemList = await _todoItemRepository.GetF(isCompleted, priority);

            return todoItemList.Adapt<List<TodoItemDto>>();
        }
        public async Task<ErrorOr<TodoItemDto>> CreateAsync(TodoItemCreateDto entity)
        {
            Error? error = ValidateResource(entity.Title);
            if (error is not null)
                return ErrorOr<TodoItemDto>.From(new List<Error> { error.Value });

            var todoItem = await _todoItemRepository.Create(entity.Adapt<TodoItem>());

            return todoItem.Adapt<TodoItemDto>();
        }
        public async Task<ErrorOr<TodoItemDto>> GetBy(int id)
        {
            
            TodoItem? todoItem = await _todoItemRepository.GetBy(id);

            if (todoItem is null)
                return ErrorOr<TodoItemDto>.From(new List<Error> { Error.NotFound() });

            return todoItem.Adapt<TodoItemDto>();
        }
        public async Task<ErrorOr<TodoItemDto>> DeleteAsync(int id)
        {

            TodoItem? todoItem = await _todoItemRepository.GetBy(id);

            if (todoItem == null)
                return ErrorOr<TodoItemDto>.From(new List<Error> { Error.NotFound() });

            await _todoItemRepository.DeleteAsync(todoItem);

            return todoItem.Adapt<TodoItemDto>(); 
        }
        public async Task<ErrorOr<TodoItemDto>> PatchAsync(int id)
        {

            TodoItem? todoItem = await _todoItemRepository.GetBy(id); 
            if (todoItem == null)
                return ErrorOr<TodoItemDto>.From(new List<Error> { Error.NotFound() });

            todoItem.IsCompleted = true;

            todoItem.CompletedAt = DateTime.Now;

            await _todoItemRepository.UpdateAsync(todoItem);

            return todoItem.Adapt<TodoItemDto>();
        }
        public async Task<ErrorOr<TodoItemDto>> UpdateAsync(TodoItemDto entity)
        {

            Error? error = ValidateResource(entity.Title);
            if (error is not null)
                return ErrorOr<TodoItemDto>.From(new List<Error> { error.Value });

            TodoItem? todoItem = await _todoItemRepository.UpdateAsync(entity.Adapt<TodoItem>());

            if (todoItem == null)
                return ErrorOr<TodoItemDto>.From(new List<Error> { Error.NotFound() });

            return todoItem.Adapt<TodoItemDto>();
        }

        private Error? ValidateResource(string title)
        {

            if (string.IsNullOrWhiteSpace(title))
            {
                return Error.Validation("title", "Поле название должно быть заполнено");
            }

            return default;
        }


    }
}
