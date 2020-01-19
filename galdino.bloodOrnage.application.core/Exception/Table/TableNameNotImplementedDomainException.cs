using System.Runtime.Serialization;

namespace galdino.bloodOrnage.application.core.Exception.Table
{
    public class TableNameNotImplementedDomainException : System.Exception
    {
        #region .::Methods
        public TableNameNotImplementedDomainException(string message) : base(message) { }
        public TableNameNotImplementedDomainException() : base() { }
        private TableNameNotImplementedDomainException(SerializationInfo info, StreamingContext context) { }
        #endregion
    }
}
