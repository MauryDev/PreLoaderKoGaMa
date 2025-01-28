using PreLoaderKoGaMa.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreLoaderKoGaMa.Services
{
    public class ConsoleTools
    {
        public class ClassPrint
        {
            public ClassPrint(string ClassName, ConsoleTools consoleTools) {
                this.ClassName = ClassName;
                ref_obj = consoleTools;
            }
            readonly string ClassName;
            readonly ConsoleTools ref_obj;
            public void Log(string message) => ref_obj.Log(ClassName, message);
            public void Warn(string message) => ref_obj.Warn(ClassName, message);
            public void Error(string message) => ref_obj.Error(ClassName, message);

        }
        public void Log(string title, string message) => ConsoleHelper.Log(title, message);
        public void Warn(string title, string message) => ConsoleHelper.Warn(title, message);
        public void Error(string title, string message) => ConsoleHelper.Error(title, message);

        public ClassPrint CreateClassLog<T>()
        {
            return new ClassPrint(typeof(T).Name,this );
        }
    }
}
