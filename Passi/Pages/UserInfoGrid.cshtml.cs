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
       // public string StatusMessage { get; set; }

        public void OnGet()
        {
            ADUserDisplayName = "";
            ADUserEmailAddress = "";
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
        public ContentResult OnPostResetPassword(IFormCollection formCollection)
        {
            string domain = HttpContext.Session.GetString("Domain");
            string pw = formCollection["ResetPassword"];
            string searchQuery = formCollection["searchQuery"];
            string StatusMessage = "";
            Console.WriteLine(pw + " AND " + searchQuery);
            if (!string.IsNullOrEmpty(searchQuery) && !string.IsNullOrEmpty(pw))
            {
                directorySearch = new DirectorySearch(searchQuery, domain);
                try
                {
                    directorySearch.userResult.SetPassword(pw);
                    StatusMessage = "Password was successfully changed.";
                }
                catch (PasswordException exception)
                {
                    StatusMessage = exception.Message;
                    Console.WriteLine(StatusMessage);
                }
            }
            else
            {
                StatusMessage = "No value was received to change password.";
            }
            return Content(StatusMessage);
           
            
            
            
            /*PrincipalContext context = Connection(HttpContext.Session.GetString("Domain"));
            UserPrincipal adUser = UserPrincipal.FindByIdentity(context, 0, user);
            ADUserDisplayName = adUser.DisplayName;
            ADUserEmailAddress = adUser.EmailAddress;*/
        }

    }

  
}