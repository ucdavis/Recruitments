using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class File : DomainObject<int>
    {
        private FileType _FileType;

        public virtual FileType FileType
        {
            get { return _FileType; }
            set { _FileType = value; }
        }

        private string _Label;

        public virtual string Label
        {
            get { return _Label; }
            set { _Label = value; }
        }
        private string _Description;

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _FileName;

        public virtual string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public File()
        {
        }

        public virtual void Fill()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Save()
        {
            throw new System.NotImplementedException();
        }

        public virtual byte[] GetFileData()
        {
            throw new System.NotImplementedException();
        }

        public virtual byte[] ConvertFile(byte[] FileToConvert)
        {
            throw new System.NotImplementedException();
        }
    }
}
