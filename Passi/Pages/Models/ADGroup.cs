using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passi.Pages.Models
{
    public class ADGroup
    {
        public string GroupName { get; set; }
        public List<ADGroupObject> GroupObjects { get; set; }
        public List<string> GroupObjectsNames { get; set; }
    }
}
