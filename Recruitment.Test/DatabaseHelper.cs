using System;
using System.Collections.Generic;
using CAESDO.Recruitment.BLL;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Data;

namespace CAESDO.Recruitment.Test
{
    public static class DatabaseHelper
    {
        private static readonly string shortRandString = "qwerty";

        public static void LoadData()
        {
            using (var ts = new TransactionScope())
            {
                LoadLookupTypes();
                LoadDepartments();
                LoadPositions();
                LoadApplicants();
                LoadApplications();
                LoadUser();

                ts.CommitTransaction();
            }
        }
        
        private static void LoadApplicants()
        {
            for (int i = 0; i < 10; i++)
            {
                var profile = new Profile
                {
                    FirstName = string.Format("FName{0}", i),
                    LastName = string.Format("LName{0}", i),
                    Address1 = shortRandString,
                    City = shortRandString,
                    State = shortRandString
                };

                var applicant = new Applicant() { Email = string.Format("email{0}@fake.com", i), IsActive = true };

                profile.AssociatedApplicant = applicant;
                applicant.Profiles.Add(profile);

                GenericBLL<Applicant, int>.EnsurePersistent(applicant);
                GenericBLL<Profile, int>.EnsurePersistent(profile);
            }
        }

        private static void LoadApplications()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 5; j++) //5 applications per position
                {
                    var application = new Application()
                    {
                        AppliedPosition = GenericBLL<Position, int>.GetByID(i + 1),
                        AssociatedProfile = GenericBLL<Profile, int>.GetByID(j + 1),
                        LastUpdated = DateTime.Now,
                        Email = string.Format("email{0}@fake.com", j),
                        Submitted = i < 10 //First 10 were submitted
                    };

                    GenericBLL<Application, int>.EnsurePersistent(application);
                }
            }
        }

        private static void LoadDepartments()
        {
            string[] fiscodes = { "AANS", "ADNO", "APLS", "CHEM" };
            string[] schools = { "01", "01", "02", "03" };

            for (int i = 0; i < 4; i++)
            {
                var unit = new Unit(fiscodes[i]) { FullName = "Animal Science", SchoolCode = schools[i] };
                
                UnitBLL.EnsurePersistent(unit, true);
            }
        }

        private static void LoadLookupTypes()
        {
            for (int i = 0; i < 4; i++)
            {
                var templateType = new TemplateType() { Type = string.Format("type{0}", i) };
                var fileType = new FileType() { ApplicationFile = i % 2 == 0, FileTypeName = string.Format("type{0}", i) };

                GenericBLL<TemplateType, int>.EnsurePersistent(templateType);
                GenericBLL<FileType, int>.EnsurePersistent(fileType);
            }
        }

        private static void LoadPositions()
        {
            for (int i = 0; i < 20; i++) //Add 10 positions
            {
                var pos = new Position()
                {
                    AdminAccepted = i < 15,
                    Closed = i % 5 == 0,
                    AllowApps = i < 13,
                    PositionTitle = "Position Title",
                    HRRep = "HR Rep",
                    HREmail = "fake@fake.com",
                    HRPhone = "555-5555",
                    ReferenceTemplate = new Template()
                    {
                        TemplateText = "Sample Text",
                        TemplateType =
                            GenericBLL<TemplateType, int>.GetByID(1)
                    },
                    DescriptionFile = new File()
                    {
                        Description = "Some File",
                        FileName = "SomeFile",
                        FileType = GenericBLL<FileType, int>.GetByID(1)
                    }
                };

                pos.Departments = new List<Department>
                                      {
                                          new Department() {AssociatedPosition = pos, DepartmentFIS = "AANS", PrimaryDept = true},
                                          new Department() {AssociatedPosition = pos, DepartmentFIS = "ADNO", PrimaryDept = false}
                                      };

                if (i < 10)
                {
                    pos.Departments.Add(
                        new Department() { AssociatedPosition = pos, DepartmentFIS = "CHEM", PrimaryDept = false } //Add CHEM (school 3 to last 10 positions
                        );
                }

                if (i < 5 || i > 17)
                {
                    pos.Departments.Add(
                        new Department() { AssociatedPosition = pos, DepartmentFIS = "APLS", PrimaryDept = false });
                }

                var descriptionFile = pos.DescriptionFile;

                GenericBLL<File, int>.EnsurePersistent(descriptionFile);
                GenericBLL<Position, int>.EnsurePersistent(pos);
            }
        }

        private static void LoadUser()
        {
            //Let's put a user for testing
            var user = new User { FirstName = "Test", LastName = "User", LoginIDs = new List<Login>() };
            var userLogin = new Login() { User = user };
            EntityIdSetter.SetIdOf<string>(userLogin, "tester");

            user.LoginIDs.Add(userLogin);

            //Now associate this person with some units
            user.Units = new List<Unit>();

            var apls = UnitBLL.GetByID("APLS");
            var chem = UnitBLL.GetByID("CHEM");

            user.Units.Add(apls);
            user.Units.Add(chem);

            UserBLL.EnsurePersistent(user, true);
            GenericBLL<Login, string>.EnsurePersistent(userLogin, true);
        }

    }
}