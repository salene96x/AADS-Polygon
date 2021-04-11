using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    public class SendCommandWrapper<T>
    {
        public string Type
        {
            get => typeof(T).Name;
        }
        public T Command { get; set; }
    }
}
