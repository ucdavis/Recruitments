﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;
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
            //Assert.IsFalse(target.isComplete());
        }

        /// <summary>
        ///A test for AppliedPosition
        ///</summary>
        [TestMethod()]
        public void AppliedPositionTest()
        {
            Application target = ExampleApplication;

            Assert.IsFalse(target.IsTransient());

            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AssociatedProfile
        ///</summary>
        [TestMethod()]
        public void AssociatedProfileTest()
        {
            Application target = ExampleApplication;

            Assert.IsFalse(target.IsTransient());

            Assert.AreEqual<int>(target.AssociatedProfile.ID, StaticProperties.ExistingProfileID);
        }

        /// <summary>
        ///A test for CurrentPositions
        ///</summary>
        [TestMethod()]
        public void CurrentPositionsTest()
        {
            Application target = new Application();

            System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.CurrentPosition> val = null; // TODO: Assign to an appropriate value for the property

            target.CurrentPositions = val;


            Assert.AreEqual(val, target.CurrentPositions, "CAESDO.Recruitment.Core.Domain.Application.CurrentPositions was not set correctly" +
                    ".");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Education
        ///</summary>
        [TestMethod()]
        public void EducationTest()
        {
            Application target = new Application();

            System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.Education> val = null; // TODO: Assign to an appropriate value for the property

            target.Education = val;


            Assert.AreEqual(val, target.Education, "CAESDO.Recruitment.Core.Domain.Application.Education was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Files
        ///</summary>
        [TestMethod()]
        public void FilesTest()
        {
            Application target = new Application();

            System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.File> val = null; // TODO: Assign to an appropriate value for the property

            target.Files = val;


            Assert.AreEqual(val, target.Files, "CAESDO.Recruitment.Core.Domain.Application.Files was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for isComplete ()
        ///</summary>
        [TestMethod()]
        public void isCompleteTest()
        {
            Application target = new Application();

            bool expected = false;
            bool actual;

            actual = target.isComplete();

            Assert.AreEqual(expected, actual, "CAESDO.Recruitment.Core.Domain.Application.isComplete did not return the expected" +
                    " value.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for References
        ///</summary>
        [TestMethod()]
        public void ReferencesTest()
        {
            Application target = new Application();

            System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.Reference> val = null; // TODO: Assign to an appropriate value for the property

            target.References = val;


            Assert.AreEqual(val, target.References, "CAESDO.Recruitment.Core.Domain.Application.References was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Surveys
        ///</summary>
        [TestMethod()]
        public void SurveysTest()
        {
            Application target = new Application();

            System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.Survey> val = null; // TODO: Assign to an appropriate value for the property

            target.Surveys = val;


            Assert.AreEqual(val, target.Surveys, "CAESDO.Recruitment.Core.Domain.Application.Surveys was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

    }


}
