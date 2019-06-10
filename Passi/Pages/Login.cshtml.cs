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
using System.DirectoryServices.AccountManagement;

namespace Passi.Pages
{
    public class LoginModel : PageModel
    {
        public string Username { get; set; }
        public string Domain { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                HttpContext.Session.Clear();
            }
        }

        public void OnPost(string domain, string username, string password)
        {
             
            bool authenticated = false;
            if (authenticated == false)
            {
                
                PrincipalContext context = Authentication(domain, username, password);
                authenticated = context.ValidateCredentials(username, password, ContextOptions.SimpleBind);
                //HttpContext.Session.SetString("Username", context.UserName);
                //HttpContext.Session.SetString("Domain", domain);
            }           
          

            var page = (authenticated == true) ? "/Directory" : "/Login";
            Response.Redirect(page);
        }

        private PrincipalContext Authentication(string domain, string username, string password)
        {
            PrincipalContext context = null;
            try
            {
                context = new PrincipalContext(ContextType.Domain, domain);               

            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return context;

        }

    }
}