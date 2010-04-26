using System;
using System.Collections.Generic;
using System.Linq;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.BLL
{
    public class TemplateTypeBLL : GenericBLL<TemplateType, int>
    {
        public static TemplateType GetByName(string typeName)
        {
            TemplateType templateType = new TemplateType();
            templateType.Type = typeName;

            return TemplateTypeBLL.GetUniqueByExample(templateType);
        }

        /// <summary>
        /// Get just the templates that are email templates
        /// </summary>
        public static List<TemplateType> GetEmailTemplates()
        {
            var emailTemplates = from t in EntitySet
                                 where t.IsEmailTemplate
                                 orderby t.Type ascending 
                                 select t;

            return emailTemplates.ToList();
        }
    }
}
