using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNETmcc75.Models
{
    [Table("tb_m_accounts")]
    public class Account
    {
        [Key, Column("employee_nik", TypeName ="nchar(5)")]
        public string EmployeeNik { get; set; }
        [Column("password"), MaxLength(255)]
        public string Password { get; set; }

        //cardinalitty
        public ICollection<AccountRole>? AccountRoles  { get; set; }
        public Employee? Employee  { get; set; }
    }
}
