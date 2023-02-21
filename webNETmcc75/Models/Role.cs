using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNETmcc75.Models
{
    [Table("tb_m_roles")]
    public class Role
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Required, Column("name"), MaxLength(50)]
        public string Name { get; set; }

        //cardinality
        public ICollection<AccountRole>? AccountRole  { get; set; }
    }
}
