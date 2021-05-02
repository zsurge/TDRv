using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv.Driver
{
    public class E5080B
    {
        private static int resourceMgr;
        public E5080B()
        {
            resourceMgr = 0;
        }

        public static int Open(string resourceName, ref int nHandle)
        {
            int viError;

            viError = visa32.viOpenDefaultRM(out resourceMgr);
            if (viError != visa32.VI_SUCCESS)
            {
                return viError;
            }

            viError = visa32.viOpen(resourceMgr, resourceName, visa32.VI_NO_LOCK, visa32.VI_TMO_IMMEDIATE, out nHandle);
            if (viError != visa32.VI_SUCCESS)
            {
                return viError;
            }

            StringBuilder attr = new StringBuilder();
            viError = visa32.viGetAttribute(nHandle, visa32.VI_ATTR_RSRC_CLASS, attr);
            viError = visa32.viSetAttribute(nHandle, visa32.VI_ATTR_TERMCHAR_EN, visa32.VI_TRUE);
            viError = visa32.viSetAttribute(nHandle, visa32.VI_ATTR_TERMCHAR, 0x0A);
            viError = visa32.viSetAttribute(nHandle, visa32.VI_ATTR_TMO_VALUE, 2000);

            return viError;
        }
        public static int ExecuteCommand(int nInstrumentHandle,string command)
        {
            int errorno, count;
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
            string response;
            return QueryErrorStatus(nInstrumentHandle,out response);
        }

        public static int ExecuteCmd(int nInstrumentHandle, string command)
        {
            int count;
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
            return count;
        }

        public static int QueryCommand(int nInstrumentHandle, string command, out string response)
        {
            int errorno, count;
            byte[] result = new byte[256];
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
            errorno = visa32.viRead(nInstrumentHandle, result, 256, out count);
            response = Encoding.ASCII.GetString(result, 0, count);
            return errorno;
        }

        public static int QueryCommand(int nInstrumentHandle, string command, out string response, int len)
        {
            int count;
            byte[] result = new byte[len];

            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command + "\n"), command.Length, out count);
            visa32.viRead(nInstrumentHandle, result, len, out count);

            response = Encoding.ASCII.GetString(result, 0, count);

            return count;
        }


        /* *IDN? */
        public static int GetInstrumentIdentifier(int nInstrumentHandle, out string idn)
        {
            int viError;
            int count = 0;
            string command = "*IDN?\n";
            byte[] response = new byte[256];

            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            if (viError != visa32.VI_SUCCESS)
            {
                idn = string.Empty;
                return viError;
            }
            viError = visa32.viRead(nInstrumentHandle, response, 256, out count);
            if (viError != visa32.VI_SUCCESS)
            {
                idn = string.Empty;
                return viError;
            }

            idn = Encoding.ASCII.GetString(response, 0, count);
            return viError;
        }

        /* :SYST:PRES */
        public static int Preset(int nInstrumentHandle)
        {
            int errorno;
            string command = ":SYSTem:PRESet\n";
            byte[] response = new byte[128];
            int count = 0;

            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            if (errorno != visa32.VI_SUCCESS)
            {
                return errorno;
            }

            command = "*CLS\n";
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);

            command = "SYSTem:ERRor?\n";
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            errorno = visa32.viRead(nInstrumentHandle, response, 128, out count);
            if (errorno != visa32.VI_SUCCESS)
            {
                return errorno;
            }

            string[] array = new string[2];
            array = Encoding.ASCII.GetString(response, 0, count).Split(',');
            errorno = Convert.ToInt32(array[0]);
            return errorno;
        }

        /* *CLS */
        public static int ClearAllErrorQueue(int nInstrumentHandle)
        {
            int error = 0, count = 0;
            string command = "*CLS\n", response;
            error = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            return QueryErrorStatus(nInstrumentHandle, out response);
        }

        /* *CLS */
        public static int ClearInstrument(int nInstrumentHandle)
        {
            int errorno, count;
            string command = "*CLS\n",     /* Clean up all of the instrument, including the entire error queue */
                   response;
            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            return QueryErrorStatus(nInstrumentHandle,out response);      /* Make sure all errors had been cleaned up. */
        }

        public static void viClear(int nInstrumentHandle)
        {
            visa32.viClear(nInstrumentHandle);
        }

        public static void viClose(int nInstrumentHandle)
        {
            visa32.viClose(nInstrumentHandle);
        }

        /* SYST:ERR? */
        public static int QueryErrorStatus(int nInstrumentHandle,out string errorMesg)
        {
            int errorno, count;
            string command = "SYSTem:ERRor?\n";
            byte[] response = new byte[256];

            errorno = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            errorno = visa32.viRead(nInstrumentHandle, response, 256, out count);
            if (errorno != visa32.VI_SUCCESS)
            {
                errorMesg = "VISA library error : " + errorno;
                return errorno;
            }

            string[] messages = new string[2];
            messages = Encoding.ASCII.GetString(response, 0, count).Split(',');
            errorno = Convert.ToInt32(messages[0]);
            errorMesg = messages[1];
            return errorno;
        }

        public static int measuration(int nInstrumentHandle,int channel, out string msg)
        {
            int viError;
            int count;

            byte[] result = new byte[256];
            string response = string.Empty;
            string cmd1, cmd2, cmd3, cmd4, cmd5, cmd6;

            if (channel == 1)
            {    
                cmd1 = ":CALC:PAR:SEL \"win1_tr2\"";
            }
            else
            {
                cmd1 = ":CALC:PAR:SEL \"win1_tr1\"";
            }
            
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(cmd1 + "\n"), cmd1.Length, out count);


            cmd2 = ":CALCulate1:TRANsform:TIME:STARt?";
            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(cmd2 + "\n"), cmd2.Length, out count);
            viError = visa32.viRead(nInstrumentHandle, result, 256, out count);
            response = Encoding.ASCII.GetString(result, 0, count); 

            cmd3 = "DISPlay:ENABle ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(cmd3 + "\n"), cmd3.Length, out count);

            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(cmd1 + "\n"), cmd1.Length, out count);


            cmd5 = ":INITiate1:CONTinuous ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(cmd5 + "\n"), cmd5.Length, out count);

            visa32.viClear(nInstrumentHandle);


            byte[] ret = new byte[200000];
            cmd6 = ":CALCulate1:DATA? FDATa";
            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(cmd6 + "\n"), cmd6.Length, out count);
            viError = visa32.viRead(nInstrumentHandle, ret, 200000, out count);
            msg = Encoding.ASCII.GetString(ret, 0, count);

            return viError;
        }



        public static int getStartIndex(int nInstrumentHandle, int channel, out string msg)
        {
            int viError,count;
            byte[] result = new byte[256];

            string str1 = ":CALCulate1:TRANsform:TIME:STARt -5E-10";
            
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str1 + "\n"), str1.Length, out count);

            string str2 = ":CALCulate1:TRANsform:TIME:STOP 9.5E-9";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str2 + "\n"), str2.Length, out count);
            visa32.viClear(nInstrumentHandle);

            string str3 = "CALC1:X?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str3 + "\n"), str3.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            //msg = Encoding.ASCII.GetString(result, 0, count);

            string str4 = "CALC:PAR:CAT?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str4 + "\n"), str4.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);

            string str5 = string.Empty;
            if (channel == 1)
            {
                str5 = ":CALC:PAR:SEL \"win1_tr1\"";
            }
            else
            {
                str5 = ":CALC:PAR:SEL \"win1_tr2\"";
            }

            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str5 + "\n"), str5.Length, out count);

            string str6 = ":DISPlay:ENABle ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str6 + "\n"), str6.Length, out count);

            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str5 + "\n"), str5.Length, out count);

            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str6 + "\n"), str6.Length, out count);

            string str9 = ":INITiate1:CONTinuous ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str9 + "\n"), str9.Length, out count);
            visa32.viClear(nInstrumentHandle);

            string str10 = ":CALCulate1:DATA? FDATa";

            byte[] ret = new byte[200000];
            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str10 + "\n"), str10.Length, out count);
            viError = visa32.viRead(nInstrumentHandle, ret, 200000, out count);
            msg = Encoding.ASCII.GetString(ret, 0, count);

            return viError;
        }
    }//end class E5808B
}