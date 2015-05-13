using System;
using System.Collections.Generic;
using System.Net;
using Dell.Boomi.Client.Model;
using Environment = Dell.Boomi.Client.Model.Environment;

namespace Dell.Boomi.Client
{
    public class BoomiClient
    {
        public BoomiClient(string accountId, NetworkCredential credential)
        {
            AccountId = accountId;
            Credential = credential;
        }

        #region Account Methods
        public IEnumerable<Account> GetAllAccounts(bool includeDeleted = false)
        {
            QueryFilter filter;
            if (!includeDeleted)
            {
                filter = QueryFilterFactory.CreateSimpleQueryFilter(new Expression
                {
                    Arguments = new List<string> { "deleted" },
                    Operator = Expression.ExpressionOperator.NOT_EQUALS,
                    Property = "status"
                });
            }
            else
            {
                filter = QueryFilterFactory.CreateSimpleQueryFilter(new Expression
                {
                    Operator = Expression.ExpressionOperator.IS_NOT_NULL,
                    Property = "id"
                });
            }

            return Query<Account>(filter);
        }

        public Account GetAccount(string id)
        {
            AssertNotNull(id, "id");
            return Get<Account>(id);
        }

        public Account CreateAccount(string name, DateTime expirationDate)
        {
            AssertNotNull(name, "name");
            AssertNotNull(expirationDate, "expirationDate");
            var account = new Account {Name = name, ExpirationDate = expirationDate};
            return Create(account);
        }

        public bool DeleteAccount(string id)
        {
            AssertNotNull(id, "id");
            return Delete<Account>(id);   
        }
        #endregion 
        
        #region Environment Methods
        public IEnumerable<Environment> GetAllEnvironments()
        {
            var filter = QueryFilterFactory.CreateSimpleQueryFilter(new Expression 
            {
                Operator = Expression.ExpressionOperator.IS_NOT_NULL,
                Property = "id"
            });
  
            return Query<Environment>(filter);
        }

        public Environment GetEnvironment(string id)
        {
            AssertNotNull(id, "id");
            return Get<Environment>(id);
        }

        public Environment CreateEnvironment(string name, string classification)
        {
            AssertNotNull(name, "name");
            AssertNotNull(classification, "classification");
            var environment = new Environment { Name = name, Classification = classification.ToUpper() };
            return Create(environment);
        }

        public Environment SetEnvironment(string id, string name, string classification)
        {
            AssertNotNull(id, "id");
            var environment = new Environment { Id = id, Name = name, Classification = classification == null ? null : classification.ToUpper() }; 
            return Update(id, environment);
        }

        public bool DeleteEnvironment(string id)
        {
            AssertNotNull(id, "id");
            return Delete<Environment>(id);
        }

        #endregion

        #region Generic Methods
        protected IEnumerable<T> Query<T>(QueryFilter filter)
        {
            try
            {
                return CreateBoomiGenericClient<T>().Query(filter).Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        protected T Get<T>(string id)
        {
            
            try
            {
                return CreateBoomiGenericClient<T>().Get(id).Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }      
        }

        protected T Create<T>(T newItem)
        {
            try
            {
                return CreateBoomiGenericClient<T>().Create(newItem).Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        protected T Update<T>(string id, T newItem)
        {
            try
            {
                return CreateBoomiGenericClient<T>().Update(id, newItem).Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        protected bool Delete<T>(string id)
        {
            try
            {
                return CreateBoomiGenericClient<T>().Delete(id).Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        protected BoomiGenericClient<T> CreateBoomiGenericClient<T>()
        {
            return new BoomiGenericClient<T>(AccountId, Credential.UserName, Credential.Password);  
        }

        protected void AssertNotNull(object value, string valueName)
        {
            if (value == null)
            {
                throw new ArgumentException("Must not be null", valueName);
            }
        }

        protected string AccountId;

        protected NetworkCredential Credential;
    #endregion
 
    }
}
