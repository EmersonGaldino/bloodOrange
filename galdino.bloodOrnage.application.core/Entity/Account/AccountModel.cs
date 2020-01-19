using System.ComponentModel.DataAnnotations.Schema;

namespace galdino.bloodOrnage.application.core.Entity.Base.Account
{
    [Table("AccountUser")]
    public class AccountModel : BaseEntity
    {
        [Column("accountnumber")]
        public string Account { get; set; }
        [Column("accountbalance")]
        public double CreditInCash { get; set; }
       
    }
}
