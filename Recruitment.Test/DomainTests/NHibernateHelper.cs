using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.DataInterfaces;

namespace CAESDO.Recruitment.Test.DomainTests
{
    public static class NHibernateHelper
    {
        public static IDaoFactory DaoFactory
        {
            get { return new NHibernateDaoFactory(); }
        } 
    }
}