using System.Windows;
using ToDoGenius.Models;

namespace ToDoGenius.State
{
    public class CompletedState : ITaskState
    {
        public void HandleTask(Task task)
        {
            // Logique spécifique pour le traitement des tâches terminées
            task.Status = "Terminé";
            // Autres opérations à effectuer pour les tâches terminées
            MessageBox.Show($"La tâche '{task.Title}' est maintenant terminée.");
        }
    }
}