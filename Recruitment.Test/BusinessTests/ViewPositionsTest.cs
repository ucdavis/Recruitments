using System.Collections.Generic;
using CAESDO.Recruitment.BLL;
using CAESDO.Recruitment.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CAESDO.Recruitment.Test.BusinessTests
{
    [TestClass]
    public class ViewPositionsTest : DatabaseTestBase
    {
        //<asp:Parameter DefaultValue="false" Name="Closed" Type="Boolean" />
        //<asp:Parameter DefaultValue="true" Name="AdminAccepted" Type="Boolean" />
        //<asp:Parameter DefaultValue="true" Name="AllowApplications" Type="Boolean" />
        private const bool Closed = false;
        private const bool AdminAccepted = true;
        private const bool AllowApplications = true;

        [TestMethod]
        public void ViewPositions()
        {
            var result = PositionBLL.GetByStatusAndDepartment(Closed, AdminAccepted, AllowApplications, null, null);

            Assert.IsNotNull(result); //First just see if we can get back something
        }

        [TestMethod]
        public void CanViewPositionsFilteredBySchool()
        {
            string schoolCode = "02";

            var positions = PositionBLL.GetByStatusAndDepartment(Closed, AdminAccepted, AllowApplications, null, schoolCode); //controller.ViewPositions("School", schoolCode) as ViewResult;

            Assert.IsNotNull(positions);
            Assert.AreEqual(4, positions.Count);

            foreach (var position in positions)
            {
                bool hasSchool = false;

                foreach (var dept in position.Departments)
                {
                    var unit = UnitBLL.GetByID(dept.DepartmentFIS);

                    if (unit.SchoolCode == schoolCode) hasSchool = true;
                }

                Assert.IsTrue(hasSchool, string.Format("Position {0} is not in school {1}", position.ID, schoolCode));
            }
        }

        [TestMethod]
        public void CanViewPositionsFilteredByUnit()
        {
            string unit = "CHEM";

            var positions = PositionBLL.GetByStatusAndDepartment(Closed, AdminAccepted, AllowApplications, unit, null); //controller.ViewPositions("Unit", unit) as ViewResult;

            Assert.IsNotNull(positions);
            Assert.AreEqual(8, positions.Count);

            foreach (var position in positions) //Make sure each one retrieved has APLS in the list of depts
            {
                var exDept = new Department() { DepartmentFIS = unit };
                Assert.IsTrue(position.Departments.Contains(exDept));
            }
        }

        [TestMethod]
        public void ViewPositionsOpenAndAdminAcceptedAndAllowApps()
        {
            var positions = PositionBLL.GetByStatusAndDepartment(Closed, AdminAccepted, AllowApplications, null, null);
            
            Assert.IsNotNull(positions);
            Assert.AreEqual(10, positions.Count);//there should be 10 positions that meet the criteria

            //Make sure those four positions do meet the criteria
            foreach (var position in positions)
            {
                Assert.IsTrue(position.AdminAccepted);
                Assert.IsTrue(position.AllowApps);
                Assert.IsFalse(position.Closed);
            }
        }
    }
}
