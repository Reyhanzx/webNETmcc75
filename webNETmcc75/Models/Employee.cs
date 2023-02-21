using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNETmcc75.Models
{
    [Table("tb_m_employees")]
    public class Employee
    {
        [Key, Column("nik", TypeName ="nchar(5)")]
        public string Nik { get; set; }
        [Required, Column("first_name"), MaxLength(50)]
        public string FirstName { get; set; }
        [Column("Lastt_name"), MaxLength(50)]
        public string? LastName { get; set; }
        [Required, Column("birthdate")]
        public DateTime BirthDate { get; set; }
        [Required, Column("gender")]
        public GenderEnum Gender { get; set; }
        [Required, Column("hiringdate")]
        public DateTime HireingDate { get; set; } = DateTime.Now;
        [Required, Column("email"), MaxLength(50)]
        public string Email { get; set; }
        [Column("phonenumber"), MaxLength(20)]
        public string? PhoneNumber { get; set; }

        
        //cardinality
        public ICollection<Profiling>? Profilings  { get; set; }
        public Account? Account  { get; set; }
    }
    public enum GenderEnum
    {
        Male,
        Female

    }


}
