﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using CAESDO.Recruitment.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Test.DomainTests
{
    /// <summary>
    ///This is a test class for CAESDO.Recruitment.Core.Domain.File and is intended
    ///to contain all CAESDO.Recruitment.Core.Domain.File Unit Tests
    ///</summary>
    [TestClass()]
    public class FileTest : DatabaseTestBase
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for File ()
        ///</summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            CAESDO.Recruitment.Core.Domain.File f = new CAESDO.Recruitment.Core.Domain.File();

            Assert.IsNotNull(f);
            Assert.IsTrue(f.IsTransient());
        }

        /// <summary>
        ///A test for FileType
        ///</summary>
        [TestMethod()]
        public void FileTypeTest()
        {
            List<CAESDO.Recruitment.Core.Domain.File> files = NHibernateHelper.DaoFactory.GetFileDao().GetAll();

            Assert.AreNotEqual<int>(files.Count, 0); //Make sure we got more than one file

            foreach (CAESDO.Recruitment.Core.Domain.File f in files)
            {
                //Make sure each file is valid
                Assert.IsTrue(ValidateBO<CAESDO.Recruitment.Core.Domain.File>.isValid(f), "Validation of File ID = {0} failed", f.ID);

                //Make sure each filetypeID is a pos integer with a database association
                Assert.IsTrue(f.FileType.ID > 0);
                Assert.IsFalse(f.FileType.IsTransient());

                Assert.IsFalse(string.IsNullOrEmpty(f.FileType.FileTypeName));

                this.TestContext.WriteLine("FileID = {0}, FileName = {1}, FileTypeID = {2}", f.ID, f.FileName, f.FileType.ID);
            }
        }

        [TestMethod]
        public void ValidateAllTest()
        {
            List<CAESDO.Recruitment.Core.Domain.File> f = NHibernateHelper.DaoFactory.GetFileDao().GetAll();

            foreach (CAESDO.Recruitment.Core.Domain.File file in f)
            {
                Assert.IsTrue(ValidateBO<CAESDO.Recruitment.Core.Domain.File>.isValid(file));
            }
        }

        [TestMethod]
        public void CascadeFileTypeSaveTest()
        {
            //Grab an existing file out of the database
            CAESDO.Recruitment.Core.Domain.File file = NHibernateHelper.DaoFactory.GetFileDao().GetById(StaticProperties.ExistingFileID, false);

            //Get all possible file types (lookup table)
            List<FileType> fileTypeList = NHibernateHelper.DaoFactory.GetFileTypeDao().GetAll();

            Assert.IsFalse(file.FileType.IsTransient()); //make sure we have a valid filetype

            int originalFileTypeID = file.FileType.ID;

            this.TestContext.WriteLine("Original FileTypeID = {0}", originalFileTypeID);

            //Now find a filetype that doesn't equal the current one
            FileType newType = new FileType();

            foreach (FileType fType in fileTypeList)
            {
                if (fType.ID != originalFileTypeID)
                {
                    newType = fType;
                    break;
                }
            }

            //Make sure we got a different filetype
            Assert.AreNotEqual<int>(file.FileType.ID, newType.ID);

            //Now update the file with the new fileType
            using (var ts = new TransactionScope())
            {
                file.FileType = newType;
                NHibernateHelper.DaoFactory.GetFileDao().SaveOrUpdate(file);

                ts.CommitTransaction();
            }

            this.TestContext.WriteLine("New FileTypeID = {0}", newType.ID);
            //Get the original file back out of the database, and make sure the fileType changed to the new type
            CAESDO.Recruitment.Core.Domain.File fileDB = NHibernateHelper.DaoFactory.GetFileDao().GetById(StaticProperties.ExistingFileID, false);

            Assert.AreEqual<int>(fileDB.FileType.ID, newType.ID);
        }

        [TestMethod]
        public void SaveDeleteTest()
        {
            CAESDO.Recruitment.Core.Domain.File file = new CAESDO.Recruitment.Core.Domain.File();

            FileType ftype = NHibernateHelper.DaoFactory.GetFileTypeDao().GetById(StaticProperties.ExistingFileTypeID, false);

            file.FileType = ftype;
            file.FileName = StaticProperties.TestString;
            file.Label = string.Empty;

            //Make sure the file is valid
            Assert.IsTrue(ValidateBO<CAESDO.Recruitment.Core.Domain.File>.isValid(file), "File Not Valid");
            
            Assert.IsTrue(file.IsTransient()); //file is not saved
            
            using (var ts = new TransactionScope())
            {
                file = NHibernateHelper.DaoFactory.GetFileDao().SaveOrUpdate(file);

                ts.CommitTransaction();
            }

            Assert.IsFalse(file.IsTransient()); //file should now be saved

            CAESDO.Recruitment.Core.Domain.File fileDB = new CAESDO.Recruitment.Core.Domain.File();

            //Get a new file using the saved file's ID
            fileDB = NHibernateHelper.DaoFactory.GetFileDao().GetById(file.ID, false);

            //Make sure they are the same
            Assert.AreEqual(file, fileDB);

            this.TestContext.WriteLine("File Created had ID = {0}", fileDB.ID);

            //Now delete the file
            using (var ts = new TransactionScope())
            {
                NHibernateHelper.DaoFactory.GetFileDao().Delete(file);

                ts.CommitTransaction();
            }

            //Make sure it is deleted
            bool isDeleted = false;

            try
            {
                file = NHibernateHelper.DaoFactory.GetFileDao().GetById(fileDB.ID, false);
                file.IsTransient();
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                isDeleted = true;
            }
            
            Assert.IsTrue(isDeleted);
        }

    }
}