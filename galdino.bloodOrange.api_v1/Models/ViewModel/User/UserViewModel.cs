using galdino.bloodOrange.api_v1.Models.Interface;
using galdino.bloodOrnage.application.core.Entity.User;
using System.ComponentModel.DataAnnotations;

namespace galdino.bloodOrange.api_v1.Models.ViewModel.User
{
    public class UserViewModel : IViewModel<UserDomain>
    {
        /// <summary>
        /// Seu usuário do sistema
        /// </summary>
        /// <example>Galdino</example>
        [Required]
        public string Login { get; set; }
        /// <summary>
        /// Sua senha cadastrada no sistema
        /// </summary>
        /// <example>123456</example>
        [Required]
        public string Password { get; set; }
    }
}
