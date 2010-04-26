using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.BLL
{
    public class TemplateBLL : GenericBLL<Template, int>
    {
        public static Template GetFirstByTypeName(string typeName)
        {
            TemplateType reminderTemplateType = new TemplateType();
            reminderTemplateType.Type = typeName;

            reminderTemplateType = daoFactory.GetTemplateTypeDao().GetUniqueByExample(reminderTemplateType);

            List<Template> templates = daoFactory.GetTemplateDao().GetTemplatesByType(reminderTemplateType);

            if (templates.Count > 0)
            {
                return templates[0];
            }
            else
            {
                return null;
            }
        }
    }
}
