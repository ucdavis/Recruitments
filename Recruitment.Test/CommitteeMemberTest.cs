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
    ///This is a test class for CAESDO.Recruitment.Core.Domain.CommitteeMember and is intended
    ///to contain all CAESDO.Recruitment.Core.Domain.CommitteeMember Unit Tests
    ///</summary>
    [TestClass()]
    public class CommitteeMemberTest
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
        ///A test for AssociatedPosition
        ///</summary>
        [TestMethod()]
        public void AssociatedPositionTest()
        {
            Position target = NHibernateHelper.daoFactory.GetPositionDao().GetById(StaticProperties.ExistingPositionID, false);
            CommitteeMember member = new CommitteeMember();

            member.AssociatedPosition = target;

            List<CommitteeMember> members = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetByExample(member, "Email", "UserID", "MemberType");

            Assert.AreNotEqual<int>(members.Count, 0);
            
            this.TestContext.WriteLine("There are {0} members for this position", members.Count);
        }

        /// <summary>
        ///A test for CommitteeMember ()
        ///</summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            CommitteeMember target = new CommitteeMember();

            // TODO: Implement code to verify target
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for MemberType
        ///</summary>
        [TestMethod()]
        public void MemberTypeTest()
        {
            CommitteeMember target = new CommitteeMember();

            MemberType val = null; // TODO: Assign to an appropriate value for the property

            target.MemberType = val;


            Assert.AreEqual(val, target.MemberType, "CAESDO.Recruitment.Core.Domain.CommitteeMember.MemberType was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void AllCommitteeMembersTest()
        {
            Position target = NHibernateHelper.daoFactory.GetPositionDao().GetById(StaticProperties.ExistingPositionID, false);

            List<CommitteeMember> members = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetAllByMemberType(target, MemberTypes.AllCommittee);

            Assert.AreNotEqual<int>(members.Count, 0);

            this.TestContext.WriteLine("There are {0} members for this position", members.Count);

        }

        /// <summary>
        ///A test for UserID
        ///</summary>
        [TestMethod()]
        public void UserIDTest()
        {
            CommitteeMember target = new CommitteeMember();

            int val = 0; // TODO: Assign to an appropriate value for the property

            target.UserID = val;


            Assert.AreEqual(val, target.UserID, "CAESDO.Recruitment.Core.Domain.CommitteeMember.UserID was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

    }


}
