using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TDRv
{
    public delegate string DelegateSend();

    class IDevToHost
    {
        //transactionid 为时间戳 
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 响应主机查询初始化数据打包 4.2
        /// </summary>
        /// <returns></returns>
        public string _DevInitResp()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "InitialDataReply"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("control_mode", "2")),
                    new XElement("operation_mode", "1"),
                    new XElement("recipe_path", @"D:\test"),
                    new XElement("recipe_name", "test001"),
                    new XElement("cam_path", @"http://wwww.baidu.com"),
                    new XElement("job_id", "test001"),
                    new XElement("total_panel_count", "1000"),
                    new XElement("process_panel_count", "888"),
                new XElement("return",
                    new XElement("returncode", ""),
                    new XElement("returnmessage", ""))));
            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }

        /// <summary>
        /// 响应主机查询设备校时打包 4.3
        /// </summary>
        /// <returns></returns>
        public string _SyncTimeResp()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "DateTimeSyncReply"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("return_code", "1")),
                new XElement("return",
                    new XElement("returncode", ""),
                    new XElement("returnmessage", ""))));

            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }

        /// <summary>
        /// 通知设备下发数据打包4.7
        /// </summary>
        /// <returns></returns>
        public string _JobDownResp()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "JobDataDownloadReply"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("return_code", "1")),
                new XElement("return",
                    new XElement("returncode", ""),
                    new XElement("returnmessage", ""))));
            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }

        /// <summary>
        /// 响应主机人员上机确认信息打包4.13
        /// </summary>
        /// <returns></returns>
        public string _LoginConfrim()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "OperatorLoginConfirmReply"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("return_code", "1")),
                new XElement("return",
                    new XElement("returncode", ""),
                    new XElement("returnmessage", ""))));
            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }
        /*-------------------------------------------------------------------------*/

        //查询主机是否存在
        public string _DevStatusResp()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "AreYouThereRequest"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("server_ip", "192.168.10.102")),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));
            xmlbuf =  xmldata.Declaration.ToString()+ xmldata.ToString();
            
            return xmlbuf;
        }

        //控制模式上送4.14
        public string _ControlModeResp()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "EquipmentControlMode"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("control_mode", "2")),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));
            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }

        //设备上报当前时间4.16
        public string _CurrentDateTime()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "EquipmentCurrentDateTime"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("date_time", DateTime.Now.ToString("yyyyMMddhhmmss"))),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));
            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }


        //人员上机报告 4.20
        public string _LoginReport()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "OperatorLoginLogoutReport"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("identify_type", "1"),
                    new XElement("operator_id", "10055")),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));
            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }

        //人员下机报告
        public string _LogoutReport()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "OperatorLoginLogoutReport"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("identify_type", "2"),
                    new XElement("operator_id", "10055")),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));
            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }

        //读板报告4.23
        public string _PanelReadReport()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "PanelReadReport"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("panel_id", "10055")),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));
            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }

        //机台配方参数调用报告4.26
        public string _EquipmentRecipeSetupReport()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "EquipmentRecipeSetupReport"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("process_id", "test001"),
                    new XElement("recipe_path", @"d:\test"),
                    new XElement("cam_path", @"http://www.baidu.com"),
                    new XElement("setup_result", "1")),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));

            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }

        //制程/量测数据报告4.34
        public string _TestReport()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "ProcessDataReport"),
                    new XElement("transactionid", GetTimeStamp())),
                new XElement("body",
                    new XElement("eqp_id", "20210126"),
                    new XElement("sub_eqp_id", "test001"),
                    new XElement("job_id", "20210126"),
                    new XElement("proc_data_list", "20210126"),
                    new XElement("proc_data", "20210126"),
                    new XElement("data_item", "20210126"),
                    new XElement("data_value", "1")),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));

            xmlbuf = xmldata.Declaration.ToString()+ xmldata.ToString();

            return xmlbuf;
        }
    }//end IDevToHost

    public interface ISendToHost
    {
        event DelegateSend eventSend;
        string packetXmlData();
    }

    //响应系统初始化
    public class DevInitResp : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }

    //响应系统下发对时
    public class SyncTimeResp : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }

    //响应系统任务下发
    public class JobDownResp : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }

    //响应人员上机确认
    public class LoginConfrim : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();

        }
    }

    /*-------------------------------------------------------------------------*/
    //主机健康情况询问处理
    public class DevStatusResp : ISendToHost
    {
        public event DelegateSend eventSend;
        public string packetXmlData()
        {
            return eventSend();
        }
    }

    //柜台控制模式服务端响应处理
    public class ControlModeResp : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }

    //柜台当前系统时间
    public class CurrentDateTime : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }


    //人员上机报告
    public class LoginReport : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }

    //人员下机报告
    public class LogoutReport : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }

    //读板报告
    public class PanelReadReport : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }

    //机台配方参数调用报告
    public class EquipmentRecipeSetupReport : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }

    //制程/量测数据报告
    public class TestReport : ISendToHost
    {
        public event DelegateSend eventSend;

        public string packetXmlData()
        {
            return eventSend();
        }
    }


}



/*
            string xmlbuf = string.Empty;
            //创建最上一层的节点。
            XmlDocument xmlDoc = new XmlDocument();

            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);//设置声明 
            xmlDoc.AppendChild(dec);

            //创建根节点
            XmlElement rootNode = xmlDoc.CreateElement("message");
            xmlDoc.AppendChild(rootNode);

            //创建header子节点
            XmlElement headerNode = xmlDoc.CreateElement("header");
            rootNode.AppendChild(headerNode);

            XmlElement messagename_ement = xmlDoc.CreateElement("messagename");
            messagename_ement.InnerText = "AreYouThereRequest";
            headerNode.AppendChild(messagename_ement);

            XmlElement transactionid_lement = xmlDoc.CreateElement("transactionid");
            transactionid_lement.InnerText = "1234567890";
            headerNode.AppendChild(transactionid_lement);


            //创建body子节点
            XmlElement bodyNode = xmlDoc.CreateElement("body");
            rootNode.AppendChild(bodyNode);

            XmlElement eqp_id_ement = xmlDoc.CreateElement("eqp_id");
            eqp_id_ement.InnerText = "20210126";
            bodyNode.AppendChild(eqp_id_ement);

            XmlElement server_ip_lement = xmlDoc.CreateElement("server_ip");
            server_ip_lement.InnerText = "192.168.10.102";
            bodyNode.AppendChild(server_ip_lement);

            //创建return子节点
            XmlElement returnNode = xmlDoc.CreateElement("return");
            rootNode.AppendChild(returnNode);

            XmlElement returncode_ement = xmlDoc.CreateElement("returncode");
            returncode_ement.InnerText = "2021";
            returnNode.AppendChild(returncode_ement);

            XmlElement returnmessage_lement = xmlDoc.CreateElement("returnmessage");
            returnmessage_lement.InnerText = "success";
            returnNode.AppendChild(returnmessage_lement);        

            xmlDoc.Save("test.xml");
 */