using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Passi.Pages.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.DirectoryServices.AccountManagement;
using System.ComponentModel.DataAnnotations;

namespace Passi.Pages
{
    public class UserInfoGridModel : PageModel
    {
        DirectorySearch directorySearch;
        public string searchQuery { get; set; }
        public string ADUserDisplayName { get; set; }
        public string ADUserEmailAddress { get; set; }

        public void OnGet()
        {
            searchQuery = RouteData.Values["searchQuery"].ToString();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                string domain = HttpContext.Session.GetString("Domain");
                Console.WriteLine(searchQuery);
                directorySearch = new DirectorySearch(searchQuery, domain);
                ADUserDisplayName = directorySearch.userResult.SamAccountName;
                ADUserEmailAddress = directorySearch.userResult.EmailAddress;
                directorySearch.context.Dispose();
                directorySearch.userResult.Dispose();
            }
            else
            {
                Console.WriteLine("string was empty or null");
            }
            
            /*string domain = HttpContext.Session.GetString("Domain");
            directorySearch = new DirectorySearch(searchQuery, domain);
            ADUserDisplayName = directorySearch.userResult.SamAccountName;
            ADUserEmailAddress = directorySearch.userResult.EmailAddress;
            //directorySearch.userResult.Dispose();*/
        }

       
        [HttpPost]
        public void OnPostResetPassword(IFormCollection formCollection)
        {
            string personID = formCollection["searchQuery"];
            Console.WriteLine(personID);
            /*PrincipalContext context = Connection(HttpContext.Session.GetString("Domain"));
            UserPrincipal adUser = UserPrincipal.FindByIdentity(context, 0, user);
            ADUserDisplayName = adUser.DisplayName;
            ADUserEmailAddress = adUser.EmailAddress;*/
        }

    }

  
}