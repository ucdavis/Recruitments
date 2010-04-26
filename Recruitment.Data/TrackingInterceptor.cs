using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Data
{
    public class TrackingInterceptor : NHibernate.IInterceptor
    {
        #region IInterceptor Members

        public void AfterTransactionBegin(NHibernate.ITransaction tx)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AfterTransactionCompletion(NHibernate.ITransaction tx)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void BeforeTransactionCompletion(NHibernate.ITransaction tx)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int[] FindDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public object Instantiate(Type type, object id)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public object IsUnsaved(object entity)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnDelete(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool OnLoad(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool OnSave(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void PostFlush(System.Collections.ICollection entities)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void PreFlush(System.Collections.ICollection entities)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
