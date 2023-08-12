using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPadApp
{
    internal class TaskItem
    {
        string id;
        string title;
        string desc;
        bool status;
        int priorityStatus;
        DateTime dueDate;


        public string? TaskId { get { return id; } set { id = value; } }
        public string? Title { get { return title; } set { title = value; } }
        public string? Description { get { return desc; } set { desc = value; } }
        public bool CompletionStatus { get { return status; } set { status = value; } }

        /*public DateTime DueDate { get { return dueDate; } set { dueDate = value; } }

        public int PriorityStatus { get { return priorityStatus; } set { priorityStatus = value; } }*/

        public TaskItem(string id,string title,string desc,bool status = false) 
        {
            TaskId = id;
            Title = title;
            Description = desc;
            CompletionStatus = status;
        }

    }
}
