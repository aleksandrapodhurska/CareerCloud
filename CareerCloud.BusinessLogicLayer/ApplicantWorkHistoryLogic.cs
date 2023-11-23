using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
	{
		public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
		{
            _repository = repository;
		}
        public override void Add(ApplicantWorkHistoryPoco[] pocos)
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

        public override void Update(ApplicantWorkHistoryPoco[] pocos)
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
        override protected void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantWorkHistoryPoco poco in pocos)
            {
                if (poco.CompanyName.Length < 3)
                {
                    exceptionsList.Add(new ValidationException(105, "Must be greater than 2"));
                }
            }
            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
    }
}

