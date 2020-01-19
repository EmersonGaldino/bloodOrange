using galdino.bloodOrange.api_v1.Models.Interface;
using galdino.bloodOrnage.application.core.Entity.Base.Account;

namespace galdino.bloodOrange.api_v1.Models.ModelView.Launch
{
    public class LaunchModelView : IModelView<AccountLaunch>
    {
        public bool State { get; set; }
        public string Message { get; set; }
    }
}
