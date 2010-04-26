using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.BLL
{
    public class TemplateBLL : GenericBLL<Template, int>
    {
        public static Template GetFirstByTypeName(string typeName)
        {
            var template = from t in EntitySet
                               where t.TemplateType.Type == typeName
                               select t;

            return template.FirstOrDefault();
        }
    }
}
