using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class SecurityLoginsRoleLogic : BaseLogic<SecurityLoginsRolePoco>
	{
		public SecurityLoginsRoleLogic(IDataRepository<SecurityLoginsRolePoco> repository) : base(repository)
		{
		}
	}
}

