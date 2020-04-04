using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DemoDI.Demos
{
    public class ObjectTrace
    {
        public ObjectTrace()
        {
            Groups = new ConcurrentDictionary<string, IDictionary<int, ObjectTraceItem>>(StringComparer.OrdinalIgnoreCase);
        }
        public IDictionary<string, IDictionary<int, ObjectTraceItem>> Groups { get; set; }

        public void TraceCreate(object instance)
        {
            lock (Lock)
            {
                var theType = instance.GetType().FullName;
                Groups.TryGetValue(theType, out var group);
                if (group == null)
                {
                    group = new ConcurrentDictionary<int, ObjectTraceItem>();
                    Groups.Add(theType, group);
                }

                var theKey = instance.GetHashCode();
                group.TryGetValue(theKey, out var item);
                if (item == null)
                {
                    item = new ObjectTraceItem();
                    item.HashCode = theKey;
                    item.Desc = "Create: " + group.Count;
                    group.Add(theKey, item);
                }

                LogHelper.Instance.Info("<<<<" + theKey);
            }
        }

        public void TraceDispose(object instance)
        {
            lock (Lock)
            {
                var theType = instance.GetType().FullName;
                Groups.TryGetValue(theType, out var group);
                if (group == null)
                {
                    return;
                }

                var theKey = instance.GetHashCode();
                group.TryGetValue(theKey, out var item);
                if (item == null)
                {
                    return;
                }

                item.DisposeAt = DateTime.Now;
                item.Desc += " => Disposed!";
                LogHelper.Instance.Info("    " + theKey + ">>>>");
            }
        }

        public static readonly object Lock = new object();
        public static ObjectTrace Instance = new ObjectTrace();
    }

    public class ObjectTraceItem
    {
        public ObjectTraceItem()
        {
            CreateAt = DateTime.Now;
        }

        public int HashCode { get; set; }
        public string Desc { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? DisposeAt { get; set; }
    }
    
    public static class ObjectTraceExtensions
    {
        public static void TraceCreate(this object instance)
        {
            ObjectTrace.Instance.TraceCreate(instance);
        }

        public static void TraceDispose(this object instance)
        {
            ObjectTrace.Instance.TraceDispose(instance);
        }
    }
}
