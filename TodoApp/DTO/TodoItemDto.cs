using System.ComponentModel.DataAnnotations;

namespace TodoApp.DTO
{
    public class TodoItemDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(200, ErrorMessage = "Название должно быть до 200 символов")]
        public required string Title { get; set; } = default!;

        [StringLength(1000, ErrorMessage = "Описание должно быть до 1000 символов")]
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }
    }
}
