using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace Passi.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPostLogin()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];
            var domain = Request.Form["domain"];
        }
        
    }
}