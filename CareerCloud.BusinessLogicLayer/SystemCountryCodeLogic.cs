using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class SystemCountryCodeLogic
	{
        protected IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
            _repository = repository;
        }
        public void Add(SystemCountryCodePoco[] pocos)
        {
            try
            {
                Verify(pocos);
                Add(pocos);
            }
            catch (ValidationException ex)
            {
                throw new AggregateException(ex);
            }
        }

        public virtual SystemCountryCodePoco Get(string Code)
        {
            return _repository.GetSingle(c => c.Code == Code);
        }

        public virtual List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            try
            {
                Verify(pocos);
                Update(pocos);
            }
            catch (ValidationException ex)
            {
                throw new AggregateException(ex);
            }
        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> InnerExceptions = new List<ValidationException>();

            foreach (SystemCountryCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    InnerExceptions.Add(new ValidationException(900, "Cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    InnerExceptions.Add(new ValidationException(901, "Cannot be empty"));
                }
            }
            if (InnerExceptions.Count > 0)
            {
                throw new AggregateException(InnerExceptions);
            }
        }
    }
}

