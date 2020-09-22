using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCatalogSqLite.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Field required!")]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field required!")]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characteres")]
        public string Genus { get; set; }
        [MaxLength(20, ErrorMessage = "Maximum of 20 characteres")]
        public string SubSpecie { get; set; }
        [Required(ErrorMessage = "Field required!")]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characteres")]
        public string Specie { get; set; }
    }
}
