using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System;
using Serilog;

namespace PathoLab.Web.Controllers
{
    public class EmailController : Controller
    {
        public IConfiguration Configuration { get; }
        public EmailController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult SendEmail(string EmailId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string tomail, string ccmail, string bccmail, string sub, string body)
        {
            try
            {
                MailMessage ObjMailMessage = new MailMessage();
                SmtpClient ObjSmtpClient = new SmtpClient();


                MailAddress ObjMailAddress = new MailAddress(Configuration.GetValue<string>("Email:EmailId"));

                //For Attatchment
                //if (objPersistantObject.Attatchment != null && objPersistantObject.Attatchment != string.Empty && objPersistantObject.Attatchment != "")
                //{
                //    if (File.Exists(objPersistantObject.Attatchment))
                //    {
                //        ObjMailMessage.Attachments.Add(new Attachment(objPersistantObject.Attatchment));
                //    }
                //}
                //For CC
                //if (objPersistantObject.Cc != null && objPersistantObject.Cc != string.Empty && objPersistantObject.Cc != "")
                //{
                //    strCCArray = objPersistantObject.Cc.TrimEnd(';').Split(';');
                //    foreach (string CCitem in strCCArray)
                //    {
                //        ObjMailMessage.CC.Add(new MailAddress(CCitem));
                //    }
                //}

                ObjMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                ObjMailMessage.SubjectEncoding = System.Text.Encoding.Default;
                ObjMailMessage.IsBodyHtml = true;
                ObjMailMessage.Priority = MailPriority.High;
                ObjMailMessage.From = ObjMailAddress;
                if (tomail != null && tomail != string.Empty && tomail != "")
                {
                    ObjMailMessage.To.Add(tomail.Replace("\r\n", " "));
                }
                if (ccmail != null && ccmail != string.Empty && ccmail != "")
                {
                    ObjMailMessage.CC.Add(ccmail.Replace("\r\n", " "));
                }
                if (bccmail != null && bccmail != string.Empty && bccmail != "")
                {
                    ObjMailMessage.Bcc.Add(bccmail.Replace("\r\n", " "));
                }
                ObjMailMessage.Body = body.Replace("\r\n", " ");
                ObjMailMessage.Body = body;
                ObjMailMessage.Subject = sub.Replace("\r\n", " ");
                ObjSmtpClient.Host = Configuration.GetValue<string>("Email:Host");// "smtp.zoho.com";
                ObjSmtpClient.EnableSsl = true;
                ObjSmtpClient.UseDefaultCredentials = false;
                ObjSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                ObjSmtpClient.Port = Convert.ToInt32(Configuration.GetValue<string>("Email:Port"));// 465;//587;
                ObjSmtpClient.Credentials = new System.Net.NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password"));
                ObjSmtpClient.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                // ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                ObjSmtpClient.Send(ObjMailMessage);
                //ObjMailMessage = null;
                //ObjSmtpClient = null;
                //ObjMailAddress = null;
                return Ok("Mail Sent Successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + "\n" + ex.StackTrace);
                return Ok(ex.Message);
                //throw ex;
            }
        }
    }
}