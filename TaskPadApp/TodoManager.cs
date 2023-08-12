using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPadApp
{
    internal class TodoManager
    {
        protected static List<TaskItem>? taskList;
        protected static List<TaskItem>? currTaskList;

        //protected static string[] priorityList = {"Urgent","Important","Normal","Less Important","Least Important, Can be Delayed!!!"};

        public TodoManager()
        {
            currTaskList = new();
        }

        public static List<TaskItem>? FetchList()
        {
            return currTaskList;
        }

        public static void AddTask(TaskItem task)
        {
            currTaskList?.Add(task);
        }

        public static void clearCurrentList()
        {
            currTaskList?.Clear();
        }

    }

    class TodoViewTask : TodoManager
    {
        public static void ViewCurrentTasks()
        {
            if (currTaskList?.Count > 0)
            {
                foreach (var task in currTaskList)
                {
                    Console.WriteLine("Task Id : " + task.TaskId);
                    Console.WriteLine("Task Title : " + task.Title);
                    Console.WriteLine("Task Description : " + task.Description);
                    /*Console.WriteLine("Task Due Date : " + task.DueDate);
                    Console.WriteLine("Task Priority : " + priorityList[task.PriorityStatus-1]);*/
                    if (task.CompletionStatus == true)
                    {
                        Console.WriteLine("Task Status : Completed");
                    }
                    else
                        Console.WriteLine("Task Status : Pending");
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("Currently, no tasks available!!!");
        }

        public static void ViewAllTask()
        {
            taskList = FileHandling.LoadTasksFromFile();
            if (taskList.Count > 0)
            {
                foreach (var task in taskList)
                {
                    Console.WriteLine("Task Id : " + task.TaskId);
                    Console.WriteLine("Task Title : " + task.Title);
                    Console.WriteLine("Task Description : " + task.Description);
                    /*Console.WriteLine("Task Due Date : " + task.DueDate);
                    Console.WriteLine("Task Priority : " + priorityList[task.PriorityStatus - 1]);*/
                    if (task.CompletionStatus == true)
                    {
                        Console.WriteLine("Task Status : Completed");
                    }
                    else
                        Console.WriteLine("Task Status : Pending");
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("Currently, no tasks saved at database!!!");

            // Using LINQ Query
            /*IEnumerable<TaskItem> taskItemList = taskList.Select(task => task);

            foreach(var task in taskItemList)
            {
                Console.WriteLine("Task Id : " + task.TaskId);
                Console.WriteLine("Task Title : " + task.Title);
                Console.WriteLine("Task Description : " + task.Description);
                if (task.CompletionStatus == true)
                {
                    Console.WriteLine("Task Status : Completed");
                }
                else
                    Console.WriteLine("Task Status : Pending");
                Console.WriteLine("-----------------------------");
                Console.WriteLine();
            }*/

        }

        public static void ViewTaskById(string id)
        {
            /*var taskAvailable = taskList.Exists(task => task.TaskId == id);
            if (taskAvailable)
            {
                var taskItem = taskList.Find(task => task.TaskId == id);
                Console.WriteLine("Task Id : " + taskItem.TaskId);
                Console.WriteLine("Task Title : " + taskItem.Title);
                Console.WriteLine("Task Description : " + taskItem.Description);
                if (taskItem.CompletionStatus == true)
                {
                    Console.WriteLine("Task Status : Completed");
                }
                else
                    Console.WriteLine("Task Status : Pending");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Currently, no such tasks with given task id exists!!");
            }*/

            taskList = FileHandling.LoadTasksFromFile();
            var taskItemList = taskList.Where(task => task.TaskId == id)
                                       .Select(task => task).ToList();

            if (taskItemList.Count > 0)
            {
                foreach (var taskItem in taskItemList)
                {
                    Console.WriteLine("Task Id : " + taskItem.TaskId);
                    Console.WriteLine("Task Title : " + taskItem.Title);
                    Console.WriteLine("Task Description : " + taskItem.Description);
                    /*Console.WriteLine("Task Due Date : " + taskItem.DueDate);
                    Console.WriteLine("Task Priority : " + priorityList[taskItem.PriorityStatus - 1]);*/
                    if (taskItem.CompletionStatus == true)
                    {
                        Console.WriteLine("Task Status : Completed");
                    }
                    else
                        Console.WriteLine("Task Status : Pending");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Currently, no such tasks with given task id exists!!");
            }

        }
    }

    class TodoUpdateAndDeleteTask : TodoManager
    {
        public static void UpdateTaskById(string id)
        {
            /*var taskAvailable = taskList.Exists(task => task.TaskId == id);
            if (taskAvailable)
            {
                var taskIndex = taskList.FindIndex(task => task.TaskId == id);

                while (true)
                {
                    Console.Write("Do you want to update Task title? (1 : Yes, 0 : No) : ");
                    string answerTitle = Console.ReadLine();
                    if (string.IsNullOrEmpty(answerTitle))
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else
                    {
                        int toUpdate = Convert.ToInt32(answerTitle);
                        if(toUpdate == 1)
                        {
                            Console.Write("Enter New Title : ");
                            string newTitle = Console.ReadLine();
                            if (string.IsNullOrEmpty(newTitle))
                            {
                                Console.WriteLine("Please provide valid title!!!");
                                continue;
                            }
                            else
                                taskList[taskIndex].Title = newTitle;
                        }
                    }

                    Console.WriteLine();

                    Console.Write("Do you want to update Task Description? (1 : Yes, 0 : No) : ");
                    string answerDesc = Console.ReadLine();
                    if (string.IsNullOrEmpty(answerDesc) || Convert.ToInt32(answerDesc) > 1 || Convert.ToInt32(answerDesc) < 0)
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else
                    {
                        int toUpdate = Convert.ToInt32(answerDesc);
                        if (toUpdate == 1)
                        {
                            Console.Write("Enter New Description : ");
                            string newDesc = Console.ReadLine();
                            if (string.IsNullOrEmpty(newDesc))
                            {
                                Console.WriteLine("Please provide valid description!!!");
                                continue;
                            }
                            else
                                taskList[taskIndex].Description = newDesc;
                        }
                    }

                    Console.WriteLine();

                    Console.Write("Do you want to update Task Status? (1 : Yes, 0 : No) : ");
                    string answerStatus = Console.ReadLine();
                    if (string.IsNullOrEmpty(answerStatus) || Convert.ToInt32(answerStatus) > 1 || Convert.ToInt32(answerStatus) < 0)
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else
                    {
                        int toUpdate = Convert.ToInt32(answerStatus);
                        if (toUpdate == 1)
                        {
                            Console.Write("Enter New Status (1 : Completed, 0 : Pending) : ");
                            string newStatus = Console.ReadLine();
                            if (string.IsNullOrEmpty(newStatus))
                            {
                                Console.WriteLine("Please provide valid description!!!");
                                continue;
                            }
                            else
                            {
                                int updateStatus = Convert.ToInt32(newStatus);
                                if (updateStatus == 1)
                                    taskList[taskIndex].CompletionStatus = true;
                                else
                                    taskList[taskIndex].CompletionStatus = false;
                            }
                        }
                    }

                    Console.WriteLine();

                    Console.WriteLine("Do you want to change further? 1 : Yes, 0 : No");
                    string choice = Console.ReadLine();
                    if (string.IsNullOrEmpty(choice) || Convert.ToInt32(choice) > 1 || Convert.ToInt32(choice) < 0)
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else if(Convert.ToInt32(choice) == 0)
                    {
                        break;
                    }
                }

                Console.WriteLine("Successfully updated details of given task!!");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Currently, no such tasks with given task id exists!!");
            }*/

            taskList = FileHandling.LoadTasksFromFile();
            var taskIndex = taskList.Select((task, index) => new { Task = task, Index = index })
                                    .FirstOrDefault(item => item.Task.TaskId == id)?.Index ?? -1;

            if (taskIndex != -1)
            {
                while (true)
                {
                    Console.Write("Do you want to update Task title? (1 : Yes, 0 : No) : ");
                    string? answerTitle = Console.ReadLine();
                    if (string.IsNullOrEmpty(answerTitle) || !int.TryParse(answerTitle,out int res))
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else
                    {
                        int toUpdate = Convert.ToInt32(answerTitle);
                        if (toUpdate == 1)
                        {
                            Console.Write("Enter New Title : ");
                            string? newTitle = Console.ReadLine();
                            if (string.IsNullOrEmpty(newTitle))
                            {
                                Console.WriteLine("Please provide valid title!!!");
                                continue;
                            }
                            else
                                taskList[taskIndex].Title = newTitle;
                        }
                    }

                    Console.WriteLine();

                    Console.Write("Do you want to update Task Description? (1 : Yes, 0 : No) : ");
                    string? answerDesc = Console.ReadLine();
                    if (string.IsNullOrEmpty(answerDesc) || !int.TryParse(answerDesc, out int res1))
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else
                    {
                        int toUpdate = Convert.ToInt32(answerDesc);
                        if (toUpdate == 1)
                        {
                            Console.Write("Enter New Description : ");
                            string? newDesc = Console.ReadLine();
                            if (string.IsNullOrEmpty(newDesc))
                            {
                                Console.WriteLine("Please provide valid description!!!");
                                continue;
                            }
                            else
                                taskList[taskIndex].Description = newDesc;
                        }
                    }

                    /*Console.Write("Do you want to update Task Due Date? (1 : Yes, 0 : No) : ");
                    string? answerDate = Console.ReadLine();
                    if (string.IsNullOrEmpty(answerDate) || !int.TryParse(answerDate, out int dt))
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else
                    {
                        int toUpdate = Convert.ToInt32(answerDate);
                        if (toUpdate == 1)
                        {
                            Console.Write("Enter New Due Date : ");
                            string? newDate = Console.ReadLine();
                            if (string.IsNullOrEmpty(newDate) || !DateTime.TryParse(newDate, out DateTime dt1))
                            {
                                Console.WriteLine("Please provide valid Due Date Format!!!");
                                continue;
                            }
                            else
                                taskList[taskIndex].DueDate = Convert.ToDateTime(newDate);
                        }
                    }

                    Console.WriteLine();

                    Console.Write("Do you want to update Task Priority? (1 : Yes, 0 : No) : ");
                    string? ansPriority = Console.ReadLine();
                    if (string.IsNullOrEmpty(ansPriority) || !int.TryParse(ansPriority, out int prior))
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else
                    {
                        int toUpdate = Convert.ToInt32(ansPriority);
                        if (toUpdate == 1)
                        {
                            Console.Write("Set Updated Priority : ");
                            string? newPriority = Console.ReadLine();
                            if (string.IsNullOrEmpty(newPriority) || !int.TryParse(ansPriority, out int prior1))
                            {
                                Console.WriteLine("Please provide valid Priority!!!");
                                continue;
                            }
                            else if(Convert.ToInt32(newPriority) > 5 || Convert.ToInt32(newPriority) < 1)
                            {
                                Console.WriteLine("Please provide valid Priority!!!");
                                continue;
                            }
                            else
                                taskList[taskIndex].PriorityStatus = Convert.ToInt32(newPriority);
                        }
                    }*/

                    Console.WriteLine();

                    Console.Write("Do you want to update Task Status? (1 : Yes, 0 : No) : ");
                    string? answerStatus = Console.ReadLine();
                    if (string.IsNullOrEmpty(answerStatus) || !int.TryParse(answerStatus, out int res2))
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else
                    {
                        int toUpdate = Convert.ToInt32(answerStatus);
                        if (toUpdate == 1)
                        {
                            Console.Write("Enter New Status (1 : Completed, 0 : Pending) : ");
                            string? newStatus = Console.ReadLine();
                            if (string.IsNullOrEmpty(newStatus))
                            {
                                Console.WriteLine("Please provide valid description!!!");
                                continue;
                            }
                            else
                            {
                                int updateStatus = Convert.ToInt32(newStatus);
                                if (updateStatus == 1)
                                    taskList[taskIndex].CompletionStatus = true;
                                else
                                    taskList[taskIndex].CompletionStatus = false;
                            }
                        }
                    }

                    Console.WriteLine();

                    Console.Write("Do you want to change further? (1 : Yes, 0 : No) : ");
                    string? choice = Console.ReadLine();
                    if (string.IsNullOrEmpty(choice) || !int.TryParse(choice, out int res3))
                    {
                        Console.WriteLine("Please provide valid option!!!");
                        continue;
                    }
                    else if (Convert.ToInt32(choice) == 0)
                    {
                        FileHandling.UpdateTasksDetailsToFile(taskList);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please provide correct confirmation status!!!");
                        continue;
                    }
                }

                Console.WriteLine("Successfully updated details of given task!!");
            }
            else
            {
                Console.WriteLine("No such tasks with given id exists!!");
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();
        }

        public static void UpdateTaskStatusById(string id)
        {
            /*var taskAvailable = taskList.Exists(task => task.TaskId == id);
            if (taskAvailable)
            {
                var taskIndex = taskList.FindIndex(task => task.TaskId == id);
                taskList[taskIndex].CompletionStatus = true;
                Console.WriteLine("Successfully updated status of given task!!");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Currently, no such tasks with given task id exists!!");
            }*/

            List<TaskItem> existingTaskItems = FileHandling.LoadTasksFromFile();

            var taskAvailable = existingTaskItems.Exists(task => task.TaskId == id);
            if (taskAvailable)
            {
                var taskIndex = existingTaskItems
                    .Select((task, index) => new { Task = task, Index = index })
                    .FirstOrDefault(item => item.Task.TaskId == id)?
                    .Index ?? -1;

                existingTaskItems[taskIndex].CompletionStatus = true;
                FileHandling.UpdateTasksDetailsToFile(existingTaskItems);
            }
            else
            {
                Console.WriteLine("Currently, no such tasks with given task id exists!!");
            }
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();
        }

        public static void DeleteTaskById(string id)
        {
            /*var taskAvailable = taskList.Exists(task => task.TaskId == id);
            if (taskAvailable)
            {
                var taskIndex = taskList.FindIndex((task) => task.TaskId == id);
                taskList.RemoveAt(taskIndex);
                Console.WriteLine("Successfully deleted task!!!");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Currently, no such tasks with given task id exists!!");
            }*/

            List<TaskItem> existingTaskItems = FileHandling.LoadTasksFromFile();

            var taskAvailable = existingTaskItems.Exists(task => task.TaskId == id);
            if (taskAvailable)
            {
                var taskIndex = existingTaskItems
                    .Select((task, index) => new { Task = task, Index = index })
                    .FirstOrDefault(item => item.Task.TaskId == id)?
                    .Index ?? -1;

                existingTaskItems.RemoveAt(taskIndex);
                FileHandling.UpdateTasksDetailsToFile(existingTaskItems);
                Console.WriteLine("Successfully Deleted the task with Given Id!!!");
            }
            else
            {
                Console.WriteLine("Currently, no such tasks with given task id exists!!");
            }
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();
        }
    }
}
