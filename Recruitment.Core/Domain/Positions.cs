using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.DataInterfaces;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Positions : DomainObject<int>
    {
        #region Constructors
        public Positions()
        {

        }

        public Positions(string positionNumber)
        {
            PositionNumber = positionNumber;
        } 
        #endregion

        #region Properties

        public int PositionID
        {
            get { return ID; }
        }

        private string _PositionTitle;

        public string PositionTitle
        {
            get { return _PositionTitle; }
            set { _PositionTitle = value; }
        }

        private string _PositionNumber;

        public string PositionNumber
        {
            get { return _PositionNumber; }
            set { _PositionNumber = value; }
        }


        private DateTime _DatePosted;

        public DateTime DatePosted
        {
            get { return _DatePosted; }
            set { _DatePosted = value; }
        }

        private DateTime _Deadline;

        public DateTime Deadline
        {
            get { return _Deadline; }
            set { _Deadline = value; }
        }

        private bool _AllowApps;

        public bool AllowApps
        {
            get { return _AllowApps; }
            set { _AllowApps = value; }
        }

        private int _NumReferences;

        public int NumReferences
        {
            get { return _NumReferences; }
            set { _NumReferences = value; }
        }

        private int _NumPublications;

        public int NumPublications
        {
            get { return _NumPublications; }
            set { _NumPublications = value; }
        }

        private bool _CommitteeView;

        public bool CommitteeView
        {
            get { return _CommitteeView; }
            set { _CommitteeView = value; }
        }

        private bool _FacultyView;

        public bool FacultyView
        {
            get { return _FacultyView; }
            set { _FacultyView = value; }
        }

        private bool _Vote;

        public bool Vote
        {
            get { return _Vote; }
            set { _Vote = value; }
        }

        private bool _FinalVote;

        public bool FinalVote
        {
            get { return _FinalVote; }
            set { _FinalVote = value; }
        }

        private IList<Applications> _AssociatedApplications;

        public IList<Applications> AssociatedApplications
        {
            get { return _AssociatedApplications; }
            set { _AssociatedApplications = value; }
        }

        /// <summary>
        /// Like the following, but for Customers
        /// Provides an accessor for injecting an IOrderDao so that this class does 
        /// not have to create one itself.  Can be set from a controller, using 
        /// IoC, or from another business object.  As a rule-of-thumb, I do not like
        /// domain objects to use DAOs directly; but there are exceptional cases; 
        /// therefore, this shows a way to do it without having a concrete dependency on the DAO.
        /// </summary>
        public IApplicationsDao ApplicationsDao
        {
            get
            {
                if (_ApplicationsDao == null)
                {
                    throw new MemberAccessException("OrderDao has not yet been initialized");
                }

                return _ApplicationsDao;
            }
            set
            {
                _ApplicationsDao = value;
            }
        }

        private IApplicationsDao _ApplicationsDao;

        #endregion

        /// <summary>
        /// Gets applications that are submitted for this postition
        /// </summary>
        /// <returns></returns>
        public List<Applications> GetSubmittedApplications()
        {
            Applications app = new Applications();

            app.Submitted = true;

            List<Applications> matchingApplications = ApplicationsDao.GetByExample(app);
            List<Applications> matchingApplicationsForCurrentPosition = new List<Applications>();

            foreach (Applications application in matchingApplications)
            {
                if (app.AppliedPosition.ID == ID)
                {
                    matchingApplicationsForCurrentPosition.Add(application);
                }
            }

            return matchingApplicationsForCurrentPosition;
        }
    }
}
