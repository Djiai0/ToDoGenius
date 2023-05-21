using System;

namespace ToDoGenius.Models
{
    public class TaskPrototype
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        public Task Clone()
        {
            return new Task
            {
                Title = this.Title,
                Description = this.Description,
                DueDate = this.DueDate,
                Status = this.Status
            };
        }
    }
}