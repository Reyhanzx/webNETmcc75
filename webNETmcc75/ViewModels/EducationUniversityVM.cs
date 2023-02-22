using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webNETmcc75.ViewModels
{
    public class EducationUniversityVM
    {
        public int Id { get; set; }
        
        public string Major { get; set; }
        [MaxLength( 2),MinLength(2, ErrorMessage = "S1 paham")]
        [Required(ErrorMessage = "S1 paham")]
        public string Degree { get; set; }
        [Range(0, 4, ErrorMessage ="Inputan cuma 0 sammpe 4 LOL")]
        public float GPA { get; set; }
        [Display(Name ="University Name")]
        public string UniversityName { get; set; }
    }
}
