using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace CAESDO.Recruitment.Data
{
    public abstract class TransactionScopeBase : IDisposable
    {
        private bool transactionCommitted;

        private static ISession Session
        {
            get { return NHibernateSessionManager.Instance.GetSession(); }
        }

        protected TransactionScopeBase()
        {
            transactionCommitted = false;
            //Session.BeginTransaction();
            NHibernateSessionManager.Instance.BeginTransaction();
        }

        public void RollBackTransaction()
        {
            //Session.Transaction.Rollback();
            NHibernateSessionManager.Instance.RollbackTransaction();

            transactionCommitted = false;
        }

        public void CommitTransaction()
        {
            //Session.Transaction.Commit();
            NHibernateSessionManager.Instance.CommitTransaction();

            transactionCommitted = true;
        }

        public bool HasOpenTransaction
        {
            //get { return Session.Transaction.IsActive; }
            get { return NHibernateSessionManager.Instance.HasOpenTransaction(); }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (transactionCommitted == false) //rollback the transaction if it hasn't been committed
            {
                //RollBackTransaction();
                NHibernateSessionManager.Instance.RollbackTransaction();
            }
        }

        #endregion
    }
}
