using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNetCore.Http;

namespace Passi.Pages.Models
{
    public class DirectorySearch
    {
       // SearchResult searchResult { get; set; }
        public UserPrincipal userResult { get; set; }
        public PrincipalContext context { get; set; }
        public List<string> SecurityGroups { get; set; }


        public DirectorySearch(string searchQuery, string domain)
        {
            try
            {
                context = new PrincipalContext(ContextType.Domain, domain, "administrator", "Letmein123!");
                userResult = UserPrincipal.FindByIdentity(context, searchQuery);
                if(userResult != null)
                {
                    SecurityGroups = new List<string>();
                    PrincipalSearchResult<Principal> groups = userResult.GetGroups();
                    foreach(Principal p in groups)
                    {
                        if(p is GroupPrincipal)
                        {
                            SecurityGroups.Add(p.ToString());
                        }
                        
                    }
                }
                //context.Dispose();
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
           
        }

      }

    
}
