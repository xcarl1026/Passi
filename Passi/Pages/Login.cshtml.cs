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


        UserConnection userConnection;

        public void OnGet()
        {
            //Message = "Enter your message here";
        }

        public void OnPost(string domain, string username, string password)
        {

            bool authenticated = false;
            if (authenticated == false)
            {
                Console.WriteLine("YOOOOO " + username);
                Console.WriteLine("YOOOOO " + domain);
                Console.WriteLine("YOOOOO " + password);
                try
                {
                    userConnection = new UserConnection(Request.Form[domain], Request.Form[username], Request.Form[password]);
                    authenticated = userConnection.Authenticated;
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }

            var page = (authenticated == true) ? "/success" : "/Index";
            RedirectToPage(page, userConnection);

            /*if (authenticated == true)
            {
                HttpContext.Session.SetString("Name", "yo");
                Response.Redirect("/success");
            }
            else
            {
                Response.Redirect("/Index");
            }*/

        }
        
    }
}