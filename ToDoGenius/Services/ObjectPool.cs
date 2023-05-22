using System.Collections.Generic;
using ToDoGenius.Models;

namespace ToDoGenius.Services
{
    public class TaskObjectPool
    {
        private readonly Queue<TodoTask> taskPool;

        public TaskObjectPool()
        {
            taskPool = new Queue<TodoTask>();
        }

        public TodoTask GetTask()
        {
            if (taskPool.Count > 0)
            {
                return taskPool.Dequeue();
            }
            else
            {
                return CreateNewTask();
            }
        }

        public void ReturnTask(TodoTask TodoTask)
        {
            // Reset TodoTask properties if needed
            taskPool.Enqueue(TodoTask);
        }

        private static TodoTask CreateNewTask()
        {
            // Create a new instance of TodoTask
            return new TodoTask();
        }
    }
}