using Dapper;
using galdino.bloodOrange.application.shared.Interfaces.IConnections.BloodOrange;
using galdino.bloodOrange.data.persistence.Repository.Base;
using galdino.bloodOrnage.application.core.Entity.User;
using galdino.bloodOrnage.application.core.Interface.IRepository.User;
using System.Threading.Tasks;

namespace galdino.bloodOrange.data.persistence.Repository.User
{
	public class UserRepository : RepositoryBase<UserDomain>, IUserRepository
	{
		#region .::Constructor
		public IConnectionBloodOrange Uow { get; set; }
		public UserRepository(IConnectionBloodOrange uow) : base(uow)
		{
			Uow = uow;
		}
		public IConnectionBloodOrange getUow() { return Uow; }
		#endregion

		#region .::Methods
		public async Task<UserDomain> ValidingUserAsync(UserDomain viewModel)
		{
			try
			{
				var query = $@"SELECT {GetFields()} FROM {GetTableName()} 
				         WHERE str_login = @Login
				         and str_password = @Password 
				         LIMIT 1";
				return await Uow.GetConnection()
					.QueryFirstOrDefaultAsync<UserDomain>(query,
						new
						{
							viewModel.Login,
							viewModel.Password,
						}, Uow.GetTransaction());

			}
			finally
			{
				Uow.Release();
			}
		}
		#endregion
	}
}
