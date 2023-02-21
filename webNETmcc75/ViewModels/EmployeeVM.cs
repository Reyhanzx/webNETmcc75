using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using webNETmcc75.Models;

namespace webNETmcc75.ViewModels
{
    public class EmployeeVM
    {
        [MaxLength(5), MinLength(5, ErrorMessage = "5 angka paham")]
        [Required(ErrorMessage = "5 angka lebih baik")]
        public string Nik { get; set; }
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }
        
        public GenderEnum Gender { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HireingDate { get; set; } = DateTime.Now;
        
        public string Email { get; set; }
        [Display(Name = " Phone Number")]
        public string? PhoneNumber { get; set; }
    }
    public enum GenderEnum
    {
        Male,
        Female

    }
}
