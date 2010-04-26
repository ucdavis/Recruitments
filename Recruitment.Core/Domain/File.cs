using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class File : DomainObject<int>
    {
        private FileType _FileType;

        public FileType FileType
        {
            get { return _FileType; }
            set { _FileType = value; }
        }

        private string _Label;

        public string Label
        {
            get { return _Label; }
            set { _Label = value; }
        }
        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _FileName;

        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public File()
        {
        }

        public void Fill()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public byte[] GetFileData()
        {
            throw new System.NotImplementedException();
        }

        public byte[] ConvertFile(byte[] FileToConvert)
        {
            throw new System.NotImplementedException();
        }
    }
}
