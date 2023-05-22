using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using ToDoGenius.Models;
using ToDoGenius.ViewModels;

namespace ToDoGenius.View
{
    public partial class AddTaskView : Window
    {
        private Button validate;
        private TaskViewModel taskViewModel;

        bool isTitreChanged = false;
        bool isDescriptionChanged = false;
        public AddTaskView(TaskViewModel taskViewModel)
        {
            InitializeComponent();
            validate = validateButton;
            DataContext = taskViewModel;
        }

        private void Titre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Titre.Text != "")
            {
                isTitreChanged = true;
            }
            else
            {
                isTitreChanged = false;
            }
            EnableSubmitBtn();

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
                    DueDate = Date.SelectedDate ?? DateTime.Today
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

        public void EnableSubmitBtn()
        {
            {
                if (isTitreChanged && isDescriptionChanged)
                {

                    validate.IsEnabled = true;
                }
                else
                {
                    validate.IsEnabled = false;
                }
            }

        }

        private void Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Description.Text != "")
            {
                isDescriptionChanged = true;
            }
            else
            {
                isDescriptionChanged = false;
            }
            EnableSubmitBtn();

        }
    }
}