﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
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
    ///This is a test class for CAESDO.Recruitment.Core.Domain.ChangeTracking and is intended
    ///to contain all CAESDO.Recruitment.Core.Domain.ChangeTracking Unit Tests
    ///</summary>
    [TestClass()]
    public class ChangeTrackingTest
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
        ///A test for ChangeTracking ()
        ///</summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            ChangeTracking target = new ChangeTracking();

            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void ValidateAllTest()
        {
            List<ChangeTracking> cList = NHibernateHelper.daoFactory.GetChangeTrackingDao().GetAll();

            Assert.AreNotEqual<int>(0, cList.Count);

            foreach (ChangeTracking c in cList)
            {
                this.TestContext.WriteLine("TrackingID = {0}", c.ID);

                foreach (ValidationResult res in ValidateBO<ChangeTracking>.GetValidationResults(c))
                {
                    this.TestContext.WriteLine("Key = {0}, Message = {1}", res.Key, res.Message);
                }

                Assert.IsTrue(ValidateBO<ChangeTracking>.isValid(c));
            }
        }

        [TestMethod]
        public void CascadeSaveTest()
        {
            ChangeTracking target = NHibernateHelper.daoFactory.GetChangeTrackingDao().GetById(StaticProperties.ExistingTrackingID, false);

            Assert.IsFalse(target.IsTransient());

            int numProperties = target.ChangedProperties.Count;

            //Now we'll add a few new properties to the target
            target.ChangedProperties.Add(new ChangedProperty(StaticProperties.TestString, StaticProperties.TestString, target));
            target.ChangedProperties.Add(new ChangedProperty(StaticProperties.TestString, StaticProperties.TestString, target));

            Assert.AreEqual<int>(numProperties + 2, target.ChangedProperties.Count); //make sure we have two new properties

            using (new NHibernateTransaction())
            {
                target = NHibernateHelper.daoFactory.GetChangeTrackingDao().SaveOrUpdate(target);
            }

            foreach (ChangedProperty prop in target.ChangedProperties)
            {
                this.TestContext.WriteLine("Changed Property ID = {0} created: Property = {1}, new value = {2}", prop.ID, prop.PropertyChanged, prop.PropertyChangedValue);
            }

            Assert.IsFalse(target.IsTransient());

            Assert.AreEqual<int>(numProperties + 2, target.ChangedProperties.Count); //make sure we STILL have two new properties

            //Delete the new properties
            target.ChangedProperties.RemoveAt(target.ChangedProperties.Count - 1);
            target.ChangedProperties.RemoveAt(target.ChangedProperties.Count - 1);
            
            using (new NHibernateTransaction())
            {
                target = NHibernateHelper.daoFactory.GetChangeTrackingDao().SaveOrUpdate(target);
            }

            Assert.AreEqual<int>(numProperties, target.ChangedProperties.Count); //make sure we are back at the previous number of properties
        }

        [TestMethod]
        public void SaveDeleteTest()
        {
            ChangeTracking tracking = new ChangeTracking();

            ChangeType ctype = NHibernateHelper.daoFactory.GetChangeTypeDao().GetById((int)ChangeTypes.Update, false);

            tracking.ObjectChanged = StaticProperties.TestString;
            tracking.ObjectChangedID = StaticProperties.ExistingProfileID;
            tracking.PropertyChanged = StaticProperties.TestString;
            tracking.PropertyChangedValue = StaticProperties.TestString;
            tracking.ChangeType = ctype;

            Assert.IsTrue(ValidateBO<ChangeTracking>.isValid(tracking), "Tracking Object Not Valid");

            Assert.IsTrue(tracking.IsTransient());

            using (new NHibernateTransaction())
            {
                tracking = NHibernateHelper.daoFactory.GetChangeTrackingDao().SaveOrUpdate(tracking);
            }

            Assert.IsFalse(tracking.IsTransient());

            ChangeTracking trackingDB = NHibernateHelper.daoFactory.GetChangeTrackingDao().GetById(tracking.ID, false);

            Assert.AreEqual<ChangeTracking>(tracking, trackingDB);

            this.TestContext.WriteLine("Tracking Created had ID = {0}", trackingDB.ID);

            using (new NHibernateTransaction())
            {
                NHibernateHelper.daoFactory.GetChangeTrackingDao().Delete(tracking);
            }

            ////Make sure it is deleted
            bool isDeleted = false;

            try
            {
                tracking = NHibernateHelper.daoFactory.GetChangeTrackingDao().GetById(trackingDB.ID, false);
                tracking.IsTransient();
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                isDeleted = true;
            }

            Assert.IsTrue(isDeleted);
        }

        /// <summary>
        ///A test for ChangeType
        ///</summary>
        [TestMethod()]
        public void ChangeTypeTest()
        {
            //ChangeTracking target = new ChangeTracking();

            //ChangeType val = null; // TODO: Assign to an appropriate value for the property

            //target.ChangeType = val;


            //Assert.AreEqual(val, target.ChangeType, "CAESDO.Recruitment.Core.Domain.ChangeTracking.ChangeType was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

    }


}
