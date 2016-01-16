using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparki_Control_App
{
    class GeneralCommands
    {
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


    }
}
