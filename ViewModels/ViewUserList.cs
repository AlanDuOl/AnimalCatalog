using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCatalogSqLite.ViewModels
{
    public class ViewUserList
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
}
