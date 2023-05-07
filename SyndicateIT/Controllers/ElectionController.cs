using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;

namespace SyndicateIT.Controllers
{
    public class ElectionController : Controller
    {
        SyndicatDataEntities db = new SyndicatDataEntities();
        Guid User_Id = Guid.Parse("FB587755-2CEC-46FC-BB94-29F29738289B"); /// get from session
        string key = "b14ca5898a4e4133bbce2ea2315a1916";
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewSession(string session)
        {
            var model = new T_Election()
            {
                Session = session,
                From_Date = DateTime.Now.AddDays(-2),
                To_Date = DateTime.Now.AddMonths(1),
            };
            var voters = db.T_Voter_Code.ToList();
            db.T_Voter_Code.RemoveRange(voters);
            var users = db.Users.Select(s => new
            {
                s.User_ID,
                s.Email,
                s.FirstName,
                s.LastName,
                s.PhoneNumber
            }).ToList();
            model.Nb_Voters = users.Count;
            db.T_Election.Add(model);
            db.SaveChanges();
            users.ForEach(f =>
            {
                string code = Guid.NewGuid().ToString().Split('-')[0];
                SendEmailOrSMS(f.Email, f.PhoneNumber, code, model);
                db.T_Voter_Code.Add(new T_Voter_Code()
                {
                    ////// session 
                    User_Id = f.User_ID,
                    Code = code,
                    Election_Id = model.Id
                });
            });
            db.SaveChanges();
            return Content("new sesssion created and msgs sended to users");
        }
        public ActionResult Statistics(string session)
        {
            var e = db.T_Election.FirstOrDefault(p => p.Session == session);
            var Candidates = db.T_Candidate.Select(s => new
            {
                s.Id,
                s.Full_Name,
                Votes = s.T_Election_Vote.Count(p => p.Election_Id==e.Id),
            });
            var Voters = new
            {
                Voted = e.T_Election_Vote.Count,
                Not_Voted = e.T_Voter_Code.Count
            };
            return Json(new { Candidates, Voters }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Vote()
        {
            ViewData["candidates"] = db.T_Candidate.Select(s => new SelectListItem() { Value = s.Id.ToString(), Text = s.Full_Name }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Vote(string code, string signature, long Candidate_Id)
        {
            var vc = db.T_Voter_Code.FirstOrDefault(p =>  p.User_Id == User_Id);
            string Vote_Key = EncryptString(key, User_Id.ToString() + ";" + code);
            if (vc == null)
            {
                return Content("you are voted");
            }
            if (vc.Code!=code)
            {
                ViewData["error"] = "code error";
                return Vote();
            }
            if (vc.T_Election.From_Date > DateTime.Now || vc.T_Election.To_Date < DateTime.Now)
            {
                return Content("out of time ");
            }
            db.T_Election_Vote.Add(new T_Election_Vote()
            {
                Candidate_Id = Candidate_Id,
                Election_Id = vc.Election_Id,
                Signature = EncryptString(key, signature),
                Vote_Key = Vote_Key
            });
            db.T_Voter_Code.Remove(vc);
            db.SaveChanges();
            return Vote();
        }
        void SendEmailOrSMS(string email, string phone, string code, T_Election election)
        {
            ///////// send email with code and start end date
        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}