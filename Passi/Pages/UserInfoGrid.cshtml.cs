using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Passi.Pages.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using System.DirectoryServices.AccountManagement;



namespace Passi.Pages
{
    public class UserInfoGridModel : PageModel
    {
        DirectorySearch directorySearch;
        public string searchQuery { get; set; }
        public string ADUserDisplayName { get; set; }
        public String ADUserEmailAddress { get; set; }
        public void OnGet()
        {
            /*searchQuery = RouteData.Values["searchQuery"].ToString();
            Console.WriteLine(searchQuery);
            string domain = HttpContext.Session.GetString("Domain");
            directorySearch = new DirectorySearch(searchQuery, domain);
            ADUsername = directorySearch.userResult.SamAccountName;
            ADEmailaddress = directorySearch.userResult.EmailAddress;
            //directorySearch.userResult.Dispose();*/
        }

        public void UnlockAccount()
        {
            directorySearch.userResult.UnlockAccount();
        }

        public void ChangePassword()
        {
            searchQuery = RouteData.Values["searchQuery"].ToString();
            string domain = HttpContext.Session.GetString("Domain");
            directorySearch = new DirectorySearch(searchQuery, domain);
            directorySearch.userResult.SetPassword(RouteData.Values["pw"].ToString());
        }

        [HttpPost]
        public void OnPostSearchADUser(IFormCollection formCollection)
        {
            string personID = formCollection["searchQuery"];
            Console.WriteLine(personID);
            /*PrincipalContext context = Connection(HttpContext.Session.GetString("Domain"));
            UserPrincipal adUser = UserPrincipal.FindByIdentity(context, 0, user);
            ADUserDisplayName = adUser.DisplayName;
            ADUserEmailAddress = adUser.EmailAddress;*/
        }

        public void OnPostTest()
        {
            string pw = Request.Form["PwIn"];
            Response.Redirect("/Directory");
            
            
            // Console.WriteLine();
        }

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