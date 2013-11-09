using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class TaskCollection: LinkedList<Task>
    {
        Unit unit;

        public TaskCollection(Unit u)
        {
            this.unit = u;
        }

        public TaskCollection()
        {
        }


        public void Tick()
        {
            TaskCollection toRemove = new TaskCollection();
            foreach(Task t in this)
            {
                if (!t.Tick())
                    toRemove.AddLast(t);
            }
            foreach (Task t in toRemove)
            {
                 this.Remove(t);
            }
            toRemove.Clear();  
        }

        public void Add(Task t)
        { 
            this.AddLast(t); 
        }

        public void AddUnique(Task task)
        {
            this.RemoveByType(task.GetType());
            this.Add(task);
        }

        public void RemoveByType(System.Type TypeToRemove)
        {
            TaskCollection toRemove = new TaskCollection();
            foreach (Task t in this)
            {
                if (t.GetType() == TypeToRemove)
                    toRemove.AddLast(t);
            }
            foreach (Task t in toRemove)
            {
                this.Remove(t);
            }
            toRemove.Clear();
        }

    }
}
