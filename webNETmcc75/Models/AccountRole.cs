using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNETmcc75.Models
{
    [Table("tb_tr_account_roles")]
    public class AccountRole
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Required, Column("accountNik", TypeName = "nchar(5)")]
        public string AccountNik { get; set; }
        [Required, Column("role_id")]
        public int RoleId { get; set; }

        //relation n cardinality
        [ForeignKey(nameof(AccountNik))]
        public Account? Account  { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role? Role  { get; set; }
    }
}
