using galdino.bloodOrange.application.shared.Interfaces.IMessages;

namespace galdino.bloodOrange.utils.Messaging
{
    public class Messages : IMessaging
    {
        #region .::Constructor
        private string message;
        public Messages(string message)
        {
            this.message = message;
        }
        #endregion

        #region .::Methods
        public string AUTH_ERROR() => 
            "Não foi possivel realizar o login, tente novamente";

        public string AUTH_SUCCESS() => 
            "Login efetuado com sucesso.";
        public string ACCOUNT_FOUNDS_INSUFFICIENT() => 
            "Saldo insuficiente para debitar.";

        public string ACCOUNT_DEBIT_NOT_EXISTS() => 
            "Não localizamos a conta para debitar o saldo.";

        public string ACCOUNT_NOT_FOUND() => 
            "Não localizamos a conta para creditar o saldo.";

        public string DEBIT_MADE_SUCCESS() => 
            "Debito efetuado com sucesso."; 

        #endregion
    }
}
