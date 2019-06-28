using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace Passi.Pages.Models
{
    public class DirectoryQueries
    {
        private UserPrincipal userResult { get; set; }
        private PrincipalContext context { get; set; }
        private List<string> SecurityGroups { get; set; }
        private Dictionary<string, string> AppAuth { get; set; }

        public DirectoryQueries()
        {
            /*Explore file provider
             * var provider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
            var appAuthFile = provider.GetDirectoryContents(AppDomain.CurrentDomain.BaseDirectory);*/

            //Read JSON
            string file = "appauth.json";
            Console.WriteLine(File.Exists(file) ? "File exists." : "File does not exist.");
            if (File.Exists(file))
            {
                JObject o1 = JObject.Parse(File.ReadAllText(file));
                string json = o1.ToString();
                AppAuth = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                //Console.WriteLine(AppAuth["Password"]);
            }

        }

        public bool Authenticate(string domain, string username, string password)
        {
            bool authenticated = false;
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, domain);
                authenticated = context.ValidateCredentials(username, password, ContextOptions.SimpleBind);
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            return authenticated;
        }
    }
}
