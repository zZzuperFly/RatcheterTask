using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Ratcheter;

namespace RatcheterTests
{
    [TestFixture]
    public class XmlHandlerIntegrationTests
    {
        [Test]
        public void LoadVerifyerParametersReturnTheExpectedList()
        {
            //Arrange
            XmlHandler handler = new XmlHandler();
            var list = handler.ReadVerifyerParametersFromFile("file");
            //Act

            //Assert
            Assert.AreEqual(1, list.Count());
            
        }
    }
}
