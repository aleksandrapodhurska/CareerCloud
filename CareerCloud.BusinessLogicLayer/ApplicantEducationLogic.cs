using System;
using System.Threading.Tasks;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
            _repository = repository;
        }

        public override void Add(ApplicantEducationPoco[] pocos)
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

        public override void Update(ApplicantEducationPoco[] pocos)
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

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantEducationPoco poco in pocos)
            {
                if (poco.Major == null || poco.Major.Length < 3)
                {
                    exceptionsList.Add(new ValidationException(107, "Cannot be empty or less than 3 characters"));
                }
                DateTime startDate = poco.StartDate ?? DateTime.MinValue;
                DateTime completionDate = poco.CompletionDate ?? DateTime.MinValue;

                if (startDate > DateTime.Now)
                {
                    exceptionsList.Add(new ValidationException(108, $"StartDate for Applicant {poco.Id} cannot be greater than today {DateTime.Now}"));
                }
                if (completionDate < startDate)
                {
                    exceptionsList.Add(new ValidationException(109, $"Completion for Applicant {poco.Id} cannot be earlier than StartDate {startDate}"));
                }
            }

            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
    }
}

