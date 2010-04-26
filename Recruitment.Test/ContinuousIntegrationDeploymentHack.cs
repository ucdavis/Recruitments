using System;

namespace CAESDO.Recruitment.Test
{
    /// <summary>
    /// NOTE: This is a hack to make sure that the spring and SQLLite assemblies are copied into the Test deployemnt directory
    /// This class never gets called.
    /// </summary>
    public class ContinuousIntegrationDeploymentHack
    {
        public ContinuousIntegrationDeploymentHack()
        {
            new NHibernate.ProxyGenerators.CastleDynamicProxy.ProxyFactoryFactory();
            new System.Data.SQLite.SQLiteException();

            throw new Exception("This class should never be called or instantiated");
        }
    }

}