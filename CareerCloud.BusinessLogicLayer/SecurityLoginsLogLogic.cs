using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class SecurityLoginsLogLogic : BaseLogic<SecurityLoginsLogPoco>
	{
		public SecurityLoginsLogLogic(IDataRepository<SecurityLoginsLogPoco> repository) : base(repository)
		{
		}
	}
}

