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
        SearchResult searchResult { get; set; }
        public UserPrincipal userResult { get; set; }

        public DirectorySearch(string searchQuery, string domain)
        {
            
            PrincipalContext context = new PrincipalContext(ContextType.Domain, domain, "administrator", "Letmein123!");
            userResult = UserPrincipal.FindByIdentity(context, searchQuery);
        }

      }
}
