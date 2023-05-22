using System;
using ToDoGenius.State;

namespace ToDoGenius.Models
{
    public class TaskPrototype
    {
        public TaskPrototype()
        {
            State = "inprogress"; // Définir l'état initial comme "inprogress"
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string State { get; set; }

        public TodoTask Clone()
        {
            return new TodoTask
            {
                Title = this.Title,
                Description = this.Description,
                DueDate = this.DueDate,
                State = this.State
            };
        }
    } 
}