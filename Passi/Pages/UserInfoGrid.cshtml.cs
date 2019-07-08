using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Passi.Pages.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.DirectoryServices.AccountManagement;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;

namespace Passi.Pages
{
    public class UserInfoGridModel : PageModel
    {
        public string Username { get; set; }
        public string searchQuery { get; set; }
        public ADUser ADUser { get; set; }

       // public string StatusMessage { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
            if (RouteData.Values["searchQuery"] != null)
            {
                searchQuery = RouteData.Values["searchQuery"].ToString();
                string domain = HttpContext.Session.GetString("Domain");
                Console.WriteLine(searchQuery);
                ADUser user = new DirectoryMethods().DirectorySearch(searchQuery, domain);
                ADUser = user;
             
            }
            else
            {
                Console.WriteLine("string was empty or null");
            }
        }

        /* [HttpPost]
         public ContentResult OnPostResetPassword(IFormCollection formCollection)
         {
             string domain = HttpContext.Session.GetString("Domain");
             string pw = formCollection["ResetPassword"];
             string searchQuery = formCollection["searchQuery"];
             string StatusMessage = "";
             Console.WriteLine(pw + " AND " + searchQuery);
             if (!string.IsNullOrEmpty(searchQuery) && !string.IsNullOrEmpty(pw))
             {
                 StatusMessage = new DirectoryMethods().ResetPassword(searchQuery, pw);
             }
             return Content(StatusMessage);

         }*/

        [HttpPost]
        public ContentResult OnPostResetPassword(IFormCollection formCollection)
        {
            string domain = HttpContext.Session.GetString("Domain");
            string pw = formCollection["ResetPassword"];
            string searchQuery = formCollection["searchQuery"];
            string StatusMessage = "";
           
            return Content(StatusMessage);

        }

        public void OnPostUnlockAccount()
        {
            string domain = HttpContext.Session.GetString("Domain");
            string accountUnlockStatus = string.Empty;
            if (RouteData.Values["searchQuery"] != null)
            {
                searchQuery = RouteData.Values["searchQuery"].ToString();
                accountUnlockStatus = new DirectoryMethods().UnlockAccount(searchQuery);
            }
            else
            {
                Console.WriteLine("No searchQuery val");
            }
            
        }

    }

  
}