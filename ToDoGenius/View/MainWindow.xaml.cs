using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoGenius.Models;
using ToDoGenius.State;
using ToDoGenius.View;
using ToDoGenius.ViewModels;

namespace ToDoGenius
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Closing += MainWindow_Closing;


            // Définissez le DataContext de la fenêtre sur l'instance de TaskViewModel
            DataContext = new TaskViewModel();
        }
        private void Checkbox_Checked_1(object sender, RoutedEventArgs e)
        {
            if (DataContext is TaskViewModel taskViewModel && (sender as CheckBox).DataContext is TodoTask task)
            {
                CompletedState completedState = new();
                completedState.HandleTask(task, taskViewModel.TaskPool);
            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskView addTaskView = new((TaskViewModel)DataContext);
            addTaskView.Show();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TaskViewModel taskViewModel = DataContext as TaskViewModel;
            taskViewModel?.SaveTasksToJson();
        }

    }
}
