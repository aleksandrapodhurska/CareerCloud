using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class SecurityRoleLogic : BaseLogic<SecurityRolePoco>
	{
		public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repository) : base(repository)
		{
			_repository = repository;
		}
        public override void Add(SecurityRolePoco[] pocos)
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

        public override void Update(SecurityRolePoco[] pocos)
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

        override protected void Verify(SecurityRolePoco[] pocos)
        {
            List<ValidationException> InnerExceptions = new List<ValidationException>();

            foreach (SecurityRolePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Role))
                {
                    InnerExceptions.Add(new ValidationException(800, "Cannot be empty"));
                }
            }
            if (InnerExceptions.Count > 0)
            {
                throw new AggregateException(InnerExceptions);
            }
        }
    }
}

