using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Passi.Pages.Models;

namespace Passi.Pages
{
    public class GroupsInfoModel : PageModel
    {
        public string Username { get; set; }
        public string searchQuery { get; set; }
        public List<string> GroupeMembers { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
            if (RouteData.Values["searchQuery"] != null)
            {
                searchQuery = RouteData.Values["searchQuery"].ToString();
                string domain = HttpContext.Session.GetString("Domain");
                Console.WriteLine(searchQuery);
                GroupeMembers = new DirectoryMethods().GetGroupMembers(domain, searchQuery);
            }
            else
            {
                Console.WriteLine("string was empty or null");
            }
        }
    }
}