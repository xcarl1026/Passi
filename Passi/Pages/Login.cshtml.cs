using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using System.DirectoryServices.Protocols;
using Passi.Pages.Models;
using Microsoft.AspNetCore.Http;
using System.Xml.Serialization;
using System.IO;
using System.DirectoryServices.AccountManagement;

namespace Passi.Pages
{
    public class LoginModel : PageModel
    {


        UserConnection userConnection;

        public void OnGet()
        {
            
        }

        public void OnPost(string domain, string username, string password)
        {
             
            bool authenticated = false;
            if (authenticated == false)
            {
                Console.WriteLine("YOOOOO " + username);
                Console.WriteLine("YOOOOO " + domain);
                Console.WriteLine("YOOOOO " + password);
                userConnection = new UserConnection(domain, username, password);
                authenticated = userConnection.Authenticated;                  
                
            }
           
            
                byte[] tempByte = ProtoSerializer.ProtoSerialize<UserConnection>(userConnection);
                HttpContext.Session.Set("userConnection", tempByte);
            

             UserConnection test = ProtoSerializer.ProtoDeserialize<UserConnection>(tempByte);
            //HttpContext.Session.Get("userConnection"
             string username2 = test.User;
             PrincipalContext conn = test.PrincipalContext;
            Console.WriteLine(userConnection.PrincipalContext.ToString());
            Console.WriteLine(conn.ToString());
            

            var page = (authenticated == true) ? "/success" : "/Index";
            Response.Redirect(page);
        }
        
    }
}