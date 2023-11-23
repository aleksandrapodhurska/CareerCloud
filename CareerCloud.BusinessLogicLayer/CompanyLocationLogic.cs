using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
	{
		public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
		{
            _repository = repository;
		}
        public override void Add(CompanyLocationPoco[] pocos)
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

        public override void Update(CompanyLocationPoco[] pocos)
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
        override protected void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> InnerExceptions = new List<ValidationException>();
            foreach (CompanyLocationPoco poco in pocos)
            {
                if (poco.CountryCode == null || poco.CountryCode.Length < 1 )
                {
                    InnerExceptions.Add(new ValidationException(500, "CountryCode cannot be greater empty"));
                }
                if (poco.Province == null || poco.Province.Length < 1 )
                {
                    InnerExceptions.Add(new ValidationException(501, "Province cannot be greater empty"));
                }
                if (poco.Street == null || poco.Street.Length < 1 )
                {
                    InnerExceptions.Add(new ValidationException(502, "Street cannot be greater empty"));
                }
                if (poco.City == null || poco.City.Length < 1 )
                {
                    InnerExceptions.Add(new ValidationException(503, "City cannot be greater empty"));
                }
                if (poco.PostalCode == null || poco.PostalCode.Length < 1 )
                {
                    InnerExceptions.Add(new ValidationException(504, "PostalCode cannot be greater empty"));
                }
            }
            if (InnerExceptions.Count > 0)
            {
                throw new AggregateException(InnerExceptions);
            }
        }
    }
}

