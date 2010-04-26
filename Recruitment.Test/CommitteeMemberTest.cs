﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using CAESDO.Recruitment.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Data;

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
            List<CommitteeMember> cList = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetAll();

            Assert.AreNotEqual<int>(0, cList.Count);

            foreach (CommitteeMember c in cList)
            {
                //Make sure each has a valid associated position
                Assert.IsFalse(c.AssociatedPosition.IsTransient());

                this.TestContext.WriteLine(c.AssociatedPosition.ToString());
            }
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

        [TestMethod]
        public void SaveDeleteTest()
        {
            CommitteeMember member = new CommitteeMember();

            MemberType mtype = NHibernateHelper.daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.CommitteeMember, false);

            //member.Email = StaticProperties.TestString;
            //member.UserID = StaticProperties.ExistingUserID;
            member.AssociatedPosition = NHibernateHelper.daoFactory.GetPositionDao().GetById(StaticProperties.ExistingPositionID, false);
            member.MemberType = mtype;

            //Make sure the file is valid
            Assert.IsTrue(ValidateBO<CommitteeMember>.isValid(member), "CommitteeMember not valid");

            Assert.IsTrue(member.IsTransient()); //file is not saved

            using (var ts = new TransactionScope())
            {
                member = NHibernateHelper.daoFactory.GetCommitteeMemberDao().SaveOrUpdate(member);

                ts.CommitTransaction();
            }

            Assert.IsFalse(member.IsTransient());

            CommitteeMember memberDB = new CommitteeMember();

            //Get a new file using the saved file's ID
            memberDB = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetById(member.ID, false);

            //Make sure they are the same
            Assert.AreEqual(member, memberDB);

            this.TestContext.WriteLine("Member Created had ID = {0}", memberDB.ID);

            //Now delete the file
            using (var ts = new TransactionScope())
            {
                NHibernateHelper.daoFactory.GetCommitteeMemberDao().Delete(member);

                ts.CommitTransaction();
            }

            //Make sure it is deleted
            bool isDeleted = false;

            try
            {
                member = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetById(memberDB.ID, false);
                member.IsTransient();
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                isDeleted = true;
            }

            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void ValidateAllTest()
        {

            List<CommitteeMember> cList = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetAll();

            Assert.AreNotEqual<int>(0, cList.Count); //should be at least on committeemember

            //Make sure every committeeMember in the database is valid
            foreach (CommitteeMember c in cList)
            {
                Assert.IsTrue(ValidateBO<CommitteeMember>.isValid(c));

                //this.TestContext.WriteLine("CommitteeMemberID = {0}, UserID = {1}, MemberType = {2}", c.ID, c.UserID, c.MemberType.Type);
            }
        }

        [TestMethod]
        public void CascadeMemberTypeSaveTest()
        {
            //Grab an existing member out of the database
            CommitteeMember member = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetById(StaticProperties.ExistingCommitteeMemberID, false);

            //Get all possible member types (lookup table)
            List<MemberType> memberTypeList = NHibernateHelper.daoFactory.GetMemberTypeDao().GetAll();

            Assert.IsFalse(member.MemberType.IsTransient()); //make sure we have a valid memberType

            int originalMemberTypeID = member.MemberType.ID;

            this.TestContext.WriteLine("Original MemberType = {0}", originalMemberTypeID);

            //Now find a memberType that doesn't equal the current one
            MemberType newType = new MemberType();

            foreach (MemberType mType in memberTypeList)
            {
                if (mType.ID != originalMemberTypeID)
                {
                    newType = mType;
                    break;
                }
            }

            //Make sure we got a different memberType
            Assert.AreNotEqual<int>(member.MemberType.ID, newType.ID);

            //Now update the committeeMember with the new memberType
            using (var ts = new TransactionScope())
            {
                member.MemberType = newType;
                NHibernateHelper.daoFactory.GetCommitteeMemberDao().SaveOrUpdate(member);

                ts.CommitTransaction();
            }

            this.TestContext.WriteLine("New MemberTypeID = {0}", newType.ID);

            //Get the original CommitteeMember back out of the database, and make sure the memberType changed to the new type
            CommitteeMember memberDB = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetById(StaticProperties.ExistingCommitteeMemberID, false);

            Assert.AreEqual<int>(memberDB.MemberType.ID, newType.ID);
        }

        [TestMethod()]
        public void AllCommitteeMembersTest()
        {
            Position target = NHibernateHelper.daoFactory.GetPositionDao().GetById(StaticProperties.ExistingPositionID, false);

            List<CommitteeMember> members = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetAllByMemberType(target, MemberTypes.AllCommittee);
            
            Assert.AreNotEqual<int>(members.Count, 0);

            this.TestContext.WriteLine("There are {0} members for this position", members.Count);

            foreach (CommitteeMember m in members)
            {
                Assert.AreNotEqual<int>((int)MemberTypes.FacultyMember, m.ID); //Don't want a faculty member in this list

                this.TestContext.WriteLine("PositionID = {0}, CommitteeMemberID = {1}, Type = {2}", m.AssociatedPosition.ID, m.ID, m.MemberType.Type);
            }
        }

        [TestMethod]
        public void AllFacultyMembersTest()
        {
            Position target = NHibernateHelper.daoFactory.GetPositionDao().GetById(StaticProperties.ExistingPositionID, false);

            List<CommitteeMember> members = NHibernateHelper.daoFactory.GetCommitteeMemberDao().GetAllByMemberType(target, MemberTypes.FacultyMember);

            Assert.AreNotEqual<int>(members.Count, 0);

            this.TestContext.WriteLine("There are {0} members for this position", members.Count);

            foreach (CommitteeMember m in members)
            {
                Assert.AreEqual<int>((int)MemberTypes.FacultyMember, m.ID); //only want faculty members
                this.TestContext.WriteLine("PositionID = {0}, CommitteeMemberID = {1}, Type = {2}", m.AssociatedPosition.ID, m.ID, m.MemberType.Type);
            }
        }

    }


}
