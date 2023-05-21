using ToDoGenius.Models;

namespace ToDoGenius.State
{
    public interface ITaskState
    {
        void HandleTask(Task task);
    }
}