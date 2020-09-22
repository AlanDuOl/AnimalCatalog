using System.ComponentModel.DataAnnotations;

namespace AnimalCatalogSqLite.ViewModels
{
    public class ViewAnimalInsert
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characteres")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characteres")]
        public string Genus { get; set; }
        [Display(Name = "Sub Specie")]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characteres")]
        public string SubSpecie { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characteres")]
        public string Specie { get; set; }
    }
}
