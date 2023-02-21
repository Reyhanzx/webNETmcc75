using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webNETmcc75.Models
{
    [Table("tb_m_universities")]
    public class University
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Required, Column("name"), MaxLength(50)]
        public string Name { get; set; }

        //cardinality
        public ICollection<Education>? Educations { get; set; }
    }
}
