using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Data
{
    public class NHibernateTransaction : IDisposable
    {
        public NHibernateTransaction()
        {
            NHibernateSessionManager.Instance.BeginTransaction();
        }

        public void RollBackTransaction()
        {
            NHibernateSessionManager.Instance.RollbackTransaction();
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                NHibernateSessionManager.Instance.CommitTransaction();
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSession();
            }
        }

        #endregion
    }
}
