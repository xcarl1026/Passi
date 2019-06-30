using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passi.Pages.Models
{
    public class ADUser
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public List<string> SecurityGroups { get; set; }
        public List<string> ProxyAddresses { get; set; }
        public DateTime? LastBadPasswordAttempt { get; set; }
        public DateTime? LastLogon { get; set; }
        public bool AccountLocked { get; set; }
        public DateTime? AccountLockoutTime { get; set; }
        public string OU { get; set; }
        public bool UserCannotChangePassword { get; set; }

        public ADUser()
        {
            SecurityGroups = new List<string>();
            ProxyAddresses = new List<string>();
  
        }
       
    }
}
