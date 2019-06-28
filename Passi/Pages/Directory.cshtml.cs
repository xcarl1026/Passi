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
            ADActiveUserList = GetADUserList(Domain);

            /*foreach (var u in ADActiveUserList)
            {
                Console.WriteLine(u);
            }  */      
        }

        public List<string> GetADUserList(string domain)
        {
            ADActiveUserList = new List<string>();
            PrincipalContext context = new PrincipalContext(ContextType.Domain, domain, "administrator", "Letmein123!");
            UserPrincipal userPrin = new UserPrincipal(context);
            PrincipalSearcher searcher = new PrincipalSearcher(userPrin);
            foreach (var result in searcher.FindAll())
            {
               DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
               UserPrincipal u = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, de.Properties["samAccountName"].Value.ToString());
               if(u.Enabled == true)
                {
                    ADActiveUserList.Add(u.SamAccountName);
                }

            }
            searcher.Dispose();
            userPrin.Dispose();
            context.Dispose();
            ADActiveUserList.Sort();
            return ADActiveUserList;
        }

    }

}