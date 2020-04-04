using System;
using System.Collections.Generic;
using System.Threading;

namespace DemoDI.Demos.ObjectTraces
{
    public class TraceDbContext : IDisposable
    {
        //public List<string> Foos { get; set; }

        public TraceDbContext()
        {
            this.TraceCreate();

            //Foos = new List<string>();
            //for (int i = 0; i < 1024000; i++)
            //{
            //    Foos.Add(Guid.NewGuid() + " : " + i.ToString());
            //}
        }

        public void Dispose()
        {
            this.TraceDispose();
        }
    }
}