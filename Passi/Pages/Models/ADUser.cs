using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passi.Pages.Models
{
    public class ADUser
    {
        public string UserName { get; set; }

        public string UserDisplayName { get; set; }

        public List<string> UserSecGroups { get; set; }

        public bool Found { get; set; }

        public ADUser()
        {
            UserName = String.Empty;
            UserDisplayName = String.Empty;
            UserSecGroups = new List<string>();
            Found = false;
        }
       
    }
}
