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
using System.DirectoryServices;

namespace Passi.Pages
{
    public class UserInfoGridModel : PageModel
    {
        DirectorySearch directorySearch;
        public string searchQuery { get; set; }
        public string ADUserDisplayName { get; set; }
        public string ADUsername { get; set; }
        public string ADUserEmailAddress { get; set; }
        public DateTime? ADUserLastBadPasswordAttempt { get; set; }
        public DateTime? ADUserLastLogon { get; set; }
        public List<string> SecurityGroups {get;set;}
        public List<string> ADProxyAddresses { get; set; }


       // public string StatusMessage { get; set; }

        public void OnGet()
        {
            ADUserDisplayName = "";
            ADUserEmailAddress = "";
            SecurityGroups = new List<string>();
            ADProxyAddresses = new List<string>();
            if (RouteData.Values["searchQuery"] != null)
            {
                searchQuery = RouteData.Values["searchQuery"].ToString();
            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                string domain = HttpContext.Session.GetString("Domain");
                Console.WriteLine(searchQuery);
                directorySearch = new DirectorySearch(searchQuery, domain);
                if(directorySearch.userResult != null)
                {
                    ADUserDisplayName = directorySearch.userResult.DisplayName;
                    ADUserEmailAddress = directorySearch.userResult.EmailAddress;
                    ADUserLastBadPasswordAttempt = directorySearch.userResult.LastBadPasswordAttempt;
                    ADUserLastLogon = directorySearch.userResult.LastLogon;
                    ADUsername = directorySearch.userResult.SamAccountName;
                    PropertyCollection properties = ((DirectoryEntry)directorySearch.userResult.GetUnderlyingObject()).Properties;
                    foreach(object property in properties["proxyaddresses"])
                    {
                        string p = property.ToString();
                        ADProxyAddresses.Add(p);
                    }
                    foreach (string g in directorySearch.SecurityGroups)
                    {
                        SecurityGroups.Add(g);
                        Console.WriteLine(g);
                    }
                }
                
                directorySearch.userResult.Dispose();
                directorySearch.context.Dispose();
       
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
                    directorySearch.context.Dispose();
                    directorySearch.userResult.Dispose();
                }
                catch (PasswordException exception)
                {
                    StatusMessage = exception.Message;
                    Console.WriteLine(StatusMessage);
                }
            }
            return Content(StatusMessage);
            
            
            /*PrincipalContext context = Connection(HttpContext.Session.GetString("Domain"));
            UserPrincipal adUser = UserPrincipal.FindByIdentity(context, 0, user);
            ADUserDisplayName = adUser.DisplayName;
            ADUserEmailAddress = adUser.EmailAddress;*/
        }

        public void OnPostUnlockAccount()
        {
            string domain = HttpContext.Session.GetString("Domain");
            directorySearch = new DirectorySearch(searchQuery, domain);
            directorySearch.userResult.UnlockAccount();
            directorySearch.context.Dispose();
            directorySearch.userResult.Dispose();
        }

    }

  
}