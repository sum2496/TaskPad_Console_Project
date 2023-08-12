using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPadApp
{
    internal class FileHandling
    {
        public static void SaveTasksToFile(List<TaskItem> task)
        {
            string filepath = "tasks.json";
            try
            {
                List<TaskItem> existingTasks = LoadTasksFromFile();

                existingTasks ??= new List<TaskItem>();
                existingTasks.AddRange(task);

                string jsonData = JsonConvert.SerializeObject(existingTasks, Formatting.Indented);
                File.WriteAllText(filepath, jsonData);

                Console.WriteLine("Data successfully stored in the database!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured : {ex.Message}");
            }
        }

        public static void UpdateTasksDetailsToFile(List<TaskItem> tasks)
        {
            string filepath = "tasks.json";
            string jsonData = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(filepath, jsonData);
        }

        public static List<TaskItem> LoadTasksFromFile()
        {
            string filepath = @"C:\Users\SumitShetty\source\repos\TaskPadApp\TaskPadApp\bin\Debug\net6.0\tasks.json";
            List<TaskItem> tasks = new();

            string jsonData = File.ReadAllText(filepath);

            // Deserialize the JSON data into a list of TaskItem objects
            if(jsonData != "") 
                tasks = JsonConvert.DeserializeObject<List<TaskItem>>(jsonData);

            return tasks;
        }


    }

}
