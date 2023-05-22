using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToDoGenius.ViewModels;

namespace ToDoGenius
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Créer une instance de TaskViewModel
            TaskViewModel taskViewModel = new();

            // Charger les tâches depuis le fichier JSON
            taskViewModel.LoadTasksFromJson();
            MainWindow mainWindow = new MainWindow
            {
                DataContext = taskViewModel
            };
            mainWindow.Show();
        }
    }
}