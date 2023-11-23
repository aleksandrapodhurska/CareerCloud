using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
	{
		public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository)
		{
            _repository = repository;
		}
        public override void Add(CompanyJobEducationPoco[] pocos)
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

        public override void Update(CompanyJobEducationPoco[] pocos)
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
        override protected void Verify(CompanyJobEducationPoco[] pocos)
        {
            List<ValidationException> InnerExceptions = new List<ValidationException>();
            foreach (CompanyJobEducationPoco poco in pocos)
            {
                if (poco.Major == null || poco.Major.Length < 3)
                {
                    InnerExceptions.Add(new ValidationException(200, "Major must be at least 2 characters"));
                }
                if (poco.Importance.CompareTo(0) < 0)
                {
                    InnerExceptions.Add(new ValidationException(201, "Importance cannot be less than 0"));
                }
            }
            if (InnerExceptions.Count > 0)
                {
                    throw new AggregateException(InnerExceptions);
                }
            }
        }
    }

