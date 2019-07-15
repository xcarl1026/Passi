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
        public ADUser ADUser { get; set; }

        public void OnGet(string id)
        {
            Username = HttpContext.Session.GetString("Username");
            Domain = HttpContext.Session.GetString("Domain");
            ADActiveUserList = new DirectoryMethods().GetADUserList(Domain);
            ADUser = null;
            if(id != null)
            {
                searchQuery = id;
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
    }

}