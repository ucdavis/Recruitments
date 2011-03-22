using System;
using CAESDO.Recruitment.BLL;
using CAESDO.Recruitment.Core.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Principal;
using System.Linq;

namespace CAESDO.Recruitment.Test.BusinessTests
{
    [TestClass]
    public class MessagingTests : DatabaseTestBase
    {
        [TestMethod]
        public void CanSendMessage()
        {
            //Mock up an iprincipal
            SetupMessageGateway("testtest", true);

            var success = MessageBLL.SendMessage("from", "to", "subject", "body");

            Assert.IsTrue(success);

            //Now let's see if there was a tracking message inserted

            //Find the messagetracking associated with the to address
            var mtracking = MessageTrackingBLL.EntitySet.Where(m => m.To == "to");

            Assert.IsTrue(mtracking.Count() == 1);
            Assert.AreEqual("testtest", mtracking.First().SentBy);
        }

        [TestMethod]
        public void SendMessageCanFail()
        {
            //Mock up an iprincipal
            SetupMessageGateway("testtest", false);

            var success = MessageBLL.SendMessage("from", "to", "subject", "body");

            Assert.IsFalse(success);

            //Now let's see if there was a tracking message inserted

            //There shouldn't be a messagetracking associated with the to address
            Assert.AreEqual(0, MessageTrackingBLL.EntitySet.Where(m => m.To == "to").Count());
        }

        private static void SetupMessageGateway(string username, bool succeed)
        {
            var principal = new Mock<IPrincipal>();
            principal.Setup(d => d.Identity.Name).Returns(username);

            //Mock up the email gateway to succeed
            var gateway = new Mock<IMessageGateway>();
            
            var setup = gateway.Setup(
                d =>
                d.SendMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            if (succeed == false) setup.Throws(new Exception());

            MessageBLL.MessageGateway = gateway.Object;
            MessageBLL.UserContext = principal.Object;
        }
    }
}
