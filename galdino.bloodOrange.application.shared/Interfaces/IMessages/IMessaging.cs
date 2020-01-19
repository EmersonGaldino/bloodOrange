namespace galdino.bloodOrange.application.shared.Interfaces.IMessages
{
    public interface IMessaging
    {
        string AUTH_ERROR();
        string AUTH_SUCCESS();
        string ACCOUNT_DEBIT_NOT_EXISTS();
        string ACCOUNT_FOUNDS_INSUFFICIENT();
        string ACCOUNT_NOT_FOUND();
        string DEBIT_MADE_SUCCESS();
    }
}
