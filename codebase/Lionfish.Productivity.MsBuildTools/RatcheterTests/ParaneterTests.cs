using NUnit.Framework;
using Ratcheter;

namespace RatcheterTests
{
    [TestFixture]
    public class ParaneterTests
    {
        [Test]
        public void AParameterIsADirectComparerObject()
        {
            //Arrange
            var para = new Parameter("name", 5,4,3,1);
            //Act
            Assert.AreEqual("name", para.ParameterName );
            
            //Assert
        }
    }
}
