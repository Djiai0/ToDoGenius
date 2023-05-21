using System.Collections.Generic;
using ToDoGenius.Models;

namespace ToDoGenius.Services
{
    public class TaskObjectPool
    {
        private readonly Queue<Task> taskPool;

        public TaskObjectPool()
        {
            taskPool = new Queue<Task>();
        }

        public Task GetTask()
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

        public void ReturnTask(Task task)
        {
            // Reset task properties if needed
            taskPool.Enqueue(task);
        }

        private static Task CreateNewTask()
        {
            // Create a new instance of Task
            return new Task();
        }
    }
}