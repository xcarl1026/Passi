using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Passi.Pages.Models;
using System.DirectoryServices.AccountManagement;



namespace Passi.Pages
{
    public class DirectoryModel : PageModel
    {
        public string Username { get; set; }

       // DirectorySearch directorySearch;
        public string searchQuery { get; set; }
        public string ADUserDisplayName{ get; set; }
        public String ADUserEmailAddress { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
        }
    }
}