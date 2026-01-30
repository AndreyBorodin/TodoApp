using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Drawing;
using TodoApp.DataAccess;
using TodoApp.DTO;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoDbContext _context;

        public TodoItemRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetF(bool? isCompleted, string? priority)
        {
            IQueryable<TodoItem> query = _context.Set<TodoItem>().AsQueryable();

            if (isCompleted is not null)
                query = query.Where(x => x.IsCompleted == isCompleted);

            if (priority is not null)
                query = query.Where(x => x.Priority == (Priority)Enum.Parse(typeof(Priority), priority));

            return await query.ToListAsync();
        }

        public async Task<TodoItem> Create(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }
        public async Task<TodoItem?> GetBy(int id)
        {

            var todoItem = await _context.TodoItems
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return todoItem;
        }
        public async Task<TodoItem?> DeleteAsync(TodoItem? todoItem)
        {

            _context.TodoItems.Remove(todoItem);

            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task<TodoItem?> UpdateAsync(TodoItem? todoItem)
        {

            _context.TodoItems.Update(todoItem);

            await _context.SaveChangesAsync();

            return todoItem;
        }



    }
}
