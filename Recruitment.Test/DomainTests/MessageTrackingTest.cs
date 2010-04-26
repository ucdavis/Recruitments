using CAESDO.Recruitment.BLL;
using CAESDO.Recruitment.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace CAESDO.Recruitment.Test.DomainTests
{
    [TestClass]
    public class MessageTrackingTest : DatabaseTestBase
    {
        [TestMethod]
        public void CanSaveMessageTrackingEntry()
        {
            var tracking = new MessageTracking
                               {
                                   From = StaticProperties.TestString,
                                   To = StaticProperties.TestString,
                                   SentBy = StaticProperties.TestString,
                                   Body = StaticProperties.TestString,
                                   DateSent = DateTime.Now
                               };

            using (var ts = new TransactionScope())
            {
                GenericBLL<MessageTracking, int>.EnsurePersistent(tracking);
   
                ts.CommitTransaction();
            }
        }
    }
}
