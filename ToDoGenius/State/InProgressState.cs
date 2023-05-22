using System.Windows;
using ToDoGenius.Models;
using ToDoGenius.Services;

namespace ToDoGenius.State
{
    public class InProgressState : ITaskState
    {
        public void HandleTask(TodoTask task, TaskObjectPool taskObjectPool)
        {
            MessageBox.Show($"La tâche '{task.Title}' est maintenant en cours de traitement.");
            task.State = "En cours";
        }
    }
}