using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace Passi.Pages
{
    public class DirectoryModel : PageModel
    {
        public string Username { get; set; }
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
        }

        public void OnPostSearch(string directoryQ)
        {
            Console.WriteLine(directoryQ);
        }
    }
}