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
        public void LoadParametersReturnTheExpectedList()
        {
            //Arrange
            XmlHandler handler = new XmlHandler();
            var list = handler.ReadParameterProjectsFromFile(@"..\..\..\Ratcheter\files\testFile.xml");
            //Act

            //Assert
            Assert.AreEqual(2, list.Count);
            
        }

        [Test]
        public void LoadVerifyerProjectsLoads3Projects()
        {
            //arrange
            XmlHandler handler = new XmlHandler();
            var list = handler.ReadVerifyerParametersFromFile(@"..\..\..\Ratcheter\files\targetvalues_test.xml");
            //act

            //assert
           
            Assert.AreEqual(3, list.Count);
            //just to make sure
            var result = list[1];
            Assert.AreEqual("porject2", result.ProjectName );
            Assert.AreEqual(RatchetingDirections.TowardsZero , result.VerifyerParameters[0].Direction );
        }

        [Test]
        public void WriteUpdatedVerifyerParametersToFileShouldWrite()
        {
            //arrange
            var handler=new XmlHandler();
            var startList = handler.ReadVerifyerParametersFromFile((@"..\..\..\Ratcheter\files\targetvalues_test.xml"));
            var old = startList[1].VerifyerParameters[0].TargetValue;
            startList[1].VerifyerParameters[0].TargetValue = 3; 
            //act
            handler.WriteUpdatedVerifyerParametersToFile(@"..\..\..\Ratcheter\files\targetvalues_back.xml", startList );
            //assert

            var resultList = handler.ReadVerifyerParametersFromFile(@"..\..\..\Ratcheter\files\targetvalues_back.xml");
            Assert.AreEqual(3, resultList[1].VerifyerParameters[0].TargetValue);
            Assert.AreNotEqual(old, resultList[1].VerifyerParameters[0].TargetValue);
            
        }
    }
}
