using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.BLL
{
    public class FileTypeBLL : GenericBLL<FileType, int>
    {
        public static FileType GetByName(string fileTypeName)
        {
            FileType fileType = new FileType();
            fileType.FileTypeName = fileTypeName;

            return FileTypeBLL.GetUniqueByExample(fileType, "ApplicationFile");
        }

        public static List<FileType> GetAllByApplicationFileType(bool ApplicationFileType, string propertyName, bool ascending)
        {
            return daoFactory.GetFileTypeDao().GetAllByApplicationFileType(ApplicationFileType, propertyName, ascending);
        }
    }
}
