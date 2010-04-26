using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace CAESDO.Recruitment.Data
{
    public class TrackingInterceptor : NHibernate.IInterceptor
    {
        #region IInterceptor Members

        public void AfterTransactionBegin(NHibernate.ITransaction tx)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void AfterTransactionCompletion(NHibernate.ITransaction tx)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void BeforeTransactionCompletion(NHibernate.ITransaction tx)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public int[] FindDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            return null; //Null means use default behavior

            //throw new Exception("The method or operation is not implemented.");
        }

        public object Instantiate(Type type, object id)
        {
            return null;

            //throw new Exception("The method or operation is not implemented.");
        }

        public object IsUnsaved(object entity)
        {
            return null;

            //throw new Exception("The method or operation is not implemented.");
        }

        public void OnDelete(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            if (previousState == null)
                return false;

            List<ChangedProperty> dirtyProperties = new List<ChangedProperty>();

            //For each property, check to see if it is dirty (only look at non collections)
            for (int i = 0; i < currentState.Length; i++)
            {
                if (types[i].IsCollectionType == false)
                {
                    if (currentState[i].Equals(previousState[i]) == false)
                    {
                        dirtyProperties.Add(new ChangedProperty(previousState[i].ToString(), currentState[i].ToString(), propertyNames[i], types[i]));
                        //dirtyProperties.Add(propertyNames[i], previousState[i].ToString());
                    }
                }
            }

            TrackChanges(dirtyProperties, entity, id);

            return false; //false means that the object wasn't modified

            //throw new Exception("The method or operation is not implemented.");
        }

        public bool OnLoad(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            return false;

            //throw new Exception("The method or operation is not implemented.");
        }

        public bool OnSave(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            return false;
            //throw new Exception("The method or operation is not implemented.");
        }

        public void PostFlush(System.Collections.ICollection entities)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void PreFlush(System.Collections.ICollection entities)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        private void TrackChanges(List<ChangedProperty> changeList, object target, object id)
        {
            if (HttpContext.Current == null)
                return;

            Guid changeID = Guid.NewGuid();

            System.IO.StreamWriter writer = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("RecruitmentTracking.txt"),true);

            writer.WriteLine("ChangeID {0} => Object type {1} with ID {2} was modified as follows", changeID.ToString(), target.GetType().Name, id.ToString());

            foreach (ChangedProperty change in changeList)
            {
                writer.WriteLine("--- Property {0} of type {1} was changed from {2} to {3}", change.PropertyName, change.type.Name, change.OldValue, change.NewValue);
            }

            writer.Close();
        }

        #endregion
    }

    public class ChangedProperty
    {
        public string OldValue;
        public string NewValue;
        public string PropertyName;
        public NHibernate.Type.IType type;

        public ChangedProperty(string OldValue, string NewValue, string PropertyName, NHibernate.Type.IType type)
        {
            this.OldValue = OldValue;
            this.NewValue = NewValue;
            this.PropertyName = PropertyName;
            this.type = type;
        }
    }
}
