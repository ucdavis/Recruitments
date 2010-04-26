﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using CAESDO.Recruitment.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Data;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace CAESDO.Recruitment.Test
{
    /// <summary>
    ///This is a test class for CAESDO.Recruitment.Core.Domain.Position and is intended
    ///to contain all CAESDO.Recruitment.Core.Domain.Position Unit Tests
    ///</summary>
    [TestClass()]
    public class PositionTest : DatabaseTestBase
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

        # region Generated Tests
        ///// <summary>
        /////A test for AdminAccepted
        /////</summary>
        //[TestMethod()]
        //public void AdminAcceptedTest()
        //{
        //    Position target = new Position();

        //    bool val = false; // TODO: Assign to an appropriate value for the property

        //    target.AdminAccepted = val;


        //    Assert.AreEqual(val, target.AdminAccepted, "CAESDO.Recruitment.Core.Domain.Position.AdminAccepted was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for AllowApps
        /////</summary>
        //[TestMethod()]
        //public void AllowAppsTest()
        //{
        //    Position target = new Position();

        //    bool val = false; // TODO: Assign to an appropriate value for the property

        //    target.AllowApps = val;


        //    Assert.AreEqual(val, target.AllowApps, "CAESDO.Recruitment.Core.Domain.Position.AllowApps was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for AssociatedApplications
        /////</summary>
        //[TestMethod()]
        //public void AssociatedApplicationsTest()
        //{
        //    Position target = new Position();

        //    System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.Application> val = null; // TODO: Assign to an appropriate value for the property

        //    target.AssociatedApplications = val;


        //    Assert.AreEqual(val, target.AssociatedApplications, "CAESDO.Recruitment.Core.Domain.Position.AssociatedApplications was not set correc" +
        //            "tly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Closed
        /////</summary>
        //[TestMethod()]
        //public void ClosedTest()
        //{
        //    Position target = new Position();

        //    bool val = false; // TODO: Assign to an appropriate value for the property

        //    target.Closed = val;


        //    Assert.AreEqual(val, target.Closed, "CAESDO.Recruitment.Core.Domain.Position.Closed was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for CommitteeMembers
        /////</summary>
        //[TestMethod()]
        //public void CommitteeMembersTest()
        //{
        //    Position target = new Position();

        //    System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.CommitteeMember> val = null; // TODO: Assign to an appropriate value for the property

        //    target.CommitteeMembers = val;


        //    Assert.AreEqual(val, target.CommitteeMembers, "CAESDO.Recruitment.Core.Domain.Position.CommitteeMembers was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for CommitteeView
        /////</summary>
        //[TestMethod()]
        //public void CommitteeViewTest()
        //{
        //    Position target = new Position();

        //    bool val = false; // TODO: Assign to an appropriate value for the property

        //    target.CommitteeView = val;


        //    Assert.AreEqual(val, target.CommitteeView, "CAESDO.Recruitment.Core.Domain.Position.CommitteeView was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for DatePosted
        /////</summary>
        //[TestMethod()]
        //public void DatePostedTest()
        //{
        //    Position target = new Position();

        //    DateTime val = new DateTime(); // TODO: Assign to an appropriate value for the property

        //    target.DatePosted = val;


        //    Assert.AreEqual(val, target.DatePosted, "CAESDO.Recruitment.Core.Domain.Position.DatePosted was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Deadline
        /////</summary>
        //[TestMethod()]
        //public void DeadlineTest()
        //{
        //    Position target = new Position();

        //    DateTime val = new DateTime(); // TODO: Assign to an appropriate value for the property

        //    target.Deadline = val;


        //    Assert.AreEqual(val, target.Deadline, "CAESDO.Recruitment.Core.Domain.Position.Deadline was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Departments
        /////</summary>
        //[TestMethod()]
        //public void DepartmentsTest()
        //{
        //    Position target = new Position();

        //    System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.Department> val = null; // TODO: Assign to an appropriate value for the property

        //    target.Departments = val;


        //    Assert.AreEqual(val, target.Departments, "CAESDO.Recruitment.Core.Domain.Position.Departments was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for DescriptionFileID
        /////</summary>
        //[TestMethod()]
        //public void DescriptionFileIDTest()
        //{
        //    Position target = new Position();

        //    int val = 0; // TODO: Assign to an appropriate value for the property

        //    target.DescriptionFileID = val;


        //    Assert.AreEqual(val, target.DescriptionFileID, "CAESDO.Recruitment.Core.Domain.Position.DescriptionFileID was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for FacultyView
        /////</summary>
        //[TestMethod()]
        //public void FacultyViewTest()
        //{
        //    Position target = new Position();

        //    bool val = false; // TODO: Assign to an appropriate value for the property

        //    target.FacultyView = val;


        //    Assert.AreEqual(val, target.FacultyView, "CAESDO.Recruitment.Core.Domain.Position.FacultyView was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for FinalVote
        /////</summary>
        //[TestMethod()]
        //public void FinalVoteTest()
        //{
        //    Position target = new Position();

        //    bool val = false; // TODO: Assign to an appropriate value for the property

        //    target.FinalVote = val;


        //    Assert.AreEqual(val, target.FinalVote, "CAESDO.Recruitment.Core.Domain.Position.FinalVote was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for HRAreaCode
        /////</summary>
        //[TestMethod()]
        //public void HRAreaCodeTest()
        //{
        //    Position target = new Position();

        //    string val = null; // TODO: Assign to an appropriate value for the property

        //    target.HRAreaCode = val;


        //    Assert.AreEqual(val, target.HRAreaCode, "CAESDO.Recruitment.Core.Domain.Position.HRAreaCode was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for HREmail
        /////</summary>
        //[TestMethod()]
        //public void HREmailTest()
        //{
        //    Position target = new Position();

        //    string val = null; // TODO: Assign to an appropriate value for the property

        //    target.HREmail = val;


        //    Assert.AreEqual(val, target.HREmail, "CAESDO.Recruitment.Core.Domain.Position.HREmail was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for HRPhone
        /////</summary>
        //[TestMethod()]
        //public void HRPhoneTest()
        //{
        //    Position target = new Position();

        //    string val = null; // TODO: Assign to an appropriate value for the property

        //    target.HRPhone = val;


        //    Assert.AreEqual(val, target.HRPhone, "CAESDO.Recruitment.Core.Domain.Position.HRPhone was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for HRRep
        /////</summary>
        //[TestMethod()]
        //public void HRRepTest()
        //{
        //    Position target = new Position();

        //    string val = null; // TODO: Assign to an appropriate value for the property

        //    target.HRRep = val;


        //    Assert.AreEqual(val, target.HRRep, "CAESDO.Recruitment.Core.Domain.Position.HRRep was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for NumPublications
        /////</summary>
        //[TestMethod()]
        //public void NumPublicationsTest()
        //{
        //    Position target = new Position();

        //    int val = 0; // TODO: Assign to an appropriate value for the property

        //    target.NumPublications = val;


        //    Assert.AreEqual(val, target.NumPublications, "CAESDO.Recruitment.Core.Domain.Position.NumPublications was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for NumReferences
        /////</summary>
        //[TestMethod()]
        //public void NumReferencesTest()
        //{
        //    Position target = new Position();

        //    int val = 0; // TODO: Assign to an appropriate value for the property

        //    target.NumReferences = val;


        //    Assert.AreEqual(val, target.NumReferences, "CAESDO.Recruitment.Core.Domain.Position.NumReferences was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Position ()
        /////</summary>
        //[TestMethod()]
        //public void ConstructorTest()
        //{
        //    Position target = new Position();

        //    // TODO: Implement code to verify target
        //    Assert.Inconclusive("TODO: Implement code to verify target");
        //}

        ///// <summary>
        /////A test for PositionNumber
        /////</summary>
        //[TestMethod()]
        //public void PositionNumberTest()
        //{
        //    Position target = new Position();

        //    string val = null; // TODO: Assign to an appropriate value for the property

        //    target.PositionNumber = val;


        //    Assert.AreEqual(val, target.PositionNumber, "CAESDO.Recruitment.Core.Domain.Position.PositionNumber was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for PositionTitle
        /////</summary>
        //[TestMethod()]
        //public void PositionTitleTest()
        //{
        //    Position target = new Position();

        //    string val = null; // TODO: Assign to an appropriate value for the property

        //    target.PositionTitle = val;


        //    Assert.AreEqual(val, target.PositionTitle, "CAESDO.Recruitment.Core.Domain.Position.PositionTitle was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for ShortDescription
        /////</summary>
        //[TestMethod()]
        //public void ShortDescriptionTest()
        //{
        //    Position target = new Position();

        //    string val = null; // TODO: Assign to an appropriate value for the property

        //    target.ShortDescription = val;


        //    Assert.AreEqual(val, target.ShortDescription, "CAESDO.Recruitment.Core.Domain.Position.ShortDescription was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Vote
        /////</summary>
        //[TestMethod()]
        //public void VoteTest()
        //{
        //    Position target = new Position();

        //    bool val = false; // TODO: Assign to an appropriate value for the property

        //    target.Vote = val;


        //    Assert.AreEqual(val, target.Vote, "CAESDO.Recruitment.Core.Domain.Position.Vote was not set correctly.");
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
        #endregion

        [WorkItem(166), TestMethod()]
        public void ConstructorTest()
        {
            Position p = new Position();
            
            Assert.IsNotNull(p);
            Assert.IsTrue(p.IsTransient());
        }

        [WorkItem(166), TestMethod()]
        public void ValidateAll()
        {
            List<Position> pList = NHibernateHelper.daoFactory.GetPositionDao().GetAll();

            Assert.AreNotEqual<int>(0, pList.Count);

            foreach (Position p in pList)
            {
                this.TestContext.WriteLine("PositionID = {0}", p.ID);

                foreach (ValidationResult res in ValidateBO<Position>.GetValidationResults(p))
                {
                    this.TestContext.WriteLine("Key = {0}, Message = {1}", res.Key, res.Message);
                }

                Assert.IsTrue(ValidateBO<Position>.isValid(p));
            }
        }

        [WorkItem(166), TestMethod()]
        public void CreatePosition()
        {
            Position position = new Position();

            DateTime today = DateTime.Now;

            // Fill in all non-nullable values
            position.PositionTitle = "Test Wrangler";
            position.DatePosted = today;
            position.Deadline = today.AddDays(7);
            position.AllowApps = true;
            position.NumReferences = 0;
            position.NumPublications = 0;
            position.FacultyView = false;
            position.Vote = false;
            position.FinalVote = false;
            position.Closed = false;
            position.AdminAccepted = false;
            position.HRRep = StaticProperties.TestString;
            position.HREmail = StaticProperties.ExistingApplicantEmail;
            position.ReferenceTemplate = TemplateBLL.GetByID(1);
            position.DescriptionFile = FileBLL.GetByID(StaticProperties.ExistingFileID);
            
            position = Save(position);
            
            // Save PositionID for later retrieval
            StaticProperties.CreatedPositionID = position.ID;
            Assert.IsNotNull(StaticProperties.CreatedPositionID, "Position ID not saved! Warning, other tests will fail!");

            ValidateResults(position);
        }
        
        [WorkItem(166), TestMethod()]
        public void ReadPosition()
        {
            Position position = NHibernateHelper.daoFactory.GetPositionDao().GetById(StaticProperties.ExistingPositionID, false);

            Assert.IsNotNull(position);

            Assert.AreEqual<int>(StaticProperties.ExistingPositionID, position.ID); 

            ValidateResults(position);
        }

        [WorkItem(166), TestMethod()]
        public void DeletePosition()
        {
            Position position = NHibernateHelper.daoFactory.GetPositionDao().GetById(StaticProperties.ExistingPositionID, false);
            int positionID = position.ID;

            using (var ts = new TransactionScope())
            {
                NHibernateHelper.daoFactory.GetPositionDao().Delete(position);

                ts.CommitTransaction();
            }

            // Verify deletion
            bool isDeleted = false;
            
            try
            {
                Position positionDB = NHibernateHelper.daoFactory.GetPositionDao().GetById(positionID, false);
                positionDB.IsTransient(); //check to see if its in the db
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                isDeleted = true;
            }

            Assert.IsTrue(isDeleted);
        }

        #region Helper Methods

        private void ValidateResults(Position position)
        {
            this.TestContext.WriteLine("PositionID = {0}", position.ID);
            
            foreach (ValidationResult res in ValidateBO<Position>.GetValidationResults(position))
            {
                this.TestContext.WriteLine("Key = {0}, Message = {1}", res.Key, res.Message);
            }
            Assert.IsTrue(ValidateBO<Position>.isValid(position), position.getValidationResultsAsString(position));
        }

        private static Position Save(Position position)
        {
            // Validate before saving
            Assert.IsTrue(ValidateBO<Position>.isValid(position), "Test Wrangler not valid: " + position.getValidationResultsAsString(position));

            using (var ts = new TransactionScope())
            {
                position = NHibernateHelper.daoFactory.GetPositionDao().SaveOrUpdate(position);
            }

            // Verify save happened
            Assert.IsNotNull(position);
            Assert.IsFalse(position.IsTransient(), "Position not persisted by NHibernate to database");
            return position;
        }

        public override void LoadData()
        {
            base.LoadData();

            var templateType = new TemplateType {Type = StaticProperties.TestString};

            var template = new Template {TemplateText = StaticProperties.TestString, TemplateType = templateType};

            using (var ts = new TransactionScope())
            {
                GenericBLL<TemplateType, int>.EnsurePersistent(templateType);
                GenericBLL<Template, int>.EnsurePersistent(template);

                ts.CommitTransaction();
            }
        }

        #endregion
    }


}
