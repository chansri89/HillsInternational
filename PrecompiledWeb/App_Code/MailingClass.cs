using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using Ganini.Lib;
using GeneratingMail.Messages;
//using Microsoft.Office.Interop.Excel;
using System.Web;
using Microsoft.CSharp;


namespace GeneratingMail
{
    public class MailingClass
    {
        #region Common Declarations
       // ProcessBus Bus = new ProcessBus();
        string ErrorFlag = null;
        string FromMail = ConfigurationManager.AppSettings["FromMail"].ToString();
        string IP = ConfigurationManager.AppSettings["IP"].ToString();
        int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["_Port"].ToString());
        bool QuarterlyTrigger;
        bool HalfyearlyTrigger;
        int QuarterlyMonth = 0;
        int HalfyearlyMonth = 0;
        int RedAlertDay = Convert.ToInt32(Config.GetAppsetting("RedAlertDay"));
        string TestFileName = Config.GetAppsetting("TestFile");
        string GldFty = Config.GetAppsetting("GldFty");
        string EastFty = Config.GetAppsetting("EastFty");
        string WestFty = Config.GetAppsetting("WestFty");
        string GL = Config.GetAppsetting("GL");//For Green Leaf
        string LS = Config.GetAppsetting("LS");//For Green Leaf/Made Tea Leased For Total Purpose
        string MT = Config.GetAppsetting("MT");//For Made Tea
        string GRD = Config.GetAppsetting("GRD");//For Grade
        string DPS = Config.GetAppsetting("DPS");//For Despatches
        #endregion
        //-------------
        #region  NameChit Reports Methods        
        #endregion NameChit Reports
        public int SendMail(string FromMail, string ToMail, MailAddressCollection CC, string BodyMessage, string Subject, string IP, int Port)
        {
            if (Config.GetAppsetting("TestingFlag") == "Y")
            {
                File.AppendAllText(TestFileName, "send mail method starts" + System.DateTime.Now + "\r\n");
            }
            int Result = 0;
            try
            {
                MailMessage Mail = new MailMessage(FromMail, ToMail);
                //SmtpClient SmtpServer = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPSRV"].ToString());//Extra
                if (CC.ToString() != string.Empty)
                {
                    Mail.CC.Add(CC.ToString());
                }
                Mail.Subject = Subject;
                Mail.Body = BodyMessage;
                Mail.IsBodyHtml = true;

                //SmtpServer.Port = 587;
                //string username = System.Configuration.ConfigurationManager.AppSettings["MName"].ToString();
                //string password = System.Configuration.ConfigurationManager.AppSettings["MPw"].ToString();
                //SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
                //SmtpServer.EnableSsl = true;

                //SmtpServer.Send(Mail);
                //Mail.Dispose();

                SmtpClient Client = new SmtpClient(IP, Port);  //Comment for checking.. vinoth 130514
                Client.UseDefaultCredentials = true;
                Client.Send(Mail);
                Mail.Dispose();
                Result = 8;
            }
            catch (Exception ex)
            {
                ErrorException("Log for Mail Failure", ex.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            return Result;
        }
        public void ErrorException(string Message, string ex)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string dat = System.DateTime.Now.ToString("ddMMMyyyy-");
                string path = System.Configuration.ConfigurationManager.AppSettings["LogFile"].ToString();
                string FileName = path + dat + ".txt";
                if (File.Exists(FileName) == false)
                {
                    sb = new StringBuilder();
                    sb.Append(Message + " " + dat + ErrorFlag + "\r\n");
                    sb.Append(ex + "\r\n");
                    string AppDate = sb.ToString();
                    File.AppendAllText(FileName, AppDate);
                }
                else
                {
                    sb = new StringBuilder();
                    sb.Append(Message + " " + dat + ErrorFlag + "\r\n");
                    sb.Append(ex + "\r\n");
                    string AppDate = sb.ToString();
                    File.AppendAllText(FileName, AppDate);
                }
            }
            catch (Exception Ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
        }
        public void MonthGeneration()
        {
            int i = 0;
            //Quaterly Trigger
            for (i = 1; i <= 12; i = i + 3)
            {
                int Month = 0;
                Month = i;
                if (Month > 12)
                {
                    Month = 1;
                }
                if (System.DateTime.Now.Month == Month)
                {
                    QuarterlyTrigger = true;
                    QuarterlyMonth = Month;
                    break;
                }
            }
            i = 0;
            //HalfYear Trigger
            for (i = 3; i <= 12; i = i + 6)
            {
                if (System.DateTime.Now.Month == i)
                {
                    HalfyearlyMonth = i;
                    HalfyearlyTrigger = true;
                    break;
                }
            }
        }

    }

}