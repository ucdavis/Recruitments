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
    ///This is a test class for CAESDO.Recruitment.Core.Domain.Survey and is intended
    ///to contain all CAESDO.Recruitment.Core.Domain.Survey Unit Tests
    ///</summary>
    [TestClass()]
    public class SurveyTest
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
        ///A test for Complete
        ///</summary>
        [TestMethod()]
        public void CompleteTest()
        {
            Survey target = new Survey();

            bool val = true;

            target.Complete = val;

            Assert.AreEqual(val, target.Complete, "CAESDO.Recruitment.Core.Domain.Survey.Complete was not set correctly.");

        }

    
        /// <summary>
        ///A test for Gender
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(NHibernate.MappingException), "Gender cannot be null.")]
        public void NullGenderTest()
        {
            Survey target = new Survey();
            Gender val = null;

            target.Gender = val;
        }

        [TestMethod()]
        public void isMaleTest()
        {
            Survey target = new Survey();
            Gender val = new Gender();
            val.GenderType = "male";

            target.Gender = val;

            Assert.AreEqual(target.Gender.GetType(), "male", "Gender ambiguity.");
        }

        /// <summary>
        ///A test for isComplete ()
        ///</summary>
        [TestMethod()]
        public void isCompleteTest()
        {
            Survey target = new Survey();

            bool expected = true;
            bool actual;

            target.Complete = true;
            actual = target.isComplete();

            Assert.AreEqual(expected, actual, "CAESDO.Recruitment.Core.Domain.Survey.isComplete did not return the expected valu" +
                    "e.");
            
        }


        /// <summary>
        ///A test for RecruitmentSrc
        ///</summary>
        [TestMethod]
            [ExpectedException(typeof(NHibernate.MappingException), "Must have recruitment source.")]
        public void RecruitmentSrcTest()
        {
            Survey target = new Survey();
            RecruitmentSrc val = null;

            target.RecruitmentSrc = val;
        }


        /// <summary>
        ///A test for TribalAffiliation
        ///</summary>
        [TestMethod]
            [ExpectedException(typeof(NHibernate.MappingException), "Must belong to a tribe.")]
        public void TribalAffiliationTest()
        {
            Survey target = new Survey();
            string val = null;

            target.TribalAffiliation = val;

        }

        [TestMethod()]
        public void belongsTribeTest()
        {
            
            Survey target = new Survey();
            string val = "Qwerty Tribe";

            target.TribalAffiliation = val;
            Assert.AreEqual(target.TribalAffiliation, val, "Tribal Affiliation does not match.");
        }

    }


}
