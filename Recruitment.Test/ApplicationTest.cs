﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace CAESDO.Recruitment.Test
{
    /// <summary>
    ///This is a test class for CAESDO.Recruitment.Core.Domain.Application and is intended
    ///to contain all CAESDO.Recruitment.Core.Domain.Application Unit Tests
    ///</summary>
    [TestClass()]
    public class ApplicationTest
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

        Application ExampleApplication;

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            ExampleApplication = NHibernateHelper.daoFactory.GetApplicationDao().GetById(StaticProperties.ExistingApplicationID, false);
        }
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
        ///A test for Application ()
        ///</summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            Application target = new Application();

            Assert.IsNotNull(target);
            Assert.IsTrue(target.IsTransient());
        }

        [TestMethod]
        public void ValidateAllTest()
        {
            List<Application> appList = NHibernateHelper.daoFactory.GetApplicationDao().GetAll();

            Assert.AreNotEqual<int>(0, appList.Count);

            foreach (Application app in appList)
            {
                this.TestContext.WriteLine("PositionID = {0}", app.ID);

                foreach (ValidationResult res in ValidateBO<Application>.GetValidationResults(app))
                {
                    this.TestContext.WriteLine("Key = {0}, Message = {1}", res.Key, res.Message);
                }

                Assert.IsTrue(ValidateBO<Application>.isValid(app));

            }
        }

        /// <summary>
        ///A test for AssociatedProfile
        ///</summary>
        [TestMethod()]
        public void AssociatedProfileTest()
        {
            Application target = ExampleApplication;

            Assert.IsFalse(target.IsTransient());

            Assert.IsNotNull(target.AssociatedProfile);
            Assert.AreEqual<int>(target.AssociatedProfile.ID, StaticProperties.ExistingProfileID);
        }

        [TestMethod]
        public void AppliedPosition()
        {
            Application target = ExampleApplication;

            Assert.IsNotNull(target.AppliedPosition);
        }

        /// <summary>
        ///A test for CurrentPositions
        ///</summary>
        [TestMethod()]
        public void CurrentPositionsTest()
        {
            Application target = ExampleApplication;

            Assert.IsNotNull(target.CurrentPositions);
            Assert.AreNotEqual<int>(target.CurrentPositions.Count, 0);
        }

        /// <summary>
        ///A test for Education
        ///</summary>
        [TestMethod()]
        public void EducationTest()
        {
            Application target = ExampleApplication;

            Assert.IsNotNull(target.Education);
            Assert.AreNotEqual<int>(target.Education.Count, 0);
        }

        /// <summary>
        ///A test for Files
        ///</summary>
        [TestMethod()]
        public void FilesTest()
        {
            Application target = ExampleApplication;

            Assert.IsNotNull(target.Files);
            Assert.AreNotEqual<int>(target.Files.Count, 0);            
        }

        /// <summary>
        ///A test for isComplete ()
        ///</summary>
        [TestMethod()]
        public void isCompleteTest()
        {
            Application target = ExampleApplication;

            bool Complete = false;

            foreach (Reference r in target.References)
            {
                Complete = Complete && r.isComplete();
            }

            foreach (Education edu in target.Education)
            {
                Complete = Complete && edu.isComplete();
            }

            foreach (Survey s in target.Surveys)
            {
                Complete = Complete && s.isComplete();
            }

            foreach (CurrentPosition p in target.CurrentPositions)
            {
                Complete = Complete && p.isComplete();
            }

            Assert.AreEqual<bool>(Complete, target.isComplete());
        }

        /// <summary>
        ///A test for References
        ///</summary>
        [TestMethod()]
        public void ReferencesTest()
        {
            Application target = ExampleApplication;

            Assert.IsNotNull(target.References);
            Assert.AreNotEqual<int>(target.References.Count, 0);         
        }

        /// <summary>
        ///A test for Surveys
        ///</summary>
        [TestMethod()]
        public void SurveysTest()
        {
            Application target = ExampleApplication;

            Assert.IsNotNull(target.Surveys);
            Assert.AreNotEqual<int>(target.Surveys.Count, 0);    
        }

    }


}
