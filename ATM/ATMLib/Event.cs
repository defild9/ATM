using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLib
{
    public class Event : EventArgs
    {
        public string Message;
        public Event(string message)
        {
            Message = message;
        }
    }
}
