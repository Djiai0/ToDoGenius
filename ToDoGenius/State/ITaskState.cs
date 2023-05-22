using ToDoGenius.Models;
using ToDoGenius.Services;

namespace ToDoGenius.State
{
    public interface ITaskState
    {
        void HandleTask(TodoTask TodoTask, TaskObjectPool taskObjectPool);
    }
}