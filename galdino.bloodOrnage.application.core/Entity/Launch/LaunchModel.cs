using galdino.bloodOrnage.application.core.Entity.Base;
using galdino.bloodOrnage.application.core.Entity.Base.Account;

namespace galdino.bloodOrnage.application.core.Entity.Launch
{
    public class LaunchModel : BaseEntity
    {
        public string AccountDebit { get; set; }
      
        public string AccountCredit { get; set; }
       
        public double Value { get; set; }
        public string Message { get; set; }
    }
}
