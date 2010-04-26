using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public interface IApplicationStep
    {
        int ApplicationID
        {
            get;
            set;
        }

        ApplicationStepType ApplicationStepType
        {
            get;
            set;
        }

        bool isComplete();

        void Fill();

        void Save();
    }
}
