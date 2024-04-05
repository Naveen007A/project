using Microsoft.AspNetCore.Mvc;
using scopeindia.Models;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using scopeindia.Entity;
using Microsoft.EntityFrameworkCore;

namespace scopeindia.Controllers
{
    public class LogInController : Controller
    {
        private readonly EntDbContext db;

        public LogInController(EntDbContext db)
        {
            this.db = db;
        }
        public IActionResult In()
        {
            if (HttpContext.Request.Cookies["naveen"] != null)
            {
                return RedirectToAction("DashBoard");
            }
            return View("In");
        }
        [HttpPost]
        public IActionResult In(Login ft)

        {
            CookieOptions optn1 = new CookieOptions();
            optn1.Expires = DateTime.Now.AddMinutes(60);
            HttpContext.Response.Cookies.Append("mail", ft.Email, optn1);

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=scope;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Login WHERE Email=@email AND Password=@password", conn);
            cmd.Parameters.AddWithValue("@email", ft.Email);
            cmd.Parameters.AddWithValue("@password", ft.Password);
            TempData["navi"] = ft.Email;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var email = reader["Email"].ToString();
                var password = reader["Password"].ToString();
                if (email == ft.Email && password == ft.Password)
                {
                    if (ft.CheckBox == true)
                    {
                        CookieOptions optn = new CookieOptions();
                        optn.Expires = DateTime.Now.AddMinutes(10);
                        HttpContext.Response.Cookies.Append("naveen", ft.Email, optn);
                        return RedirectToAction("DashBoard");
                    }
                    return RedirectToAction("DashBoard");

                }
                else  
                {
                    ViewBag.message = "Email and password doesn't match";

                }
            }
            conn.Close();
            return Redirect("In");

        }
        public IActionResult Firsttimelogin()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Firsttimelogin(Firsttimelogin frt)
        {
            //storing email in database
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=scope;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            SqlConnection conn1 = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=scope;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            conn1.Open();
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Login (Email) VALUES (@email)", conn);
            cmd.Parameters.AddWithValue("@email", frt.Email);
            cmd.ExecuteNonQuery();

            //generating a random number

            Random random = new Random();
            var random_number = random.Next(100000, 999999);
            TempData["Email"] = frt.Email;
            var email2 = TempData["Email"];

            //sending otp to an user

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("naveen481997@gmail.com");
            mail.To.Add(new MailAddress(email2.ToString()));
            mail.Subject = "OTP";
            mail.IsBodyHtml = true;
            mail.Body = random_number + "is your one time password for creating the new password";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("naveen481997@gmail.com", "zwqc lvku nmxp mwut");
            smtp.EnableSsl = true;
            smtp.Send(mail);

            //to store the otp to database

            SqlCommand cmd2 = new SqlCommand("UPDATE Login SET Password=@password Where Email=@mail", conn1);
            cmd2.Parameters.AddWithValue("@mail", email2);
            cmd2.Parameters.AddWithValue("@password", random_number);
            cmd2.ExecuteNonQuery();
            conn.Close();
            conn1.Close();
            return Redirect("Otp");                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     

        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(Firsttimelogin fi)

        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=scope;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            SqlConnection conn2 = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=scope;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            conn.Open();
            conn2.Open();

            //to check  whether the email found or not

            TempData["Email"] = fi.Email;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Login WHERE Email=@email", conn);
            cmd.Parameters.AddWithValue("@email", fi.Email);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //if email found send an otp to that email

                Random random = new Random();
                var random_number = random.Next(100000, 999999);
                var email = TempData["Email"];

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("naveen481997@gmail.com");
                mail.To.Add(new MailAddress(email.ToString()));
                mail.Subject = "OTP";
                mail.IsBodyHtml = true;
                mail.Body = random_number + "is your onetime password for creating the new password";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("naveen481997@gmail.com", "zwqc lvku nmxp mwut");
                smtp.EnableSsl = true;
                smtp.Send(mail);


                //to update random number as password

                SqlCommand cmd2 = new SqlCommand("UPDATE Login SET Password=@Password WHERE Email=@email", conn2);
                cmd2.Parameters.AddWithValue("@email", email);
                cmd2.Parameters.AddWithValue("@password", random_number);
                cmd2.ExecuteNonQuery();
                return Redirect("Otp");



            }
            else
            {
                ViewBag.Message = "Account not found create new one";
            }
            conn.Close();
            conn2.Close();
            return View("Firsttimelogin");
        }


        public IActionResult Otp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Otp(Firsttimelogin firs)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=scope;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Password FROM Login WHERE Email=@email", conn);
            cmd.Parameters.AddWithValue("email", TempData["Email"]);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //if user created the password with matching password then move to the new password page
                var password = reader["Password"].ToString();
                if (firs.Otp == password)
                {
                    return RedirectToAction("Passwordcreation");
                }
                else
                {
                    ViewBag.error_msg = "Invalid otp";
                }
            }
            return Redirect("Otp");
        }
        public IActionResult PasswordCreation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PasswordCreation(Firsttimelogin first)
        {
            //to update the password

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=scope;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Login SET Password=@password WHERE Email=@email", conn);
            cmd.Parameters.AddWithValue("@email", TempData["Email"]);
            cmd.Parameters.AddWithValue("@password", first.Password);
            cmd.ExecuteNonQuery();
            conn.Close();
            return Redirect("In");
        }
        public IActionResult DashBoard()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            return View("In");
        }


        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(Firsttimelogin nn)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=scope;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Password FROM Login WHERE Email=@email", conn);
            var a = TempData["navi"];
            cmd.Parameters.AddWithValue("email", a);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var password = reader["Password"].ToString();
                if ( password== nn.Password)
                {
                    return RedirectToAction("NewPassword");
                }
                else
                {
                    ViewBag.Message = "Incorrect password";
                }
            }
            conn.Close();
            return View();
        }
         public IActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewPassword(Firsttimelogin nk)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=scope;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            conn.Open();
            SqlCommand cmd =  new SqlCommand("UPDATE Login SET Password=@password WHERE Email=@email ",conn);
            cmd.Parameters.AddWithValue("email", TempData["navi"]);
            cmd.Parameters.AddWithValue("password", nk.Password);
            cmd.ExecuteNonQuery();
            conn.Close();
            return View("In");
        }
        public IActionResult ProfileEdit()
        {
            var mail = HttpContext.Request.Cookies["mail"];
            var a = db.Entry.Where(a=>a.Email==mail).ToList();
            return View(a);
        }
        [HttpGet]
        public IActionResult Updateprofile(int Id)
        {
            var a = db.Entry.FirstOrDefault(n=>n.Id == Id);
            if(a !=null)
            {
                var update = new Profileupdate
                {
                    Id = a.Id,
                    First_name = a.First_name,
                    Last_name = a.Last_name,
                    Gender = a.Gender,
                    DOB = a.DOB,
                    Email = a.Email,
                    Phone = a.Phone,
                    Country = a.Country,
                    State = a.State,
                    City = a.City,
                };
                return View(update);

            }
            return RedirectToAction("Updateprofile");
        }

        [HttpPost]
        public IActionResult Updateprofile(Profileupdate std)
        {
            var student = db.Entry.Find(std.Id);
            if (student != null)
            {
                student.Id = std.Id;
                student.First_name = std.First_name;
                student.Last_name = std.Last_name;
                student.Gender = std.Gender;
                student.DOB = std.DOB;
                student.Email = std.Email;
                student.Phone = std.Phone;
                student.Country = std.Country;
                student.State = std.State;
                student.City = std.City;
                db.SaveChanges();
                return RedirectToAction("ProfileEdit");
            }
            return Redirect("ProfileEdit");
        }
    }
}