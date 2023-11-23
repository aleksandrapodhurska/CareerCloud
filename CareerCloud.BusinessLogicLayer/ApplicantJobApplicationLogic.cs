using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repository) : base(repository)
        {
            _repository = repository;
        }

        public override void Add(ApplicantJobApplicationPoco[] pocos)
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

        public override void Update(ApplicantJobApplicationPoco[] pocos)
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

        override protected void Verify(ApplicantJobApplicationPoco[] pocos)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantJobApplicationPoco poco in pocos)
            {
                if (poco.ApplicationDate > DateTime.Now)
                {
                    exceptionsList.Add(new ValidationException(110, "Cannot be greater than today"));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
    }
}

