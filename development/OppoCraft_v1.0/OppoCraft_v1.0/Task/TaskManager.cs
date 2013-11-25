using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class TaskManager 
    {
        Unit unit;
        public TaskCollection tasks;
        public Dictionary<string, Object> sharedMsg;

        public TaskManager(Unit u)
        {
            this.unit = u;
            this.sharedMsg = new Dictionary<string, object>();
            this.tasks = new TaskCollection();
        }

        public void Tick()
        {
            LinkedListNode<Task> cursor=this.tasks.First;
            while (cursor != null)
            {
                if (!cursor.Value.Tick())
                    this.Remove(cursor.Value);
                cursor = cursor.Next;
            }
        }

        public void AddNonUnique(Task t)
        {
            t.unit = this.unit;
            this.tasks.AddLast(t);
            t.onStart();
        }

        public void Add(Task task)
        {
            this.RemoveByType(task.GetType());
            this.AddNonUnique(task);
        }

        public void RemoveByType(System.Type TypeToRemove)
        {
            TaskCollection toRemove = new TaskCollection();
            foreach (Task t in this.tasks)
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

        public void Remove(Task t)
        {
            t.onFinish();
            this.tasks.Remove(t);
        }

        public void addDriver()
        {
            switch (this.unit.type)
            { 
                case Unit.Type.Knight:
                    this.Add(new TaskKnightDriver());
                    break;
            }
        
        }

        public bool checkShared(string name)
        {
            return this.sharedMsg.ContainsKey(name);
        }

        public T removeShared<T>(string name)
        {
            if (!this.checkShared(name)) return default(T);
            Object rez = this.sharedMsg[name];
            this.sharedMsg.Remove(name);
            return (T)rez;
        }

        public T getShared<T>(string name)
        {
            if (!this.checkShared(name)) return default(T);
            return (T)this.sharedMsg[name];
        }

        public void setShared(string name, Object data)
        {
            this.sharedMsg.Add(name, data);
        }

    }
}
