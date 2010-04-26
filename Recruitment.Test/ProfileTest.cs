﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Core.Utils;
using Microsoft.Practices.EnterpriseLibrary.Validation;


namespace CAESDO.Recruitment.Test
{
    /// <summary>
    ///This is a test class for CAESDO.Recruitment.Core.Domain.Profile and is intended
    ///to contain all CAESDO.Recruitment.Core.Domain.Profile Unit Tests
    ///</summary>
    [TestClass()]
    public class ProfileTest
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
        ///A test for Profile ()
        ///</summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            Profile target = new Profile();

            // TODO: Implement code to verify target
            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void ValidateAllTest()
        {
            List<Profile> pList = NHibernateHelper.daoFactory.GetProfileDao().GetAll();

            Assert.AreNotEqual<int>(0, pList.Count);

            foreach (Profile p in pList)
            {
                this.TestContext.WriteLine("Profile ID = {0}, Associated with Applicant ID = {1}", p.ID, p.AssociatedApplicant.ID);

                foreach (ValidationResult res in ValidateBO<Profile>.GetValidationResults(p))
                {
                    this.TestContext.WriteLine("Key = {0}, Message = {1}", res.Key, res.Message);
                }

                Assert.IsTrue(ValidateBO<Profile>.isValid(p)); //profile is valid
            }
        }

        [TestMethod()]
        public void FillProfile()
        {
            Profile profile = NHibernateHelper.daoFactory.GetProfileDao().GetById(StaticProperties.ExistingProfileID, false);

            Assert.IsNotNull(profile);

            Assert.AreEqual<int>(profile.ID, StaticProperties.ExistingProfileID);

            Assert.IsTrue(ValidateBO<Profile>.isValid(profile));
        }

        [TestMethod()]
        public void CheckApplicant()
        {
            Profile target = NHibernateHelper.daoFactory.GetProfileDao().GetById(StaticProperties.ExistingProfileID, false);
            Applicant applicant = NHibernateHelper.daoFactory.GetApplicantDao().GetById(StaticProperties.ExistingApplicantID, false);

            Assert.AreEqual<Applicant>(target.AssociatedApplicant, applicant);         
        }

        [TestMethod()]
        public void SaveDeleteProfile()
        {
            Applicant applicant = NHibernateHelper.daoFactory.GetApplicantDao().GetById(StaticProperties.ExistingApplicantID, false);

            Profile target = new Profile();
            target.AssociatedApplicant = applicant; //associate with the applicant
            target.Address1 = StaticProperties.TestString;
            target.City = StaticProperties.TestString;
            target.FirstName = StaticProperties.TestString;
            target.LastName = StaticProperties.TestString;
            
            target = NHibernateHelper.daoFactory.GetProfileDao().Save(target); //save the target

            this.TestContext.WriteLine("Profile created: ID={0}", target.ID);

            Assert.IsNotNull(target);
            Assert.IsFalse(target.IsTransient()); //make sure that target is saved to the database

            Profile targetDB = NHibernateHelper.daoFactory.GetProfileDao().GetById(target.ID, false);

            Assert.IsNotNull(targetDB);
            Assert.AreEqual<Profile>(target, targetDB);

            //Now delete the new profile
            using (new CAESDO.Recruitment.Data.NHibernateTransaction())
            {
                NHibernateHelper.daoFactory.GetProfileDao().Delete(target);
            }
            //Make sure it is deleted
            bool isDeleted = false;

            try
            {
                targetDB = NHibernateHelper.daoFactory.GetProfileDao().GetById(target.ID, false);
                targetDB.IsTransient(); //check to see if its in the db
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                isDeleted = true;
            }

            Assert.IsTrue(isDeleted);
        }

        [TestMethod()]
        public void GetProfileByApplicantExample()
        {
            Applicant applicant = NHibernateHelper.daoFactory.GetApplicantDao().GetById(StaticProperties.ExistingApplicantID, false);
            Profile profile = new Profile();
            profile.AssociatedApplicant = applicant;

            List<Profile> profiles = NHibernateHelper.daoFactory.GetProfileDao().GetByExample(profile);

            Assert.AreEqual<int>(profiles.Count, 1);

            profile = profiles[0];

            Assert.AreEqual<int>(profile.ID, StaticProperties.ExistingProfileID);

        }

        [TestMethod()]
        public void CheckApplications()
        {
        }
    }


}
