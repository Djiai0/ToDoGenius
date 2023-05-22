using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ToDoGenius.Models;
using ToDoGenius.Services;
using ToDoGenius.State;
using ToDoGenius.View;
using System.Text.Json;
using TodoTask = ToDoGenius.Models.TodoTask;
using System;

namespace ToDoGenius.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private readonly ITaskState inProgressState;
        private readonly ITaskState completedState;
        private ObservableCollection<TodoTask> tasks;

        public ObservableCollection<TodoTask> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
        public TaskObjectPool TaskPool { get; } = new TaskObjectPool();

        public TaskPrototype TaskPrototype { get; private set; }

        private TodoTask selectedTask;
        public TodoTask SelectedTask
        {
            get { return selectedTask; }
            set
            {
                if (selectedTask != value)
                {
                    selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));
                }
            }
        }

        public ICommand AddTaskCommand { get; private set; }

        public TaskViewModel()
        {
            TaskPool = new TaskObjectPool();
            inProgressState = new InProgressState();
            completedState = new CompletedState();
            Tasks = new ObservableCollection<TodoTask>();
            AddTaskCommand = new RelayCommand(AddTask);
        }

        public void SaveTasksToJson()
        {
            List<TodoTask> uncompletedTasks = Tasks.Where(TodoTask => TodoTask.State == "En cours").ToList();

            string json = JsonSerializer.Serialize(uncompletedTasks);
            File.WriteAllText("tasks.json", json);
        }

        public void LoadTasksFromJson()
        {
            if (File.Exists("tasks.json"))
            {
                string json = File.ReadAllText("tasks.json");
                List<TodoTask> uncompletedTasks = JsonSerializer.Deserialize<List<TodoTask>>(json);

                if (uncompletedTasks != null)
                {
                    foreach (TodoTask TodoTask in uncompletedTasks)
                    {
                        Tasks.Add(TodoTask);
                        OnPropertyChanged(nameof(Tasks));
                    }
                }
            }
        }

        private void AddTask(object parameter)
        {
            if (parameter is TaskPrototype taskPrototype)
            {
                if (!string.IsNullOrEmpty(taskPrototype.Title))
                {
                    // créer une nouvelle instance de la classe InprogressState pour définir l'état de la tâche
                    InProgressState inProgressState = new();
                    // Créez une nouvelle instance de la classe TodoTask avec les valeurs saisies
                    // Obtenez une instance de TodoTask à partir du pool
                    TodoTask todoTask = TaskPool.GetTask();

                    todoTask.Title = taskPrototype.Title;
                    todoTask.Description = taskPrototype.Description;
                    todoTask.DueDate = taskPrototype.DueDate;
               
                    //définir l'état sur "en cours"
                    inProgressState.HandleTask(todoTask, TaskPool);

                    // Ajoutez la tâche à la collection Tasks
                    _ = taskPrototype.Clone();
                    Tasks.Add(todoTask);
                    OnPropertyChanged(nameof(Tasks));
                }
                else
                {
                    MessageBox.Show("Veuillez saisir le titre de la tâche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}