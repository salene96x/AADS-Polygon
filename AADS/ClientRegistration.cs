using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    public class ClientRegistration
    {
        public string Address { get; set; }
        public int CurrentPort { get; set; }
        public string RegistrationName { get; set; }
        public static ClientRegistration Registration { get; set; }
    }
}
