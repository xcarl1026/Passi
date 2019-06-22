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

       // DirectorySearch directorySearch;
        public string searchQuery { get; set; }
        [BindProperty]
        [Required]
        public string ResetPassword { get; set; }
        public List<string> ADActiveUserList { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
            string domain = HttpContext.Session.GetString("Domain");
            ADActiveUserList = GetADUserList(domain);

        }

        public List<string> GetADUserList(string domain)
        {
            ADActiveUserList = new List<string>();
            using (var context = new PrincipalContext(ContextType.Domain, domain, "administrator", "Letmein123!"))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context) { Enabled = true }))
                {
                    foreach (var user in searcher.FindAll())
                    {
                        PropertyCollection properties = ((DirectoryEntry)user.GetUnderlyingObject()).Properties;
                        string username = properties["samAccountName"].ToString();
                        ADActiveUserList.Add(username);
                    }
                }
            }
            return ADActiveUserList;
        }
        
    }
}