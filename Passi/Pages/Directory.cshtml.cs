using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Passi.Pages.Models;



namespace Passi.Pages
{
    public class DirectoryModel : PageModel
    {
        DirectorySearch directorySearch;
        public string Username { get; set; }
        public string searchQuery { get; set; }
        public string ADUsername { get; set; }
        public String ADEmailaddress { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
        }

        public void OnPostSearchADUser()
        {
            string domain = HttpContext.Session.GetString("Domain");
            Console.WriteLine("We're here");
            /*directorySearch = new DirectorySearch(searchQuery, domain);
            ADUsername = directorySearch.userResult.SamAccountName;
            ADEmailaddress = directorySearch.userResult.EmailAddress;*/
        }

        
       /* public void OnPostSearch(string searchQuery)
        {
            Username = HttpContext.Session.GetString("Username");
           
        }*/

      /*  public void OnPostLogout()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/Login");
            
        }*/


    }
}