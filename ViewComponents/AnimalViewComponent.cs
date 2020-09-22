using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnimalCatalogSqLite.Models;

namespace AnimalCatalogSqLite.ViewComponents
{
    public class AnimalViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(Animal animal)
        {
            return Task.FromResult<IViewComponentResult>(View("AnimalItem", animal));
        }
    }
}