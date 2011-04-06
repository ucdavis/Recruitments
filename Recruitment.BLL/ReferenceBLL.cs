using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.BLL
{
    public class ReferenceBLL : GenericBLL<Reference, int>
    {
        public static Reference GetByUploadID(string uploadId)
        {
            return daoFactory.GetReferenceDao().GetReferenceByUploadID(uploadId);
        }

        public static List<Reference> GetReferencesToBeNotified(Position position)
        {
            return daoFactory.GetReferenceDao().GetReferencesToBeNotified(position.ID);
        }
    }
}
