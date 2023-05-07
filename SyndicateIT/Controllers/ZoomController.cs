using Jose;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using SyndicateIT.DataLayer.DataContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SyndicateIT.Controllers
{
    public class ZoomController : Controller
    {
        SyndicatDataEntities db = new SyndicatDataEntities();
        static readonly char[] padding = { '=' };
        Guid uid;
        Guid User_Id {
            get {
                if (HttpContext.Session["user"] == null) return Guid.Parse("FB587755-2CEC-46FC-BB94-29F29738289B");
                else return Guid.Parse(HttpContext.Session["user"].ToString());
            }
            set
            {
                uid = value;
            }
        }
        
        const string apiSecret = "4J3Y2WHZZYf5wEH7rHPOlri17wgGyN8eHBgq";
        const string key_secret = "LUUzKQ9Wlmym9aP6sPU7TxYu7Sy2pWTy";
        const string key_client = "MaK4eJMzSvm9ipMKANH6Zg";
        string tokenString;

        private static long ToEpoch(DateTime value) => (value.Ticks - 621355968000000000) / (10000 * 1000);
        public static string GetSignature(long meetingNumber, int role = 0)
        {
            var now = DateTime.UtcNow;
            var iat = ToEpoch(now);
            var exp = ToEpoch(now.AddDays(1));
            var payload = new Dictionary<string, object>()
          {
              { "appKey", key_client },
              { "sdkKey", key_client },
              { "mn", meetingNumber },
              { "role", role },
              { "iat", iat },
              { "exp", exp },
              { "tokenExp", exp },
          };
            return Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(key_secret), JwsAlgorithm.HS256);
        }
        string zakToken()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient("https://api.zoom.us/v2/users/me/token?type=zak");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));

            RestResponse restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode;
            int numericStatusCode = (int)statusCode;
            var jObject = JObject.Parse(restResponse.Content);
            return (string)jObject["token"];
        }
        public ZoomController()
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);
            var nbm = 5;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "BKDim-UoR-qJ4hIO_Qbuag",
                Expires = now.AddDays(nbm),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenString = tokenHandler.WriteToken(token);
            //ViewData["token"] = tokenString;
            ViewData["key"] = key_client;
        }
        public ActionResult Index()
        {
            var model = db.T_Meeting.Where(p => p.Created_by == User_Id).OrderByDescending(o => o.created_at).ToList();
            return View(model);
        }
        public ActionResult Delete_Meeting(long id)
        {
            var model = db.T_Meeting.Find(id);
            db.T_Meeting.Remove(model);
            db.SaveChanges();
            apiCall<object>("meetings/" + id, Method.Delete, null);
            return RedirectToAction("index");
        }
        public ActionResult Save_Meeting()
        {
            var model = new T_Meeting()
            {
                T_Meeting_User = new List<T_Meeting_User>()
            };
            ViewData["users"] = db.Users.Select(s => new SelectListItem() { Value = s.User_ID.ToString(), Text = s.FirstName + " " + s.LastName }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Save_Meeting(string topic, List<Guid> Users)
        {
            var res = apiCall("users/me/meetings",Method.Post,new
            {
                topic = topic,
                duration = "30",
                start_time = DateTime.UtcNow,
                type = 1,
                settings = new
                {
                    auto_recording = "cloud",
                    host_video = true,
                    private_meeting = false,
                    approval_type=2
                },
                recording = new
                {
                    cloud_recording = true,
                    record_audio_file = true,
                }
            });
            var jObject = JObject.Parse(res);
            var model = new T_Meeting()
            {
                created_at = (DateTime)jObject["created_at"],
                Id = (long)jObject["id"],
                topic = (string)jObject["topic"],
                start_url = (string)jObject["start_url"],
                password = (string)jObject["password"],
                join_url = (string)jObject["join_url"],
                Created_by = User_Id,
                Is_End = false
            };
            db.T_Meeting.Add(model);
            db.SaveChanges();
            if (Users != null)
            {
                Users.ForEach(f =>
                {
                    db.T_Meeting_User.Add(new T_Meeting_User()
                    {
                        Meeting_Id = model.Id,
                        User_Id = f,
                        Is_Joined = false
                    });
                });
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Meet(long id)
        {
            var model = db.T_Meeting.Find(id);
            if (model.T_Meeting_User.Count(p => p.User_Id == User_Id) == 0 && model.Created_by != User_Id)
            {
                return Content("no permission");
            }
            bool isCreator = model.Created_by == User_Id;
            ViewData["signature"] = GetSignature(id, isCreator ? 1 : 0);
            if (isCreator) ViewData["zak"] = zakToken();
            var usr = db.AspNetUsers.FirstOrDefault(p => p.UserId == User_Id);
            ViewData["name"] = usr.FirstName + " " + usr.LastName;
            ViewData["user_id"] = User_Id;
            return View(model);
        }
        public ActionResult Change_User(Guid id)
        {
            HttpContext.Session["user"] = id;
            return Content("done");
        }
        string apiCall<T>(string link, Method method,T data) where T : class
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient("https://api.zoom.us/v2/"+link);
            var request = new RestRequest();
            request.Method = method;
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
            if (data != null) request.AddJsonBody<T>(data);
            RestResponse restResponse = client.Execute(request);

            //HttpStatusCode statusCode = restResponse.StatusCode;
            //int numericStatusCode = (int)statusCode;
            //var Code = Convert.ToString(numericStatusCode);

            return restResponse.Content;
        }


        /// <summary>
        /// // users connected to meeting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Participations(long id)
        {
            var res = apiCall<object>("past_meetings/" + id + "/participants", Method.Get, null);
            return Content(res);
        }
        /// <summary>
        /// // records of this meeting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult recordings(long id)
        {
            var res = apiCall<object>("meetings/" + id + "/recordings", Method.Get, null);
            ////// save records in server then delete
            ///Delete_MetingRecords(id);
            return Content(res);
        }
        void Delete_MetingRecords(long id)
        {
            //meetings/{meetingId}/recordings
            var res = apiCall<object>("meetings/" + id + "/recordings", Method.Delete, null);
        }
        public ActionResult gettoken()
        {
            return Content(tokenString);
        }
        public ActionResult redirect()
        {

            return Content("");
        }
    }
}