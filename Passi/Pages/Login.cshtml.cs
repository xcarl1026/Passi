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
using System.ComponentModel.DataAnnotations;

namespace Passi.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Domain { get; set; }
        [BindProperty]
        [Required]
        public string Username { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }
 

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                HttpContext.Session.Clear();
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                bool authenticated = false;
                if (authenticated == false)
                {
                    string[] splitDomain = Domain.Split(".");
                    string dusername = splitDomain[0] + "\\" + Username;
                    authenticated = new DirectoryMethods().Authenticate(Domain, dusername, Password);
                    HttpContext.Session.SetString("Username", dusername);
                    HttpContext.Session.SetString("Domain", Domain);
                }
                var page = (authenticated == true) ? "/Directory" : "/Login";
                return RedirectToPage(page);
            }
            else
            {
                return Page();
            }
        }
    }
}