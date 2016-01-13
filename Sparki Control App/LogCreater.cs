using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sparki_Control_App
{
    class LogCreater
    {
        

        public void logWriter(string fileName, string text, int number)
        {
            StreamWriter log = new StreamWriter(fileName, true);
            if (!File.Exists(fileName))
            {
                File.Create(fileName + ".log");
                log.Write(fileName + ".log\r\n");
                log.Write("--------------------");
            }
            log.Write(text);
            log.Write(number);
            log.Close();
        }
    }
}
