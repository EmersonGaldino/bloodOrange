using galdino.bloodOrange.application.Interface.IAccount;
using galdino.bloodOrnage.application.core.Entity.Base.Account;
using Moq;
using Xunit;

namespace galdino.bloodOrange.Test
{

    public class AccountTest 
    {
        

        [Fact]
        public void TestSearchAccount()
        {
            var model = new AccountLaunch
            {
                AccountCredit = "9baaa92e-7945-47ba-aee5-6d7e9386322b",
                AccountDebit = "d61fd10c-9f1b-4995-9c3b-3c531f489ca0",
                Value = 100              
                
            };
           

            var accountAppService = new Mock<IAccountAppService>();
            accountAppService.Setup(r => r.LaunchDispenseAccountAsync(model));

            Assert.Equal(model,model);
            //var mocker = new AutoMoqer();
            //mocker.Create<AccountService>();
            //var customerService = mocker.Resolve<AccountService>();
            //var repo = mocker.GetMock<IAccountRepository>();

            ////ACT
           // var result = accountAppService.Object.LaunchDispenseAccountAsync(model);

            ////ASERT
            //repo.Verify(r => r.GetAccountAsync("9baaa92e-7945-47ba-aee5-6d7e9386322b"));

        }
    }
}
