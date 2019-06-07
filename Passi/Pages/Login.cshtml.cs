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


        Authentication userConnection;

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
              
                userConnection = new Authentication(domain, username, password);
                authenticated = userConnection.Authenticated;
                HttpContext.Session.SetString("Username", userConnection.User);
                HttpContext.Session.SetString("Domain", userConnection.Domain);
            }           
          

            var page = (authenticated == true) ? "/Directory" : "/Login";
            Response.Redirect(page);
        }
        
    }
}