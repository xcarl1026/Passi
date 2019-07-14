using Microsoft.AspNetCore.Mvc;
using Passi.Pages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passi.Pages.Components.UserInfoGrid
{
    public class UserInfoGridViewComponent : ViewComponent
    {
        public IViewComponentResult
       Invoke(ADUser adObject)
        {
            return View("UserInfoGridPage", adObject);
        }
    }

   
}
