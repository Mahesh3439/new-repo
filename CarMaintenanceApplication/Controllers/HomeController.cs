using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using WbAPI.Models;


namespace CarMaintenanceApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RegistrationDetails()
        {
            RegistrationList reg = new RegistrationList();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44314/");
                //HTTP GET
                var responseTask = client.GetAsync("api/GetRegistrations");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result;
                    List<Registration> regList   = JsonConvert.DeserializeObject<List<Registration>>(readTask);
                    reg.Registrations = regList;
                    return View(reg);                    
                }
                
            }
            return View();
            
        }
        [HttpGet]
        public ActionResult Register()
        {
            Registration reg = new Registration();
            List<Service> services = new List<Service>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44314/");
                //HTTP GET
                var responseTask = client.GetAsync("api/GetCarServices");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result;
                    List<Service> servicelist = JsonConvert.DeserializeObject<List<Service>>(readTask);
                    reg.Services = servicelist;
                }
                
            }
            
            return View(reg);
        }
        [HttpPost]
        public ActionResult Register(Registration reg)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            List<Service> services = new List<Service>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44314/");
                //HTTP POST
                var responseTask = client.PostAsJsonAsync("api/DoRegistration", reg).Result;
                if (responseTask.IsSuccessStatusCode)
                {
                    response.StatusCode = responseTask.StatusCode;
                }

            }

            return RedirectToAction("RegistrationDetails");
        }
        [HttpGet]
        public ActionResult GetServices()
        {
            ServiceList services = new ServiceList();
            XDocument doc = XDocument.Load("C:\\Users\\manisha.vishwakarma\\Desktop\\Car\\CarMaintenanceApplication\\WbAPI\\XML\\ServiceList.xml");
            foreach (XElement element in doc.Descendants("Services")
                .Descendants("Service"))
            {
                Service s = new Service();
                s.Id = Convert.ToInt32(element.Element("Id").Value);
                s.ServiceName = element.Element("ServiceName").Value;

                services.services.Add(s);
            }
            return View(services);
        }
        [HttpGet]
        public ActionResult Service()
        {
           List<Service> services = new List<Service>();
        XDocument doc = XDocument.Load("C:\\Users\\manisha.vishwakarma\\Desktop\\Car\\CarMaintenanceApplication\\WbAPI\\XML\\ServiceList.xml");
                foreach (XElement element in doc.Descendants("Services")
                    .Descendants("Service"))
                {
                Service s = new Service();
        s.Id = Convert.ToInt32(element.Element("Id").Value);
                s.ServiceName = element.Element("ServiceName").Value;

                services.Add(s);
                }
                return View();
    }
    [HttpPost]
        public ActionResult Service(Service service)
        {
           
                List<Service> services = new List<Service>();
                XDocument doc = XDocument.Load("C:\\Users\\manisha.vishwakarma\\Desktop\\Car\\CarMaintenanceApplication\\WbAPI\\XML\\ServiceList.xml");
                foreach (XElement element in doc.Descendants("Services")
                    .Descendants("Service"))
                {
                Service s = new Service();
                s.Id = Convert.ToInt32(element.Element("Id").Value);
                s.ServiceName = element.Element("ServiceName").Value;

                services.Add(s);
                }
                return View();
 
        }

    }
}