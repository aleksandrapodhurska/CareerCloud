using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
	public class SystemLanguageCodeLogic
	{
        protected IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }
        public void Add(SystemLanguageCodePoco[] pocos)
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

        public virtual SystemLanguageCodePoco Get(string LanguageID)
        {
            return _repository.GetSingle(c => c.LanguageID == LanguageID);
        }

        public virtual List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(SystemLanguageCodePoco[] pocos)
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

        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> InnerExceptions = new List<ValidationException>();

            foreach (SystemLanguageCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    InnerExceptions.Add(new ValidationException(1000, "Cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    InnerExceptions.Add(new ValidationException(1001, "Cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    InnerExceptions.Add(new ValidationException(1002, "Cannot be empty"));
                }
            }
            if (InnerExceptions.Count > 0)
            {
                throw new AggregateException(InnerExceptions);
            }
        }
    }
}

