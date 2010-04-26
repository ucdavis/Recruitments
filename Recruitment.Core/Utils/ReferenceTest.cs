﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;
namespace Recruitment.Tests
{
    /// <summary>
    ///This is a test class for CAESDO.Recruitment.Core.Domain.Reference and is intended
    ///to contain all CAESDO.Recruitment.Core.Domain.Reference Unit Tests
    ///</summary>
    [TestClass()]
    public class ReferenceTest
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
        ///A test for AcadTitle
        ///</summary>
        [TestMethod()]
        public void AcadTitleTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.AcadTitle = val;


            Assert.AreEqual(val, target.AcadTitle, "CAESDO.Recruitment.Core.Domain.Reference.AcadTitle was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Address1
        ///</summary>
        [TestMethod()]
        public void Address1Test()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Address1 = val;


            Assert.AreEqual(val, target.Address1, "CAESDO.Recruitment.Core.Domain.Reference.Address1 was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Address2
        ///</summary>
        [TestMethod()]
        public void Address2Test()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Address2 = val;


            Assert.AreEqual(val, target.Address2, "CAESDO.Recruitment.Core.Domain.Reference.Address2 was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ApplicationStepType
        ///</summary>
        [TestMethod()]
        public void ApplicationStepTypeTest()
        {
            Reference target = new Reference();

            ApplicationStepType val = ApplicationStepType.CurrentPosition; // TODO: Assign to an appropriate value for the property

            target.ApplicationStepType = val;


            Assert.AreEqual(val, target.ApplicationStepType, "CAESDO.Recruitment.Core.Domain.Reference.ApplicationStepType was not set correctl" +
                    "y.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AssociatedApplication
        ///</summary>
        [TestMethod()]
        public void AssociatedApplicationTest()
        {
            Reference target = new Reference();

            Application val = null; // TODO: Assign to an appropriate value for the property

            target.AssociatedApplication = val;


            Assert.AreEqual(val, target.AssociatedApplication, "CAESDO.Recruitment.Core.Domain.Reference.AssociatedApplication was not set correc" +
                    "tly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for City
        ///</summary>
        [TestMethod()]
        public void CityTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.City = val;


            Assert.AreEqual(val, target.City, "CAESDO.Recruitment.Core.Domain.Reference.City was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Complete
        ///</summary>
        [TestMethod()]
        public void CompleteTest()
        {
            Reference target = new Reference();

            bool val = false; // TODO: Assign to an appropriate value for the property

            target.Complete = val;


            Assert.AreEqual(val, target.Complete, "CAESDO.Recruitment.Core.Domain.Reference.Complete was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Country
        ///</summary>
        [TestMethod()]
        public void CountryTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Country = val;


            Assert.AreEqual(val, target.Country, "CAESDO.Recruitment.Core.Domain.Reference.Country was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Dept
        ///</summary>
        [TestMethod()]
        public void DeptTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Dept = val;


            Assert.AreEqual(val, target.Dept, "CAESDO.Recruitment.Core.Domain.Reference.Dept was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Email
        ///</summary>
        [TestMethod()]
        public void EmailTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Email = val;


            Assert.AreEqual(val, target.Email, "CAESDO.Recruitment.Core.Domain.Reference.Email was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Expertise
        ///</summary>
        [TestMethod()]
        public void ExpertiseTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Expertise = val;


            Assert.AreEqual(val, target.Expertise, "CAESDO.Recruitment.Core.Domain.Reference.Expertise was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Files
        ///</summary>
        [TestMethod()]
        public void FilesTest()
        {
            Reference target = new Reference();

            System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.File> val = null; // TODO: Assign to an appropriate value for the property

            target.Files = val;


            Assert.AreEqual(val, target.Files, "CAESDO.Recruitment.Core.Domain.Reference.Files was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FirstName
        ///</summary>
        [TestMethod()]
        public void FirstNameTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.FirstName = val;


            Assert.AreEqual(val, target.FirstName, "CAESDO.Recruitment.Core.Domain.Reference.FirstName was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Institution
        ///</summary>
        [TestMethod()]
        public void InstitutionTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Institution = val;


            Assert.AreEqual(val, target.Institution, "CAESDO.Recruitment.Core.Domain.Reference.Institution was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for isComplete ()
        ///</summary>
        [TestMethod()]
        public void isCompleteTest()
        {
            Reference target = new Reference();

            bool expected = false;
            bool actual;

            actual = target.isComplete();

            Assert.AreEqual(expected, actual, "CAESDO.Recruitment.Core.Domain.Reference.isComplete did not return the expected v" +
                    "alue.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LastName
        ///</summary>
        [TestMethod()]
        public void LastNameTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.LastName = val;


            Assert.AreEqual(val, target.LastName, "CAESDO.Recruitment.Core.Domain.Reference.LastName was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MiddleName
        ///</summary>
        [TestMethod()]
        public void MiddleNameTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.MiddleName = val;


            Assert.AreEqual(val, target.MiddleName, "CAESDO.Recruitment.Core.Domain.Reference.MiddleName was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Phone
        ///</summary>
        [TestMethod()]
        public void PhoneTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Phone = val;


            Assert.AreEqual(val, target.Phone, "CAESDO.Recruitment.Core.Domain.Reference.Phone was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Reference ()
        ///</summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            Reference target = new Reference();

            // TODO: Implement code to verify target
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for State
        ///</summary>
        [TestMethod()]
        public void StateTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.State = val;


            Assert.AreEqual(val, target.State, "CAESDO.Recruitment.Core.Domain.Reference.State was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        [TestMethod()]
        public void TitleTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Title = val;


            Assert.AreEqual(val, target.Title, "CAESDO.Recruitment.Core.Domain.Reference.Title was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Zip
        ///</summary>
        [TestMethod()]
        public void ZipTest()
        {
            Reference target = new Reference();

            string val = null; // TODO: Assign to an appropriate value for the property

            target.Zip = val;


            Assert.AreEqual(val, target.Zip, "CAESDO.Recruitment.Core.Domain.Reference.Zip was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

    }


}
