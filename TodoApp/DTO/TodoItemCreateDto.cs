using TodoApp.Models;

namespace TodoApp.DTO
{
    public class TodoItemCreateDto
    {

        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string Priority { get; set; }



    }
}
