﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using CAESDO.Recruitment.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;
namespace CAESDO.Recruitment.Test
{
    /// <summary>
    ///This is a test class for CAESDO.Recruitment.Core.Domain.User and is intended
    ///to contain all CAESDO.Recruitment.Core.Domain.User Unit Tests
    ///</summary>
    [TestClass()]
    public class UserTest : DatabaseTestBase
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

        /*
        /// <summary>
        ///A test for LoginIDs
        ///</summary>
        [TestMethod()]
        public void LoginIDsTest()
        {
            //User target = new User();

            //System.Collections.Generic.IList<CAESDO.Recruitment.Core.Domain.Login> val = null; // TODO: Assign to an appropriate value for the property

            //target.LoginIDs = val;


            //Assert.AreEqual(val, target.LoginIDs, "CAESDO.Recruitment.Core.Domain.User.LoginIDs was not set correctly.");
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
         */

        [TestMethod]
        public void GetUserByLogin()
        {
            User target = NHibernateHelper.daoFactory.GetUserDao().GetUserByLogin(StaticProperties.TestString);

            Assert.IsNotNull(target);

            this.TestContext.WriteLine("{0} {1} has the EmployeeID {2} and {3} loginIDs", target.FirstName, target.LastName, target.EmployeeID, target.LoginIDs.Count);

            Assert.AreEqual<string>(StaticProperties.TestString, target.FirstName);
            Assert.AreEqual<string>(StaticProperties.TestString, target.LastName);
        }

        /// <summary>
        ///A test for User ()
        ///</summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            User target = new User();

            Assert.IsNotNull(target);
        }

        public override void LoadData()
        {
            base.LoadData();

            Login login = new Login();
            EntityIdSetter.SetIdOf<string>(login,StaticProperties.TestString);

            //Load in a user
            User user = new User();
            user.FirstName = StaticProperties.TestString;
            user.LastName = StaticProperties.TestString;
            user.LoginIDs = new List<Login> {login};

            login.User = user;

            using (var ts = new TransactionScope())
            {
                UserBLL.EnsurePersistent(ref user);
                GenericBLL<Login, string>.EnsurePersistent(ref login, true);
                
                ts.CommitTransaction();
            }
        }

    }


}
