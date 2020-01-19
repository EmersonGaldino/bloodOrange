using galdino.bloodOrnage.application.core.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace galdino.bloodOrnage.application.core.Entity.User
{
    [Table("tb_users")]
    public class UserDomain : BaseEntity
    {
        [Key]
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("str_description")]
        public string Description { get; set; }

        [Column("str_name")]
        public string Name { get; set; }

        [Column("str_login")]
        public string Login { get; set; }
        [Column("str_password")]
        public string Password { get; set; }
      
        [Column("str_email")]
        public string Email { get; set; }
    }
}
