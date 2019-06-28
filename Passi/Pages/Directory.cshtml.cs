using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Passi.Pages.Models;
using System.DirectoryServices.AccountManagement;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;

namespace Passi.Pages
{
    public class DirectoryModel : PageModel
    {
        public string Username { get; set; }
        public string Domain { get; set; }
        public string searchQuery { get; set; }
        [BindProperty]
        [Required]
        public string ResetPassword { get; set; }
        public List<string> ADActiveUserList { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
            Domain = HttpContext.Session.GetString("Domain");
            ADActiveUserList = new DirectoryMethods().GetADUserList(Domain);
        }
    }

}