using System;
using System.Windows;
using ToDoGenius.Models;
using ToDoGenius.ViewModels;

namespace ToDoGenius.View
{
    public partial class AddTaskView : Window
    {
        private TaskViewModel taskViewModel;
        public AddTaskView(TaskViewModel taskViewModel)
        {
            InitializeComponent();
            DataContext = taskViewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Vérifiez si le titre de la tâche est saisi
            if (!string.IsNullOrEmpty(Titre.Text))
            {
                // Créez un nouvel objet TaskPrototype avec les valeurs saisies
                TaskPrototype taskPrototype = new()
                {
                    Title = Titre.Text,
                    Description = Description.Text,
                    DueDate = Date.SelectedDate ?? DateTime.MinValue
                };

                // Obtenez la référence au DataContext de la fenêtre (TaskViewModel)
                TaskViewModel taskViewModel = DataContext as TaskViewModel;

                // Appelez la fonction d'ajout de tâche du TaskViewModel
                taskViewModel?.AddTaskCommand.Execute(taskPrototype);

                // Fermez la fenêtre
                Close();
            }
            else
            {
                MessageBox.Show("Veuillez saisir le titre de la tâche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}