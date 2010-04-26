using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.DataInterfaces;

namespace CAESDO.Recruitment.Test
{
    public static class NHibernateHelper
    {
        public static IDaoFactory daoFactory
        {
            get { return new NHibernateDaoFactory(); }
        } 
    }
}
