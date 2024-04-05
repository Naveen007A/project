using Microsoft.AspNetCore.Mvc;
using scopeindia.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
namespace scopeindia.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(string Name,string Email,string Subject,string Message)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("naveen481997@gmail.com");
            mail.To.Add(new MailAddress("sreedevi1862004@gmail.com"));
            mail.Subject = "student information";
            mail.IsBodyHtml = false;
            mail.Body=$"Name:{Name}\n Email Address:{Email}\n Subject:{Subject}\n Message:{Message}";
            using var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("naveen481997@gmail.com", "zwqc lvku nmxp mwut");
            smtp.EnableSsl = true;
            try
            {
             smtp.Send(mail);
                ViewBag.Success = "Mail sent Successfully";
            }catch(Exception)
            {
                ViewBag.failed = "unable to send message now please try again after some time";
            }
            ViewBag.Name = Name;
            ViewBag.Email= Email;
            ViewBag.Subject = Subject;
            ViewBag.Message = Message;
            var return_page = "Contact";
            if(ModelState.IsValid)
            {
                return_page = "Email";
            }
            return View(return_page);
        }
    }
}
