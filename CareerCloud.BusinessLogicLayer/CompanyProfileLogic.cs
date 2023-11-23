using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
	{
		public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
		{
			_repository = repository;
		}
        public override void Add(CompanyProfilePoco[] pocos)
        {
            try
            {
                Verify(pocos);
                base.Add(pocos);
            }
            catch (ValidationException ex)
            {
                throw new AggregateException(ex);
            }
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            try
            {
                Verify(pocos);
                base.Update(pocos);
            }
            catch (ValidationException ex)
            {
                throw new AggregateException(ex);
            }
        }
        override protected void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> InnerExceptions = new List<ValidationException>();

            foreach (CompanyProfilePoco poco in pocos)
            {
                if (!Validator.IsPhoneNumber(poco.ContactPhone))
                {
                    InnerExceptions.Add(new ValidationException(601, "Must correspond to a valid phone number (e.g. 416-555-1234)"));
                }
                if (!Validator.IsWebsiteDomainValid(poco.ContactPhone))
                {
                    InnerExceptions.Add(new ValidationException(600, "Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\""));
                }
            }
            if (InnerExceptions.Count > 0)
            {
                throw new AggregateException(InnerExceptions);
            }
        }
    }
}

