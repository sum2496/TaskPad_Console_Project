using ConsoleTables;
using System.Text.RegularExpressions;

namespace TaskPadApp
{
    public class Program
    {
        public static bool IsAlphanumeric(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z0-9]*$");
        }

        public static bool ValidUserChoiceFromTable(string input)
        {
            return Regex.IsMatch(input, "[1-8]");
        }

        public static bool InvalidChoiceForContinue(string input)
        {
            return !Regex.IsMatch(input, "^[0-1]");
        }


        private static void MarkUserTaskComplete(string id)
        {
            //TodoManager.UpdateTaskStatusById(id);
            TodoUpdateAndDeleteTask.UpdateTaskStatusById(id);
        }
        private static void UserAddTask()
        {
            Random rand = new();
            string? id = "" + rand.Next(0, 1000);
            Console.Write("Task Id for new Task : " + id);
            Console.WriteLine();
            while (true)
            {
                Console.Write("Enter title for new task : ");
                string? title = Console.ReadLine();
                Console.Write("Enter description for new task : ");
                string? desc = Console.ReadLine();
/*  L1:           Console.Write("Set Priority Number for the task (To Set Priority Please Refer Priority Table) : ");
                string? prior = Console.ReadLine();
  L2:           Console.Write("Set Due Date for the task (DD-MM-YYYY) : ");
                string? dueDate = Console.ReadLine();

                if (string.IsNullOrEmpty(prior) || !int.TryParse(prior,out int res)) 
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter valid input for Priority Status!!! (Refer the Priority table for more info..)");
                    Console.WriteLine("--------------------------------------------------------------------");
                    Console.WriteLine();
                    goto L1;
                }

                if(string.IsNullOrEmpty(dueDate) || !DateTime.TryParse(dueDate,out DateTime dt))
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter valid input for Due Date!!! (Format: DD-MM-YYYY)");
                    Console.WriteLine("--------------------------------------------------------------------");
                    Console.WriteLine();
                    goto L2;
                }*/

                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(desc) || title.Length < 3 || desc.Length < 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Title as well as description must be atleast 3 characters!!!");
                    Console.WriteLine("--------------------------------------------------------------------");
                    Console.WriteLine();
                }
                else if (title.Length >= 3 && desc.Length >= 3)
                {
                    /*DateTime ddate = Convert.ToDateTime(dueDate);
                    if(ddate < DateTime.Now) 
                    {
                        Console.WriteLine();
                        Console.WriteLine("Due Date must be after Current Date!!!");
                        Console.WriteLine("--------------------------------------------------------------------");
                        Console.WriteLine();
                        goto L2;

                    }
                    int priorStatus = Convert.ToInt32(prior);*/
                    TaskItem newTask = new(id, title, desc);
                    TodoManager.AddTask(newTask);
                    break;
                }
            }
        }
        public static string GetUserChoice()
        {
            string choice = "" + Console.ReadLine();
            return choice;
        }
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the TaskPad App!!!");

            /*Console.WriteLine();
            Console.WriteLine("1 : Add a task");
            Console.WriteLine("2 : View all tasks (Currently Added)");
            Console.WriteLine("3 : View a specific task");
            Console.WriteLine("4 : Mark a task as completed");
            Console.WriteLine("5 : Update a task");
            Console.WriteLine("6 : Delete a task");
            Console.WriteLine("7 : Save Tasks to a file");
            Console.WriteLine("8 : Load tasks from a file (All Saved Tasks)");
            Console.WriteLine("Otherwise, Exit");
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------");*/

            TodoManager todoManager = new();
            while (true)
            {
                Console.WriteLine();
                var table = new ConsoleTable("Option ", "Description");
                table.Options.EnableCount = false;
                Console.ForegroundColor = ConsoleColor.Green;
                table.AddRow(1, "Add a task");
                table.AddRow(2, "View all tasks (Currently Added)");
                table.AddRow(3, "View a specific task");
                table.AddRow(4, "Mark a task as completed");
                table.AddRow(5, "Update a task");
                table.AddRow(6, "Delete a task");
                table.AddRow(7, "Save Tasks to a file");
                table.AddRow(8, "Load tasks from a file (All Saved Tasks)");
                table.Write();
                Console.WriteLine("Any values less than 1 and greater than 8 will close the TaskPad app!!");
                Console.WriteLine();

                /*var priorityTable = new ConsoleTable("Priority Number ","Priority Status");
                priorityTable.Options.EnableCount = false;
                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.WriteLine("To Assign Priority, Refer Below Table : ");
                priorityTable.AddRow(5,"Urgent");
                priorityTable.AddRow(4,"Important");
                priorityTable.AddRow(3,"Normal");
                priorityTable.AddRow(2,"Less Important");
                priorityTable.AddRow(1,"Least Important, Can be delayed");
                priorityTable.Write();

                Console.ForegroundColor = ConsoleColor.Green;*/

                Console.Write("Enter a choice : ");
                string input = GetUserChoice();
                if(string.IsNullOrEmpty(input) || !int.TryParse(input,out int result)) 
                {
                    Console.WriteLine("Please Enter valid input!!!");
                    continue;
                }
                int choice = Convert.ToInt32(input);
                if (!ValidUserChoiceFromTable(input))
                {
                    break;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("-----------------------------------------");
                        UserAddTask();
                        break;
                    case 2:
                        Console.WriteLine("-----------------------------------------");
                        //TodoManager.ViewCurrentTasks();
                        TodoViewTask.ViewCurrentTasks();
                        break;
                    case 3:
                        Console.WriteLine("-----------------------------------------");
                        Console.Write("Enter the task Id : ");
                        string? id = Console.ReadLine();

                        if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int res))
                            //TodoManager.ViewTaskById(id);
                            TodoViewTask.ViewTaskById(id);

                        else
                            Console.WriteLine("Please Enter valid Id!!!!");
                        break;
                    case 4:
                        Console.WriteLine("-----------------------------------------");
                        Console.Write("Enter the task Id to be mark as completed: ");
                        string? tId = Console.ReadLine();

                        if (!string.IsNullOrEmpty(tId) && int.TryParse(tId, out int res1))
                            MarkUserTaskComplete(tId);
                        else
                            Console.WriteLine("Please Enter valid Id!!!!");

                        break;
                    case 5:
                        Console.WriteLine("-----------------------------------------");
                        Console.Write("Enter the task Id for the task to be updated: ");
                        string? tIdUpdate = Console.ReadLine();

                        if (!string.IsNullOrEmpty(tIdUpdate) && int.TryParse(tIdUpdate, out int res2))
                            //TodoManager.UpdateTaskById(tIdUpdate);
                            TodoUpdateAndDeleteTask.UpdateTaskById(tIdUpdate);
                        else
                            Console.WriteLine("Please Enter valid Id!!!!");

                        break;
                    case 6:
                        Console.WriteLine("-----------------------------------------");
                        Console.Write("Enter the task Id of a task to be deleted : ");
                        string? tIdDelete = Console.ReadLine();

                        if (!string.IsNullOrEmpty(tIdDelete) && int.TryParse(tIdDelete, out int res3))
                            TodoUpdateAndDeleteTask.DeleteTaskById(tIdDelete);
                        else
                            Console.WriteLine("Please Enter valid Id!!!!");

                        break;
                    case 7:
                        Console.WriteLine("-----------------------------------------");
                        if(TodoManager.FetchList()?.Count > 0)
                        {
                            FileHandling.SaveTasksToFile(TodoManager.FetchList());
                            TodoManager.clearCurrentList();
                        }
                        else
                        {
                            Console.WriteLine("Your taskList is empty!!!");
                            Console.WriteLine("Please add some tasks!!!");
                        }
                        Console.WriteLine("-------------------------------------");
                        break;
                    case 8:
                        Console.WriteLine();
                        //TodoManager.ViewAllTask();
                        TodoViewTask.ViewAllTask();
                        Console.WriteLine("-----------------------------------------");
                        break;
                }

                Console.WriteLine();
                Console.Write("Do you want to continue? (1 : Yes, Other : No) : ");
                string input2 = GetUserChoice();

                if (string.IsNullOrEmpty(input2) || InvalidChoiceForContinue(input2) || !int.TryParse(input2, out int res4))
                {
                    Console.WriteLine("Please Enter valid input!!!");
                    continue;
                }
                else if (Convert.ToInt32(input2) == 0)
                {
                    break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Thank you for having a great experience with our TaskPad App!!");
            Console.WriteLine("----------------------------------------------------------------------------");
        }
    }
}
