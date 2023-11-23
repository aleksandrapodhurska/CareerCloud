using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyJobLogic : BaseLogic<CompanyJobPoco>
	{
		public CompanyJobLogic(IDataRepository<CompanyJobPoco> repository) : base(repository)
		{
		}
	}
}

