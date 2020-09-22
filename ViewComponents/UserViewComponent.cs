using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalCatalogSqLite.ViewModels;

namespace AnimalCatalogSqLite.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(ViewUserList user)
        {
            return Task.FromResult<IViewComponentResult>(View("UserItem", user));
        }
    }
}
