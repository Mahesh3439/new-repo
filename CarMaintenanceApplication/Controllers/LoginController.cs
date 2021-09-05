using CarMaintenanceApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using System.Xml.Linq;
using WbAPI.Models;

namespace CarMaintenanceApplication.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Users user)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44314/");
                //HTTP POST
                var responseTask = client.PostAsJsonAsync("api/DoSignUp", user).Result;
                if (responseTask.IsSuccessStatusCode)
                {
                    response.StatusCode = responseTask.StatusCode;
                }

            }

            return RedirectToAction("Login");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user)
        {
            if (ModelState.IsValid)
            {
                Users u = new Users();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44314/");
                    //HTTP GET
                    var responseTask = client.PostAsJsonAsync("api/LoginUser",user).Result;
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var readTask = responseTask.Content.ReadAsStringAsync().Result;
                       
                        bool IsValidUser = JsonConvert.DeserializeObject<bool>(readTask);
                        if (IsValidUser)
                        {
                            FormsAuthentication.SetAuthCookie(user.UserName, false);
                            return RedirectToAction("Index", "Home");
                        }
                    }

                }                                      
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View(user);
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
