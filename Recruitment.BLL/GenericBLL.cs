using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.ComponentModel;
using CAESDO.Recruitment.Core.Abstractions;
using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.BLL
{
    [DataObject]
    public class GenericBLL<T, IdT>
    {
        public static IPrincipal UserContext = new UserContext();

        public static IDaoFactory daoFactory
        {
            get
            {
                return new NHibernateDaoFactory();
            }
        }

        public GenericBLL()
        {

        }

        public static T GetByID(IdT id)
        {
            return daoFactory.GetGenericDao<T, IdT>().GetById(id, false);
        }

        public static T GetNullableByID(IdT id)
        {
            return daoFactory.GetGenericDao<T, IdT>().GetNullableByID(id);
        }

        public static List<T> GetAll()
        {
            return daoFactory.GetGenericDao<T, IdT>().GetAll();
        }

        public static List<T> GetAll(string propertyName, bool ascending)
        {
            return daoFactory.GetGenericDao<T, IdT>().GetAll(propertyName, ascending);
        }

        public static List<T> GetByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            return daoFactory.GetGenericDao<T, IdT>().GetByExample(exampleInstance, propertiesToExclude);
        }

        public static T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            return daoFactory.GetGenericDao<T, IdT>().GetUniqueByExample(exampleInstance, propertiesToExclude);
        }

        public static List<T> GetByInclusionExample(T exampleInstance, params string[] propertiesToInclude)
        {
            return daoFactory.GetGenericDao<T, IdT>().GetByInclusionExample(exampleInstance, propertiesToInclude);
        }

        public static List<T> GetByInclusionExample(T exampleInstance, string sortPropertyName, bool ascending, params string[] propertiesToInclude)
        {
            return daoFactory.GetGenericDao<T, IdT>().GetByInclusionExample(exampleInstance, sortPropertyName, ascending, propertiesToInclude);
        }

        public static bool MakePersistent(T entity)
        {
            //Don't force a save
            return MakePersistent(entity, false);
        }

        public static bool MakePersistent(T entity, bool forceSave)
        {
            //If the entity is of type domainObject, call validation prior to persisting
            if (entity is DomainObject<T, IdT>)
            {
                DomainObject<T, IdT> obj = entity as DomainObject<T, IdT>;

                if (obj.isValid(entity) == false)
                {
                    NHibernateSessionManager.Instance.RollbackTransaction();
                    return false;
                }
            }

            //Perform the requested operation
            if (forceSave)
            {
                entity = daoFactory.GetGenericDao<T, IdT>().Save(entity);
            }
            else
            {
                entity = daoFactory.GetGenericDao<T, IdT>().SaveOrUpdate(entity);
            }

            return true;
        }

        /// <summary>
        /// Like MakePersistent, but throws an application exception on persistance failure
        /// </summary>
        public static void EnsurePersistent(T entity)
        {
            EnsurePersistent(entity, false);
        }

        public static void EnsurePersistent(T entity, bool forceSave)
        {
            bool success = MakePersistent(entity, forceSave);

            if (!success)
            {
                string validationErrors = null;

                //If the entity is of type domainObject, get validation errors
                if (entity is DomainObject<T, IdT>)
                {
                    var obj = entity as DomainObject<T, IdT>;

                    validationErrors = obj.getValidationResultsAsString(entity);
                }

                var errorMessage = new StringBuilder();

                errorMessage.AppendLine(string.Format("Object of type {0} could not be persisted\n\n", typeof(T)));

                if (!string.IsNullOrEmpty(validationErrors))
                {
                    errorMessage.Append("Validation Errors: ");
                    errorMessage.Append(validationErrors);
                }

                throw new ApplicationException(errorMessage.ToString());
            }
        }

        public static void Remove(T entity)
        {
            daoFactory.GetGenericDao<T, IdT>().Delete(entity);
        }

        /// <summary>
        /// Calls the remove method on all entities in the given list collection
        /// </summary>
        /// <remarks>Could probably abstract out to IEnumerable later</remarks>
        public static void Remove(List<T> entities)
        {
            foreach (T entity in entities)
            {
                Remove(entity);
            }
        }
    }
}
