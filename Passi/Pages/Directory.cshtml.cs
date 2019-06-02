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
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
        }
        
        public void OnPostSearch(string searchQuery)
        {
            Username = HttpContext.Session.GetString("Username");
            Console.WriteLine("YOOOO " + searchQuery);
             string domain = HttpContext.Session.GetString("Domain");
            directorySearch = new DirectorySearch(searchQuery, domain);
            Console.WriteLine("find anything?:  " + directorySearch.userResult.DisplayName);
        }

      /*  public void OnPostLogout()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/Login");
            
        }*/


    }
}