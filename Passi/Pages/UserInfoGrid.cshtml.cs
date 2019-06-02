using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Passi.Pages
{
    public class UserInfoGridModel : PageModel
    {
        public string searchQuery { get; set; }
        public void OnGet()
        {
            searchQuery = RouteData.Values["searchQuery"].ToString();
            Console.WriteLine(searchQuery);
        }
    }
}