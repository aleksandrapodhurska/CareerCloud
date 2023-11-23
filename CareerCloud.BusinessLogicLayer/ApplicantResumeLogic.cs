using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class ApplicantResumeLogic : BaseLogic<ApplicantResumePoco>
	{
		public ApplicantResumeLogic(IDataRepository<ApplicantResumePoco> repository) : base(repository)
		{
            _repository = repository;
		}
        public override void Add(ApplicantResumePoco[] pocos)
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

        public override void Update(ApplicantResumePoco[] pocos)
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
        override protected void Verify(ApplicantResumePoco[] pocos)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantResumePoco poco in pocos)
            {
                if (poco.Resume == null || poco.Resume.Length <1)
                {
                    exceptionsList.Add(new ValidationException(113, "Resume cannot be empty"));
                }
            }
            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
    }
}

