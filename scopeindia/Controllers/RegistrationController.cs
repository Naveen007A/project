using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Microsoft.Data.SqlClient;
using scopeindia.Models;
using scopeindia.Entity;


namespace scopeindia.Controllers
{
    
    
    
    public class RegistrationController : Controller
    {
        private readonly EntDbContext dbContext;
        public RegistrationController(EntDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public List<string> Hobbies {  get; set; }
        string hobbie;

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(Registration st)
        {
            if (Hobbies != null && Hobbies.Count > 0)
            {
                foreach (var item in Hobbies)
                {
                    hobbie += item + ",";
                }
            }
            string file_name = st.myfile.FileName;
            file_name = Path.GetFileName(file_name);
            string upload_folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploads");
            if (!Directory.Exists(upload_folder))
            {
                Directory.CreateDirectory(upload_folder);
            }
            string upload_path = Path.Combine(upload_folder, file_name);
            if (System.IO.File.Exists(upload_path))
            {
                ViewBag.UploadStatus += file_name + "Already Exists";
                Random file_number = new Random();
                file_name = file_number.Next().ToString() + file_name;
                upload_path = Path.Combine(upload_folder, file_name);
            }
            else
            {
                ViewBag.UploadStatus += file_name + "uploaded successfully";
            }

            var upload_steam = new FileStream(upload_path, FileMode.Create);
            st.myfile.CopyToAsync(upload_steam);

            //store the datas to database

            //SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScopeIndia;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            //conn.Open();
            //SqlCommand cmd = new SqlCommand("INSERT INTO Entry(First_name,Last_name,Gender,DOB,Email,Phone,Country,State,City,Hobbies,Image) VALUES (@Fname,@Lname,@gender,@dob,@email,@phone,@country,@state,@city,@hobbie,@image)",conn);
            //cmd.Parameters.AddWithValue("@Fname", st.FirstName);
            //cmd.Parameters.AddWithValue("@Lname", st.LastName);
            //cmd.Parameters.AddWithValue("@gender", st.Gender);
            //cmd.Parameters.AddWithValue("@dob", st.Date);
            //cmd.Parameters.AddWithValue("@email", st.Email);
            //cmd.Parameters.AddWithValue("@phone", st.Phone);
            //cmd.Parameters.AddWithValue("@country", st.Country);
            //cmd.Parameters.AddWithValue("@state", st.State);
            //cmd.Parameters.AddWithValue("@city", st.City);
            //cmd.Parameters.AddWithValue("@hobbie", hobbie);
            //cmd.Parameters.AddWithValue("@image", file_name);
            //cmd.ExecuteNonQuery();
            //conn.Close();

            var student = new Reg1
            {
                First_name=st.FirstName,
                Last_name=st.LastName,
                Gender=st.Gender,
                DOB=st.Date,
                Email=st.Email,
                Phone=st.Phone,
                Country=st.Country,
                State=st.State,
                City=st.City,
                Hobbies=hobbie,
                Image="~/uploads/"+file_name
            };
            dbContext.Entry.Add(student);
            dbContext.SaveChanges();


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("naveen481997@gmail.com");
            mail.To.Add(new MailAddress(st.Email));
            mail.Subject = "conformation message";
            mail.IsBodyHtml = true;
            mail.Body = "you have successfully registered with ScopeIndia";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("naveen481997@gmail.com", "zwqc lvku nmxp mwut");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
                ViewBag.success = "Registered Successfully";
                return RedirectToAction("Message");
            }
            catch (Exception)
            {
                ViewBag.failed = "unable to register please try again after sometime";
            }
            return View();
            
        }

        public IActionResult Message()
        {
            return View();
        }
    }
}
