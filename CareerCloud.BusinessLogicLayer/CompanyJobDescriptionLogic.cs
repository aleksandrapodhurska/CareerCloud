using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository)
        {
            _repository = repository;
        }
        public override void Add(CompanyJobDescriptionPoco[] pocos)
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

        public override void Update(CompanyJobDescriptionPoco[] pocos)
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
        override protected void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            List<ValidationException> InnerExceptions = new List<ValidationException>();
            foreach (CompanyJobDescriptionPoco poco in pocos)
            {
                if (poco.JobName == null || poco.JobName.Length < 1)
                {
                    InnerExceptions.Add(new ValidationException(300, "JobName cannot be empty"));
                }
                if (poco.JobDescriptions == null || poco.JobDescriptions.Length < 1)
                {
                    InnerExceptions.Add(new ValidationException(301, "JobDescriptions cannot be empty"));
                }
            }
            if (InnerExceptions.Count > 0)
            {
                throw new AggregateException(InnerExceptions);
            }
        }
    }
}