using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Theme : DomainObject<int>
    {
        private string _Name;

        public virtual string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _DepartmentFIS;

        public virtual string DepartmentFIS
        {
            get { return _DepartmentFIS; }
            set { _DepartmentFIS = value; }
        }
        
        public Theme()
        {

        }
    }
}
