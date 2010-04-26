using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public interface IApplicationStep
    {
        ApplicationStepType ApplicationStepType
        {
            get;
            set;
        }

        bool isComplete();
    }
}
