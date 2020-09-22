using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCatalogSqLite.ViewModels
{
    public class ViewUserRoles
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool User { get; set; }
        public bool Manager { get; set; }
        public bool Admin { get; set; }
    }
}
