using System;
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Abstractions;
using CAESDO.Recruitment.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CAESDO.Recruitment.Test.BusinessTests
{
    [TestClass]
    public class TemplateProcessingTests
    {
        private Reference reference;
        private Application application;

        [TestMethod]
        public void CanProcessTemplateByReplacingRecruitmentAdmin()
        {
            const string templateBody = "Who is the {RecruitmentAdminName}";

            var processing = new TemplateProcessing();

            var templateResult = processing.ProcessTemplate(reference, application, templateBody);

            var templateReplace = templateResult.Replace("{RecruitmentAdminName}", application.AppliedPosition.HRRep);

            Assert.IsNotNull(templateResult);
            Assert.AreEqual(templateReplace, templateResult);
        }

        [TestMethod]
        public void CanProcessTemplateByReplacingReferenceName()
        {
            const string templateBody = "Who is the {ReferenceName}";

            var processing = new TemplateProcessing();

            var templateResult = processing.ProcessTemplate(reference, application, templateBody);

            var templateReplace = templateResult.Replace("{ReferenceName}", reference.FullName);

            Assert.IsNotNull(templateResult);
            Assert.AreEqual(templateReplace, templateResult);
        }

        [TestMethod]
        public void CanProcessTemplateWithOnlyToken()
        {
            const string templateBody = "{ReferenceName}";

            var processing = new TemplateProcessing();

            var templateResult = processing.ProcessTemplate(reference, application, templateBody);

            var templateReplace = templateResult.Replace("{ReferenceName}", reference.FullName);

            Assert.IsNotNull(templateResult);
            Assert.AreEqual(templateReplace, templateResult);
        }

        [TestInitialize]
        public void SetupProperties()
        {
            var previewDepartment = new Department
            {
                DepartmentFIS = "CHAN",
                PrimaryDept = true,
                Unit = new Unit { FullName = "Advanced Sciences" }
            };

            var previewReference = new Reference { FirstName = "Mike", MiddleName = "H", LastName = "Jones", Title = "Dr." };

            var previewApplication = new Application
            {
                AssociatedProfile =
                    new Profile { FirstName = "John", MiddleName = "P", LastName = "Smith" }
            };

            previewApplication.AppliedPosition = new Position
            {
                Deadline = DateTime.Now.AddDays(20),
                PositionTitle = "Professor of Technology",
                HRRep = "Jane Williams",
                HREmail = "jwilliams@ucdavis.edu",
                Departments = new List<Department> { previewDepartment }
            };

            EntityIdSetter.SetIdOf<int>(previewApplication.AppliedPosition, 1);

            reference = previewReference;
            application = previewApplication;
        }
    }
}
