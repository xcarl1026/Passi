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

        /*public void OnGetSearchADUser(string user)
        {
            PrincipalContext context = Connection(HttpContext.Session.GetString("Domain"));
            UserPrincipal adUser = UserPrincipal.FindByIdentity(context, 0, user);
            ADUserDisplayName = adUser.DisplayName;
            ADUserEmailAddress = adUser.EmailAddress;
        }*/

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

        /* public void OnPostTest(string pwIn)
         {
             searchQuery = "lala";
             string domain = HttpContext.Session.GetString("Domain");
             directorySearch = new DirectorySearch(searchQuery, "nova");
             directorySearch.userResult.SetPassword(pwIn);
             Console.WriteLine(pwIn);
         }*/

        public PrincipalContext Connection(string domain)
        {
            PrincipalContext context = null;
            try
            {
                context = new PrincipalContext(ContextType.Domain, domain, "administrator", "Letmein123!");
                //userResult = UserPrincipal.FindByIdentity(context, searchQuery);
                //context.Dispose();
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return context;
        }
    }
}