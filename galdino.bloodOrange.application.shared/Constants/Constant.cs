using System.Globalization;

namespace galdino.bloodOrange.application.shared.Constants
{
    public class Constant
    {
        private const string DefaultLanguage = "pt-BR";
        public static CultureInfo DefaultCulture = new CultureInfo(DefaultLanguage);
        private const string CodLanguage = "en-US";
        public static CultureInfo UsCulture = new CultureInfo(CodLanguage);
        public const string EMPRESAID_CLAIM = "EmpresaId";
    }
}
