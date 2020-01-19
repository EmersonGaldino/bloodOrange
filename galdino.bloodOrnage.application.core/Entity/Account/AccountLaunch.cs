namespace galdino.bloodOrnage.application.core.Entity.Base.Account
{
    public class AccountLaunch : BaseEntity
    {
        public string AccountDebit { get; set; }       
        public string AccountCredit { get; set; }        
        public double Value { get; set; }
        public bool State { get; set; }
        public string Message { get; set; }
        public bool Debit { get; set; }
    }
}
