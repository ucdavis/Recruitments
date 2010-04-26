using System;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class MessageTracking : DomainObject<MessageTracking,int>
    {
        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string To { get; set; }

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string From { get; set; }

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string SentBy { get; set; }

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string Body { get; set; }

        [NotNullValidator] //always not null since its a datetime
        public virtual DateTime DateSent { get; set; }

    }
}
