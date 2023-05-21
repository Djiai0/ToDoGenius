using System.Windows;
using ToDoGenius.Models;

namespace ToDoGenius.State
{
    public class InProgressState : ITaskState
    {
        public void HandleTask(Task task)
        {
            // Logique spécifique pour le traitement des tâches en cours
            task.Status = "En cours";
            // Autres opérations à effectuer pour les tâches en cours
            MessageBox.Show($"La tâche '{task.Title}' est maintenant en cours de traitement.");
        }
    }
}