using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Models
{
    public class StatusMessage : IDisposable
    {
        public int statuscode { get; set; }
        public string message { get; set; }
        public void Dispose()
        {
           
        }
    }
}
