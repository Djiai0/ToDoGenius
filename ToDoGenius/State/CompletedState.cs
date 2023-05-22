using System.Windows;
using ToDoGenius.Models;
using ToDoGenius.Services;
using ToDoGenius.ViewModels;

namespace ToDoGenius.State
{
    public class CompletedState : ITaskState
    {
        public void HandleTask(TodoTask task, TaskObjectPool taskObjectPool)
        {
            TaskViewModel taskViewModel = new();
            taskViewModel.Tasks.Remove(task);
            taskObjectPool.ReturnTask(task);
            MessageBox.Show($"La tâche '{task.Title}' est maintenant terminée.");
            task.State = "completed";
        }
    }
}