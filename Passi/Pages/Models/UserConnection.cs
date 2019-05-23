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
using System.DirectoryServices.AccountManagement;
using ProtoBuf;

namespace Passi.Pages.Models
{
    [ProtoContract]
    public class UserConnection
    {
        [ProtoMember(1)]
        public string User { get; set; }
        [ProtoMember(2)]
        public string Domain { get; set; }
        // [ProtoMember(3)]
        public PrincipalContext PrincipalContext { get; set; }
        [ProtoMember(3)]
        public bool Authenticated { get; set; }

         public UserConnection(string domain, string username, string password)
         {
             User = username;
             Domain = domain;

             try
             {
                 PrincipalContext = new PrincipalContext(ContextType.Domain, domain, username, password);
                 Authenticated = PrincipalContext.ValidateCredentials(username, password, ContextOptions.SimpleBind);
             }
             catch (PrincipalException e)
             {
                 Console.WriteLine(e.Message);
             }
             Console.WriteLine(Authenticated);

         }

        [ProtoMember(4)]
        public PrincipalContextSurrogate PrincipalContextWrapper
        {
            get
            {
                return new PrincipalContextSurrogate(PrincipalContext);
            }
            set
            {
                PrincipalContext = value.ToPrincipalContext();
            }
        }



    }


    [ProtoContract]
    public class PrincipalContextSurrogate
    {
        public PrincipalContextSurrogate() { }

        [ProtoMember(5)]
        public ContextType ContextType { set; get; }
        [ProtoMember(6)]
        public string Name { set; get; }

        public PrincipalContextSurrogate(PrincipalContext context)
        {
            ContextType = context.ContextType;
            Name = context.Name;
        }
        public PrincipalContext ToPrincipalContext()
        {
            return new PrincipalContext(ContextType, Name);
        }
    }
    
}