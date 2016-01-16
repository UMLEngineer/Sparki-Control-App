using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparki_Control_App
{
    class GeneralCommands
    {
        public class LineResult
        {
            public int lineLeft;
            public int lineRight;
            public int lineCenter;
            public string errorLeft;
            public string errorRight;
            public string errorCenter;
        }

        LineResult lineResult = new LineResult();

        LogCreater logger = new LogCreater();
        Serial serialPortc = new Serial();

        public int checkSumF(char[] arr)
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
                sum += arr[i];
            return sum;
        }

        public void commandCreater(String commOut)
        {
            char[] commandArray = new char[9];
            commandArray = commOut.ToCharArray();
            int checkSumV = checkSumF(commandArray);
            commOut += checkSumV;
            commOut += 'z';
            serialPortc.writeSerial(commOut);
        }

        public int charToInt(String returned)
        {
            char[] returnedc = returned.ToCharArray();
            int length = returnedc.Length;

            int converted = 0;
            int j = 0;
            int i = 0;
            int x = 0;
            switch (length)
            {
                case 4:
                    j = 3;
                    x = 100;
                    break;
                case 3:
                    j = 2;
                    x = 10;
                    break;
                case 2:
                    j = 1;
                    x = 1;
                    break;
                default:
                    logger.logWriter("Diag.txt", "Invalid Char to Int power/r/n", 0);
                    break;
            }

            while (i < j)
            {
                converted += x * (returnedc[i] - '0');
                x /= 10;
                i++;
            }
            return converted;
        }
        public LineResult lineValues()
        {
            string returned;
            var result = new LineResult();

            commandCreater("000bll");  //ask Sparki for the left Line sensors data
            returned = serialPortc.readBluetooth();  //read the returned value
            if (returned == "TE")  //check for a transmission error
                result.errorLeft = "Transmission error";
            else  //if no error convert the character array into an int and report the value for diag
            {
                result.lineLeft = charToInt(returned);
                logger.logWriter("bwLineFollower", "\r\n\r\nNew Iteration \r\nLine Left char: " + returned + " int: ", result.lineLeft);
            }
            commandCreater("000blr");  //ask Sparki for the right Line sensors data
            returned = serialPortc.readBluetooth();
            if (returned == "TE")
                result.errorRight = "Transmission error";
            else
            {
                result.lineRight = charToInt(returned);
                logger.logWriter("bwLineFollower", "\r\nLine Right: " + returned + " int: ", result.lineRight);
            }
            commandCreater("000blc");  //ask Sparki for the Center Line sensors data
            returned = serialPortc.readBluetooth();
            if (returned == "TE")
                result.errorCenter = "Transmission error";
            else
            {
                result.lineCenter = charToInt(returned);
                logger.logWriter("bwLineFollower", "\r\nLine Center: " + returned + " int: ", result.lineCenter);
            }
            return result;
        }

    }
}
