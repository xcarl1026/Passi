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
        public string Username { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
        }

       /* public void OnPostSearchADUser(string searchQuery)
        {
            string domain = HttpContext.Session.GetString("Domain");
            Console.WriteLine("We're here");
            /*directorySearch = new DirectorySearch(searchQuery, domain);
            ADUsername = directorySearch.userResult.SamAccountName;
            ADEmailaddress = directorySearch.userResult.EmailAddress;
        }*/

       /* public void OnGetSearch(string searchQuery)
        {
            Console.WriteLine("Something");
            string domain = HttpContext.Session.GetString("Domain");
            directorySearch = new DirectorySearch(searchQuery, domain);
            ADUsername = directorySearch.userResult.SamAccountName;
            ADEmailaddress = directorySearch.userResult.EmailAddress;
        }*/

    }
}