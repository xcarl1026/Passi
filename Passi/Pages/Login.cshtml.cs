using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using System.DirectoryServices.Protocols;
using Passi.Pages.Models;
using Microsoft.AspNetCore.Http;

namespace Passi.Pages
{
    public class LoginModel : PageModel
    {

        public string Message { get; set; }
        public string Domain { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        public void OnGet()
        {
            //Message = "Enter your message here";
        }

        public void OnPost()
        {

            Domain = Request.Form[nameof(Domain)];
            Console.WriteLine("YOOOOOO  " + Domain);
            Username = Request.Form[nameof(Username)];
            Console.WriteLine("YOOOOOO  " + Username);
            Password = Request.Form[nameof(Password)];
            Console.WriteLine("YOOOOOO  " + Password);


            bool authenticated = false;

            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password) && authenticated==false)
            {
               authenticated = AuthenticatedUsers.ValidateUser(Domain, Username, Password);
            }
            else
            {
                Response.Redirect("/Index");
            }
            
            if(authenticated == true)
            {
                HttpContext.Session.SetString("Name", "yo");
                Response.Redirect("/success");
            }
            else
            {
                Response.Redirect("/Index");
            }

        }
        
    }
}