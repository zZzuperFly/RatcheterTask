using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Ratcheter
{
    public class LogProxy:ILogProxy
    {
        private Task _parent;

        public LogProxy(Task parent)
        {
            _parent = parent;
        }


        public void LogThis(MessageImportance importance, string msg)
        {
            _parent.Log.LogMessage(importance, msg);
        }
    }

    public interface ILogProxy
    {
         void LogThis(MessageImportance importance, string msg);
    }
}
