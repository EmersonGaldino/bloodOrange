using System.Runtime.Serialization;

namespace galdino.bloodOrnage.application.core.Exception.Domain
{
    public class DomainException : System.Exception
    {
        #region .::Methdos
        public DomainException(string message) : base(message) { }
        public DomainException() : base() { }
        private DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
        #endregion
    }
}
