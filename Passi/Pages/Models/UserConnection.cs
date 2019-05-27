using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security;
using System.DirectoryServices.Protocols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using System.Text;
using System.DirectoryServices;
using System.Net;
using System.Security.Permissions;
using System.DirectoryServices.AccountManagement;

namespace Passi.Pages.Models
{ 
    public class UserConnection
    {
        public string User { get; set; }
        public string Domain { get; set; }
        public bool Authenticated { get; set; }
       
        public UserConnection(string domain, string username, string password)
        {
            User = username;
            Domain = domain;

            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain, domain, username, password);
                Authenticated = context.ValidateCredentials(username, password, ContextOptions.SimpleBind);
                context.Dispose();
            }
            catch (PrincipalException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(Authenticated);

        }

    }
    
}