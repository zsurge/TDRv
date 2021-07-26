using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv.Driver
{
    public class E5063A
    {
        private static int resourceMgr;
        public E5063A()
        {
            resourceMgr = 0;
        }

        public static bool isThreePort = false;

        public static int Open(string resourceName, ref int nHandle)
        {
            int viError;
            int attr;

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
            
            //StringBuilder attr = new StringBuilder();            
            viError = visa32.viSetAttribute(nHandle, visa32.VI_ATTR_TERMCHAR_EN, visa32.VI_TRUE);//终止符使能            
            viError = visa32.viSetAttribute(nHandle, visa32.VI_ATTR_TERMCHAR, 0x0A);//终止符设置0xA
            viError = visa32.viSetAttribute(nHandle, visa32.VI_ATTR_TMO_VALUE, 3000);//超时2000ms
            viError = visa32.viGetAttribute(nHandle, visa32.VI_ATTR_TERMCHAR_EN,out attr);



            return viError;
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

        public static bool GetThreePortIdentifier(int nInstrumentHandle)
        {
            int viError;
            int count = 0;
            string command = ":SYSTem:COMMunicate:SWITch1:DEFine?\n";
            string cmd1 = ":SYSTem:COMMunicate:SWITch1:LOCK 0";
            byte[] response = new byte[256];

            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            if (viError != visa32.VI_SUCCESS)
            {
                isThreePort = false;
                return false;
            }
            viError = visa32.viRead(nInstrumentHandle, response, 256, out count);
            if (viError != visa32.VI_SUCCESS)
            {
                isThreePort = false;
                return false;
            }

            string idn = Encoding.ASCII.GetString(response, 0, count);

            if (idn.Contains("U1810"))
            {
                viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(cmd1), cmd1.Length, out count);
                if (viError != visa32.VI_SUCCESS)
                {
                    isThreePort = false;
                    return false;
                }

                isThreePort = true;
                return true;
            }

            isThreePort = false;
            return false;
        }

        public static int ClearAllErrorQueue(int nInstrumentHandle)
        {
            int error = 0, count = 0;
            string command = "*CLS\n";
            error = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(command), command.Length, out count);
            return error;
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
        public static int QueryErrorStatus(int nInstrumentHandle, out string errorMesg)
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

        public static int getStartIndex(int nInstrumentHandle, out string msg1, out string msg2)
        {
            int viError, count;
            int attr;
            byte[] result = new byte[256];


            string strSwitchCmdDiff = ":SYSTem:COMMunicate:SWITch1:PATH1 1";
            string strSwitchCmdSingle = ":SYSTem:COMMunicate:SWITch1:PATH1 2";

            string str1 = ":SYST:COMM:SWIT1:DEFine?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str1 + "\n"), str1.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);            
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str2 = ":SYST:COMM:SWIT2:DEFine?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str2 + "\n"), str2.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str3 = ":CALCulate1:FSIMulator:BALun:PARameter1:STATe OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str3 + "\n"), str3.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str4 = ":CALCulate1:SELected:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str4 + "\n"), str4.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str5 = "CALCulate1:PARameter1:SELect";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str5 + "\n"), str5.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str6 = ":INITiate1:CONTinuous ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str6 + "\n"), str6.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str7 = ":TRIGger:SINGle";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str7 + "\n"), str7.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str8 = "*OPC?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str8 + "\n"), str8.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str9 = ":INITiate1:CONTinuous OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str9 + "\n"), str9.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);


            string str10 = ":DISPlay:UPDate";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str10 + "\n"), str10.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            if (isThreePort)
            {
                visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(strSwitchCmdSingle + "\n"), strSwitchCmdSingle.Length, out count);
            }


            string str11 = ":CALCulate1:SELected:DATA:FDATa?";
            byte[] ret = new byte[800000];

            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str11 + "\n"), str11.Length, out count);

            viError = visa32.viRead(nInstrumentHandle, ret, 800000, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            msg2 = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,",""); //单端           

            string str12 = ":CALCulate1:SELected:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str12 + "\n"), str12.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str13 = ":CALCulate1:SELected:TRANsform:TIME:STOP?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str13 + "\n"), str13.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str14 = ":CALCulate1:SELected:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str14 + "\n"), str14.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str15 = ":CALCulate1:FSIMulator:BALun:PARameter1:STATe ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str15 + "\n"), str15.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str16 = ":CALCulate1:SELected:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str16 + "\n"), str16.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str17 = "CALCulate1:PARameter1:SELect";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str17 + "\n"), str17.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str18 = ":INITiate1:CONTinuous ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str18 + "\n"), str18.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str19 = ":TRIGger:SINGle";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str19 + "\n"), str19.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str20 = "*OPC?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str20 + "\n"), str20.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str21 = ":INITiate1:CONTinuous OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str21 + "\n"), str21.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);


            string str22 = ":DISPlay:UPDate";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str22 + "\n"), str22.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            if (isThreePort)
            {
                visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(strSwitchCmdDiff + "\n"), strSwitchCmdDiff.Length, out count);
            }

            string str23 = ":CALCulate1:SELected:DATA:FDATa?";

            Array.Clear(ret, 0, ret.Length);

            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str23 + "\n"), str23.Length, out count);
            viError = visa32.viRead(nInstrumentHandle, ret, 800000, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            msg1 = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", ""); //差分

            string str24 = ":CALCulate1:SELected:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str24 + "\n"), str24.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str25 = ":CALCulate1:SELected:TRANsform:TIME:STOP?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str25 + "\n"), str25.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str26 = ":CALCulate1:SELected:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str26 + "\n"), str26.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);


            string str27 = ":DISPlay:ENABle ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str27 + "\n"), str27.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str28 = ":INITiate1:CONTinuous ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str28 + "\n"), str28.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str29 = ":TRIG:SOUR INTernal";
            //string str29 = ":TRIG:SOUR BUS";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str29 + "\n"), str29.Length, out count);
            

            return viError;
        }

        public static int measuration(int nInstrumentHandle, int channel, out string msg)
        {
            int viError, count;
            byte[] result = new byte[256];
            string cmd = string.Empty;
            int attr;


            string strSwitchCmdDiff = ":SYSTem:COMMunicate:SWITch1:PATH1 1";
            string strSwitchCmdSingle = ":SYSTem:COMMunicate:SWITch1:PATH1 2";

            string str29 = ":TRIG:SOUR BUS";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str29 + "\n"), str29.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str1 = ":SYST:COMM:SWIT1:DEFine?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str1 + "\n"), str1.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str2 = ":SYST:COMM:SWIT2:DEFine?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str2 + "\n"), str2.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            if (channel == 1)
            {
                if (isThreePort)
                {
                    visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(strSwitchCmdDiff + "\n"), strSwitchCmdDiff.Length, out count);
                }

                cmd = ":CALCulate1:FSIMulator:BALun:PARameter1:STATe ON";//差分
            }
            else
            {
                if (isThreePort)
                {
                    visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(strSwitchCmdSingle + "\n"), strSwitchCmdSingle.Length, out count);
                }

                cmd = ":CALCulate1:FSIMulator:BALun:PARameter1:STATe OFF";//单端
            }   
            
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(cmd + "\n"), cmd.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);


            string str4 = ":CALCulate1:SELected:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str4 + "\n"), str4.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str5 = "CALCulate1:PARameter1:SELect";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str5 + "\n"), str5.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str6 = ":INITiate1:CONTinuous ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str6 + "\n"), str6.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str7 = ":TRIGger:SINGle";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str7 + "\n"), str7.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str8 = "*OPC?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str8 + "\n"), str8.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str9 = ":INITiate1:CONTinuous OFF";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str9 + "\n"), str9.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);


            string str10 = ":DISPlay:UPDate";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str10 + "\n"), str10.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);     


            string str11 = ":CALCulate1:SELected:DATA:FDATa?";
            byte[] ret = new byte[800000];

            viError = visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str11 + "\n"), str11.Length, out count);

            viError = visa32.viRead(nInstrumentHandle, ret, 800000, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            msg = Encoding.ASCII.GetString(ret, 0, count).Replace("+0.00000000000E+000,", "");

            string str30 = ":TRIG:SOUR INTernal";            
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str30 + "\n"), str30.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str12 = ":CALCulate1:SELected:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str12 + "\n"), str12.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str13 = ":CALCulate1:SELected:TRANsform:TIME:STOP?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str13 + "\n"), str13.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str14 = ":CALCulate1:SELected:TRANsform:TIME:STARt?";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str14 + "\n"), str14.Length, out count);
            visa32.viRead(nInstrumentHandle, result, 256, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);


            string str15 = ":DISPlay:ENABle ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str15 + "\n"), str15.Length, out count);
            viError = visa32.viGetAttribute(nInstrumentHandle, visa32.VI_ATTR_TERMCHAR_EN, out attr);

            string str16 = ":INITiate1:CONTinuous ON";
            visa32.viWrite(nInstrumentHandle, Encoding.ASCII.GetBytes(str16 + "\n"), str16.Length, out count);

            return viError;
        }



        }//end class
}//end namespace
