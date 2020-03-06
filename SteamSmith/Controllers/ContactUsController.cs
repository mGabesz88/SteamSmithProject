using SteamSmith.Models;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Web.Mvc;

namespace SteamSmith.Controllers
{
    public class ContactUsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ContactUs model)
        {
            if (ModelState.IsValid)
            {
                var message = SendEmail(model);
                if (SendEmail(message))
                {
                    TempData["ContactSuccess"] = true;
                }
                else
                {
                    TempData["ContactSuccess"] = false;
                }
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        private MailMessage SendEmail(ContactUs model)
        {
            MailMessage message = new MailMessage(model.EmailAddress, ConfigurationManager.AppSettings["sendEmailTo"]);
            message.IsBodyHtml = true;
            message.Subject = $"Contact form Enquiry from {model.Name} - {model.EmailAddress}";
            message.Body = $"<b>Name: {model.Name}</b>";
            message.Body += "<br />";
            message.Body += $"<b>Email: {model.EmailAddress}</b>";
            message.Body += "<br />";
            //message.Body += string.Format("<b>" + "Phone number:" + " </b> " + "{0}", model.RequiredPhoneNumber);
            message.Body += "<br />";
            message.Body += $"<b>Message: {model.Message}</b>" + "{0}";
            return message;

        }

        private bool SendEmail(MailMessage message)
        {
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtpSetting"], 25);
            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in Send email: {0}", e.Message);
                return false;
            }
            message.Dispose();
            return true;
        }
    }
}