using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
            _repository = repository;
        }
        public override void Add(ApplicantProfilePoco[] pocos)
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

        public override void Update(ApplicantProfilePoco[] pocos)
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
        override protected void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantProfilePoco poco in pocos)
            {
                if (poco.CurrentSalary.HasValue && poco.CurrentSalary < 0)
                {
                    exceptionsList.Add(new ValidationException(111, "CurrentSalary cannot be negative"));
                }
                if (poco.CurrentRate.HasValue && poco.CurrentRate < 0)
                {
                    exceptionsList.Add(new ValidationException(112, "CurrentRate cannot be negative"));
                }
            }
            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
    }
}

