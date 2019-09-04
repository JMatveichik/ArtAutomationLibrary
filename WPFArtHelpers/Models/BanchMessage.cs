using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtWPFHelpers.Models
{
    public enum BanchMessageType
    {
        Information = 0,
        Warning = 1,
        Alarm = 2,
        Started = 3,
        Successfully = 4
    }
    public class BanchMessage
    {
        public BanchMessage(string source, string message, BanchMessageType type)
        {
            Time = DateTime.Now;
            MessageType = type;
            Message = message;
            Source = source;
        }

        public string Source
        {
            get;
            private set;
        }
        public DateTime Time
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }

        public BanchMessageType MessageType
        {
            get;
            private set;
        }

    }
}
