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
using Microsoft.Extensions.Primitives;
using System.DirectoryServices;
using System.Net;
using System.Security.Permissions;

namespace Passi.Pages.Models
{
    public class AuthenticatedUsers
    {

        public static bool ValidateUser(string domain, string username, string password)
        {
            bool authenticated = false;
            try
            {
                // Create the new LDAP connection
                LdapDirectoryIdentifier ldi = new LdapDirectoryIdentifier(domain);
                System.DirectoryServices.Protocols.LdapConnection ldapConnection =
                    new System.DirectoryServices.Protocols.LdapConnection(ldi);
                Console.WriteLine("LdapConnection is created successfully.");
                ldapConnection.AuthType = AuthType.Negotiate;
                ldapConnection.SessionOptions.ProtocolVersion = 3;
                NetworkCredential nc = new NetworkCredential(username,
                   password); //password
                ldapConnection.Bind(nc);
                Console.WriteLine("LdapConnection authentication success");
                ldapConnection.Dispose();
                authenticated = true;
            }
            catch (LdapException e)
            {
                Console.WriteLine("\r\nUnable to login:\r\n\t" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\r\nUnexpected exception occured:\r\n\t" + e.GetType() + ":" + e.Message);
            }
            return authenticated;
        }


    }
}