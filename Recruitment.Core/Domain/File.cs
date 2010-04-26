using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class File
    {
        private int _FileID;
        private int _FileType;

        public int FileType
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
        private string _Decription;

        public string Decription
        {
            get { return _Decription; }
            set { _Decription = value; }
        }

        public File()
        {
            throw new System.NotImplementedException();
        }

        public static Dictionary<int, string> GetFileTypes()
        {
            throw new System.NotImplementedException();
        }

        private string _FileName;

        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
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
