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

        /*private DirectoryEntry GetLdapConnection()
        {
            
            DirectoryEntry ldapConnection = null;
            try
            {
                ldapConnection = new DirectoryEntry("LDAP://192.168.1.15:389");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to establish AD connection.");
            }
            
            return ldapConnection;

        }

        private SearchResult GetUser(string user)
        {
            DirectoryEntry ldapConnection = GetLdapConnection();
            DirectorySearcher searcher = new DirectorySearcher(ldapConnection);
            searcher.PropertiesToLoad.Add("displayname");
            searcher.PropertiesToLoad.Add("sammaccountname");
            searcher.PropertiesToLoad.Add("sn");
            searcher.PropertiesToLoad.Add("memberof");
            searcher.Filter = "(|(cn=" + user + ")(samaccountname=" + user + ")(displayname=" + user + ")(sn=" + user + "))";
            SearchResult result;

            try
            {
                result = searcher.FindOne();
            }
            catch (Exception e)
            {
                result = null;
            }

            searcher.Dispose();
            ldapConnection.Dispose();
            return result;
        }

        private ADUser ProcesUserGroups(string searchQuery)
        {
            List<string> userSecGroups = new List<string>();
            ADUser user = new ADUser();
       
                SearchResult result = GetUser(searchQuery);
                if (result != null)
                {

                    DirectoryEntry userObject = result.GetDirectoryEntry();
                    user.UserName = userObject.Properties["samaccountname"].Value.ToString();
                    user.UserDisplayName = userObject.Properties["displayname"].Value.ToString();

                    for (int counter = 0; counter < userObject.Properties["memberof"].Count; counter++)
                    {
                        userSecGroups.Add(userObject.Properties["memberof"][counter].ToString());

                    }
                    userSecGroups.Sort();
                    user.UserSecGroups = userSecGroups;
                    user.Found = true;
                }

            return user;
        }*/
    }
}
