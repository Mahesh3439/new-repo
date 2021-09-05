using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using WbAPI.Models;

namespace WbAPI.Controllers
{
    public class ValuesController : ApiController
    {

        [Route("api/DoSignUp")]
        [HttpPost]
        public HttpResponseMessage DoSignUp(Users newUser)
        {
            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load("C:\\Users\\manisha.vishwakarma\\Desktop\\Car\\CarMaintenanceApplication\\CarMaintenanceApplication\\UserList.xml");
            XmlNode RootNode = XmlDocObj.SelectSingleNode("Users");
            XmlNode Node = RootNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "User", ""));
            Node.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "UserName", "")).InnerText = newUser.UserName.ToString();
            Node.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Password", "")).InnerText = newUser.Password;
            XmlDocObj.Save("C:\\Users\\manisha.vishwakarma\\Desktop\\Car\\CarMaintenanceApplication\\CarMaintenanceApplication\\UserList.xml");
            var response = Request.CreateResponse<Users>(HttpStatusCode.Created, newUser);
            return response;
        }
        [Route("api/LoginUser")]
        [HttpPost]
        public bool LoginUser(Users user)
        {
           
            List<Users> RegisteredUsers = new List<Users>();
            XDocument doc = XDocument.Load("C:\\Users\\manisha.vishwakarma\\Desktop\\Car\\CarMaintenanceApplication\\CarMaintenanceApplication\\UserList.xml");
            foreach (XElement element in doc.Descendants("Users")
                .Descendants("User"))
            {
                Users U = new Users();
                U.UserName = element.Element("UserName").Value;
                U.Password = element.Element("Password").Value;

                RegisteredUsers.Add(U);
            }
             bool IsValidUser = RegisteredUsers.Any(u => u.UserName.ToLower() == user.UserName && u.Password == user.Password);
            return IsValidUser;
        }


        [Route("api/GetCarServices")]
        public List<Service> GetCarServices()
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
            return services;
        }
        [Route("api/GetRegistrations")]
        public List<Registration> GetRegistrations()
        {
            List<Registration> registrations = new List<Registration>();
            XDocument doc = XDocument.Load("C:\\Users\\manisha.vishwakarma\\Desktop\\Car\\CarMaintenanceApplication\\WbAPI\\XML\\RegistrationList.xml");
            foreach (XElement element in doc.Descendants("Registrations")
                .Descendants("Registration"))
            {
                Registration r = new Registration();
                r.RegistrationNo = Convert.ToInt32(element.Element("RegistrationNo").Value);
                r.Make = element.Element("Make").Value;
                r.Model = element.Element("Model").Value;
                r.OwnerName = element.Element("OwnerName").Value;
                r.Service = element.Element("Service").Value;


                registrations.Add(r);
            }
            return registrations;
        }

        [Route("api/DoRegistration")]
        [HttpPost]
        public HttpResponseMessage DoRegistration(Registration reg)
        {

            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load("C:\\Users\\manisha.vishwakarma\\Desktop\\Car\\CarMaintenanceApplication\\WbAPI\\XML\\RegistrationList.xml");
            XmlNode RootNode = XmlDocObj.SelectSingleNode("Registrations");
            XmlNode Node = RootNode.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Registration", ""));
            Node.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "RegistrationNo", "")).InnerText = reg.RegistrationNo.ToString();
            Node.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Make", "")).InnerText = reg.Make;
            Node.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Model", "")).InnerText = reg.Model;
            Node.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "OwnerName", "")).InnerText = reg.OwnerName;
            Node.AppendChild(XmlDocObj.CreateNode(XmlNodeType.Element, "Service", "")).InnerText = reg.Service;
            XmlDocObj.Save("C:\\Users\\manisha.vishwakarma\\Desktop\\Car\\CarMaintenanceApplication\\WbAPI\\XML\\RegistrationList.xml");
            var response = Request.CreateResponse<Registration>(HttpStatusCode.Created, reg);
            return response;
        }
      
  
       

    }
}
