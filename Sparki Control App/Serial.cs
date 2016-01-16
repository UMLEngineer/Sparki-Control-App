using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Sparki_Control_App
{
    public class Serial
    {
        static SerialPort _serialPort = new SerialPort();

        public String readBluetooth()  //read the data coming back from sparki as a line consider going to a character by character approach
        {
            try
            {
                return _serialPort.ReadLine();
            }
            catch (TimeoutException)
            {
                return "Timeout Exception";
            }
        }

        public void writeSerial(string text)
        {
            _serialPort.Write(text);
        }

        public String[] getAvailablePorts()  //poplate ports combo box
        {
            String[] ports = SerialPort.GetPortNames();
            return ports;
        }

        public void operations(int setting, string text)
        {
            switch (setting)
            {
                case 1:
                    _serialPort.PortName = text;
                    break;
                case 2:
                    _serialPort.BaudRate = Convert.ToInt32(text);
                    break;
                case 3:
                    _serialPort.Open();
                    break;
                case 4:
                    _serialPort.Close();
                    break;
            }
        }


        public void Main()
        {
        }
    }
}
