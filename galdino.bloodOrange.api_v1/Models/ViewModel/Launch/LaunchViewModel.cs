using galdino.bloodOrange.api_v1.Models.Interface;
using galdino.bloodOrnage.application.core.Entity.Base.Account;
using System.ComponentModel.DataAnnotations;

namespace galdino.bloodOrange.api_v1.Models.ViewModel.Launch
{
    public class LaunchViewModel : IViewModel<AccountLaunch>
    {
        /// <summary>
        /// Codigo da conta que sera debitado o valor
        /// </summary>
        /// <example>32156513sad</example>
        [Required]
        public string AccountDebit { get; set; }
        /// <summary>
        /// Codigo da conta que sera creditado o valor
        /// </summary>
        /// <example>32156513sad</example>
        [Required]
        public string AccountCredit { get; set; }
        /// <summary>
        /// Valor a ser debitado e creditado em outra conta
        /// </summary>
        /// <example>127.50</example>
        [Required]
        public double Value { get; set; }
     
    }
}
