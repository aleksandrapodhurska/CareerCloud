using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
	{
		public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
		{
			_repository = repository;
		}
        public override void Add(ApplicantSkillPoco[] pocos)
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

        public override void Update(ApplicantSkillPoco[] pocos)
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
        override protected void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptionsList = new List<ValidationException>();
            foreach (ApplicantSkillPoco poco in pocos)
            {
                if (poco.StartMonth > 12)
                {
                    exceptionsList.Add(new ValidationException(101, "Cannot be greater than 12"));
                }
                if (poco.EndMonth > 12)
                {
                    exceptionsList.Add(new ValidationException(102, "Cannot be greater than 12"));
                }
                if (poco.StartYear < 1900)
                {
                    exceptionsList.Add(new ValidationException(103, "Cannot be less than 1900"));
                }
                if (poco.EndYear < poco.StartYear)
                {
                    exceptionsList.Add(new ValidationException(104, "Cannot be greater than StartYear"));
                }
            }
            if (exceptionsList.Count > 0)
            {
                throw new AggregateException(exceptionsList);
            }
        }
    }
}

