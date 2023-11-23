using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
	{
		public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
		{
            _repository = repository;
		}
        public override void Add(CompanyDescriptionPoco[] pocos)
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

        public override void Update(CompanyDescriptionPoco[] pocos)
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

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> InnerExceptions = new List<ValidationException>();
            foreach (CompanyDescriptionPoco poco in pocos)
            {
                if (poco.CompanyName == null || poco.CompanyName.Length < 3)
                {
                    InnerExceptions.Add(new ValidationException(106, "CompanyName must be greater than 2 characters"));
                }
                if (poco.CompanyDescription == null || poco.CompanyDescription.Length < 3)
                {
                    InnerExceptions.Add(new ValidationException(107, "CompanyDescription must be greater than 2 characters"));
                }
            }
            if (InnerExceptions.Count > 0)
            {
                throw new AggregateException(InnerExceptions);
            }
        }
    }
}

