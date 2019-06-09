using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Passi.Pages.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


namespace Passi.Pages
{
    public class UserInfoGridModel : PageModel
    {
        DirectorySearch directorySearch;
        public string searchQuery { get; set; }
        public string ADUsername { get; set; }
        public String ADEmailaddress { get; set; }
        public void OnGet()
        {
            searchQuery = RouteData.Values["searchQuery"].ToString();
            Console.WriteLine(searchQuery);
            string domain = HttpContext.Session.GetString("Domain");
            directorySearch = new DirectorySearch(searchQuery, domain);
            ADUsername = directorySearch.userResult.SamAccountName;
            ADEmailaddress = directorySearch.userResult.EmailAddress;
            //directorySearch.userResult.Dispose();*/
        }

        public void UnlockAccount()
        {
            directorySearch.userResult.UnlockAccount();
        }

        public void ChangePassword()
        {
            searchQuery = RouteData.Values["searchQuery"].ToString();
            string domain = HttpContext.Session.GetString("Domain");
            directorySearch = new DirectorySearch(searchQuery, domain);
            directorySearch.userResult.SetPassword(RouteData.Values["pw"].ToString());
        }

        public void OnPostSearchADUser()
        {
            Console.Write("yo");
        }

        public void OnPostTest()
        {
            string pw = Request.Form["PwIn"];
            Response.Redirect("/Directory");
            
            
            // Console.WriteLine();
        }

       
    }

  
}