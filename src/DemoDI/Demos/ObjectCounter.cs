using System;
using System.Collections.Generic;

namespace DemoDI.Demos
{
    public class ObjectCounter
    {
        public static readonly object Lock = new object();
        
        public ObjectCounter()
        {
            Items = new Dictionary<Type, LogItem>();
        }

        public IDictionary<Type, LogItem> Items { get; set; }


        public void ReportCreate(object instance)
        {
            lock (Lock)
            {
                var theType = instance.GetType();
                Items.TryGetValue(theType, out var item);
                if (item == null)
                {
                    item = new LogItem();
                    item.Type = theType.Name;
                    item.CurrentCount = 1;
                    item.TotalCount = 1;
                    item.Message += "Create, ";
                }
                else
                {
                    item.CurrentCount = item.CurrentCount + 1;
                    item.TotalCount = item.TotalCount + 1;
                    item.Message += "Create, ";
                }

                Items[theType] = item;
            }
        }

        public void ReportDispose(object instance)
        {
            lock (Lock)
            {
                var theType = instance.GetType();
                Items.TryGetValue(theType, out var item);
                if (item != null)
                {
                    item.CurrentCount = item.CurrentCount - 1;
                    item.Message += "Dispose! ";
                }
            }
        }

        public static ObjectCounter Instance = new ObjectCounter();
    }

    public class LogItem
    {
        public string Type { get; set; }
        public int TotalCount { get; set; }
        public int CurrentCount { get; set; }
        public string Message { get; set; }
    }

    public static class ObjectCounterExtensions
    {
        public static void ReportCreate(this object instance)
        {
            ObjectCounter.Instance.ReportCreate(instance);
        }

        public static void ReportDispose(this object instance)
        {
            ObjectCounter.Instance.ReportDispose(instance);
        }
    }
}