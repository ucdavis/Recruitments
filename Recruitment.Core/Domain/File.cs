using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class File : DomainObject<File, int>
    {
        private FileType _FileType;

        [NotNullValidator]
        public virtual FileType FileType
        {
            get { return _FileType; }
            set { _FileType = value; }
        }

        private string _Label;

        [IgnoreNulls]
        [StringLengthValidator(0, 100)]
        public virtual string Label
        {
            get { return _Label; }
            set { _Label = value; }
        }
        private string _Description;

        [IgnoreNulls]
        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _FileName;

        [NotNullValidator]
        [StringLengthValidator(1, 100)]
        public virtual string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public File()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Path to file root folder</param>
        /// <returns></returns>
        public virtual System.IO.FileInfo GetFileData(string path)
        {
            System.IO.FileInfo fileToDownload = new System.IO.FileInfo(path + this.ID);

            return fileToDownload;
            //if (fileToDownload.Exists)
            //{
            //    Response.Clear();

            //    //Control the name that they see
            //    Response.ContentType = "application/octet-stream";
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            //    Response.AddHeader("Content-Length", fileToDownload.Length.ToString());
            //    //Response.TransmitFile(path + FileID.ToString());
            //    Response.TransmitFile(fileToDownload.FullName);
            //    Response.End();
            //}
        }

        public virtual byte[] ConvertFile(byte[] FileToConvert)
        {
            throw new System.NotImplementedException();
        }
    }
}
