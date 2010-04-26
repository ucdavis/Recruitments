using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using CAESDO.Recruitment.Core.Domain;

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
            TrackChanges(null, entity, id, ChangeTypes.Delete);
        }

        public bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            if (previousState == null)
            {
                //Note that the entity was updated, but leave out the property changes
                TrackChanges(null, entity, id, ChangeTypes.Update);
                return false;
            }

            List<ChangedProperty> dirtyProperties = new List<ChangedProperty>();

            //For each property, check to see if it is dirty (only look at non collections)
            for (int i = 0; i < currentState.Length; i++)
            {
                if (types[i].IsCollectionType == false)
                {
                    if ( Equals(currentState[i], previousState[i]) == false )
                    {
                        dirtyProperties.Add(new ChangedProperty(currentState[i] == null ? null : currentState[i].ToString(), propertyNames[i], null));
                        //dirtyProperties.Add(propertyNames[i], previousState[i].ToString());
                    }
                }
            }

            TrackChanges(dirtyProperties, entity, id, ChangeTypes.Update);

            return false; //false means that the object wasn't modified
        }

        public bool OnLoad(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            return false;

            //throw new Exception("The method or operation is not implemented.");
        }

        public bool OnSave(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            if (entity.GetType() == typeof(ChangeTracking) || entity.GetType() == typeof(ChangedProperty))
                return false;

            List<ChangedProperty> dirtyProperties = new List<ChangedProperty>();

            //For each property, check to see if it is dirty (only look at non collections)
            for (int i = 0; i < state.Length; i++)
            {
                if (types[i].IsCollectionType == false)
                {
                    //Add all properties as dirty
                    dirtyProperties.Add(new ChangedProperty(state[i] == null ? null : state[i].ToString(), propertyNames[i], null));
                }
            }

            TrackChanges(dirtyProperties, entity, id, ChangeTypes.Save);

            return false;
        }

        public void PostFlush(System.Collections.ICollection entities)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void PreFlush(System.Collections.ICollection entities)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        private void TrackChanges(List<ChangedProperty> changeList, object target, object id, ChangeTypes changeType)
        {
            ITrackable trackableObject = target as ITrackable;

            if (trackableObject == null || trackableObject.isTracked() == false)
                return;

            if (HttpContext.Current == null)
                return;
            
            ChangeTracking trackChange = new ChangeTracking();

            if ( trackableObject.arePropertiesTracked() )
                trackChange.AppendProperties(changeList, trackChange);

            trackChange.ChangeType = new NHibernateDaoFactory().GetChangeTypeDao().GetById((int)changeType, false);

            trackChange.UserName = HttpContext.Current.User.Identity.Name;
            trackChange.ObjectChanged = target.GetType().Name;
            trackChange.ObjectChangedID = id == null ? null : id.ToString();

            //Now we have a tracking object with the changed properties added to its change list
            //Make sure it is valid
            if (ValidateBO<ChangeTracking>.isValid(trackChange))
            {
                //Don't put this in a transaction becuase we are already in a transaction from the save/update/delete
                new NHibernateDaoFactory().GetChangeTrackingDao().SaveOrUpdate(trackChange);
            }
            
            //System.IO.StreamWriter writer = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("RecruitmentTracking.txt"),true);

            //writer.WriteLine("ChangeID {0} => Object type {1} with ID {2} was modified as follows", Guid.NewGuid(), target.GetType().Name, id.ToString());

            //foreach (ChangedProperty change in changeList)
            //{
            //    //trackChange.PropertyChanged = change.type.Name;
            //    //trackChange.PropertyChangedValue = change.NewValue;

            //    writer.WriteLine("--- Property {0} was changed to {1}", change.PropertyChanged, change.PropertyChangedValue);
            //}

            //writer.Close();
        }

        #endregion
    }

}
