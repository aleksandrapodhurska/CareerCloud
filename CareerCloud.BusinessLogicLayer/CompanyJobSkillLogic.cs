using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyJobSkillLogic : BaseLogic<CompanyJobSkillPoco>
	{
		public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> repository) : base(repository)
		{
			_repository = repository;
		}
        public override void Add(CompanyJobSkillPoco[] pocos)
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

        public override void Update(CompanyJobSkillPoco[] pocos)
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
        override protected void Verify(CompanyJobSkillPoco[] pocos)
        {
            List<ValidationException> InnerExceptions = new List<ValidationException>();
            foreach (CompanyJobSkillPoco poco in pocos)
            {
                if (poco.Importance < 0)
                {
                    InnerExceptions.Add(new ValidationException(400, "Importance cannot be less than 0"));
                }
            }
            if (InnerExceptions.Count > 0)
            {
                throw new AggregateException(InnerExceptions);
            }
        }
    }
}

