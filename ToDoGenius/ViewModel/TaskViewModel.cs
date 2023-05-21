using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoGenius.Models;
using ToDoGenius.Services;
using ToDoGenius.State;
using ToDoGenius.View;
using Task = ToDoGenius.Models.Task;

namespace ToDoGenius.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private readonly TaskObjectPool taskObjectPool;
        private readonly ITaskState inProgressState;
        private readonly ITaskState completedState;
        private ObservableCollection<Task> tasks;
        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public TaskPrototype TaskPrototype { get; private set; }

        private Task selectedTask;
        public Task SelectedTask
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

        public ICommand StartTaskCommand { get; private set; }
        public ICommand CompleteTaskCommand { get; private set; }

        public ICommand AddTaskCommand { get; private set; }

        public TaskViewModel()
        {
            taskObjectPool = new TaskObjectPool();
            inProgressState = new InProgressState();
            completedState = new CompletedState();

            Tasks = new ObservableCollection<Task>();
            TaskPrototype = new TaskPrototype();
            AddTaskCommand = new RelayCommand(AddTask);
            StartTaskCommand = new RelayCommand(StartTask, CanStartTask);
            CompleteTaskCommand = new RelayCommand(CompleteTask, CanCompleteTask);

        }

        private bool CanStartTask(object parameter)
        {
            return SelectedTask != null && SelectedTask.Status != "En cours";
        }

        private void StartTask(object parameter)
        {
            if (SelectedTask != null)
            {
                Task task = taskObjectPool.GetTask();
                task.Title = SelectedTask.Title;
                task.Description = SelectedTask.Description;
                task.DueDate = SelectedTask.DueDate;

                // Update the task state to "In Progress"
                inProgressState.HandleTask(task);

                Tasks.Remove(SelectedTask);
                SelectedTask = null;
            }
        }

        private bool CanCompleteTask(object parameter)
        {
            return SelectedTask != null && SelectedTask.Status != "Terminé";
        }

        private void CompleteTask(object parameter)
        {
            if (SelectedTask != null)
            {
                // Update the task state to "Completed"
                completedState.HandleTask(SelectedTask);

                Tasks.Remove(SelectedTask);
                SelectedTask = null;
            }
        }
        private void AddTask(object parameter)
        {
            TaskPrototype taskPrototype = parameter as TaskPrototype;

            // Créez une nouvelle instance de la classe Task avec les valeurs saisies
            Task task = new Task
            {
                Title = taskPrototype.Title,
                Description = taskPrototype.Description,
                DueDate = taskPrototype.DueDate,
                Status = taskPrototype.Status,
            };

            // Ajoutez la tâche à la collection Tasks
            Tasks.Add(task);
        }
    }
}