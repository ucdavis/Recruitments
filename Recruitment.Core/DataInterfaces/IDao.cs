using System.Collections.Generic;
using System.Linq;

namespace CAESDO.Recruitment.Core.DataInterfaces
{
    public interface IDao<T, IdT>
    {
        IQueryable<T> GetQueryable();
        T GetById(IdT id, bool shouldLock);
        T GetNullableByID(IdT id);
        List<T> GetAll();
        List<T> GetAll(string propertyName, bool ascending);
        List<T> GetByExample(T exampleInstance, params string[] propertiesToExclude);
        List<T> GetByInclusionExample(T exampleInstance, params string[] propertiesToInclude);
        List<T> GetByInclusionExample(T exampleInstance, string propertyName, bool ascending, params string[] propertiesToInclude);
        T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude);
        T Save(T entity);
        T SaveOrUpdate(T entity);
        void Delete(T entity);
        void CommitChanges();
    }
}
