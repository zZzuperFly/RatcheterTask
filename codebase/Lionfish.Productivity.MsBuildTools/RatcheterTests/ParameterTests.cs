using NUnit.Framework;
using Ratcheter;

namespace RatcheterTests
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void AParameterIsADirectComparerObject()
        {
            //Arrange
            var name = "name";
            var currentValue = 5;
            var para = new Parameter("name", 5);
            //Act
            Assert.AreEqual(name , para.ParameterName );
            Assert.AreEqual(currentValue, para.CurrentValue);
        }


        [Test]
        public void AVerifyerParameterIsTheVerifyerValues()
        {
            //Arrange
            var name = "name";
            var targetValue = 3;
            var warning = 2;
            var ratchet = 1;
            var verpara = new VerifyerParameter(name, targetValue, ratchet, warning,RatchetingDirections.TowardsHundred );
            //Act
            Assert.AreEqual(name , verpara.ParameterName );
            Assert.AreEqual(targetValue , verpara.TargetValue );
            Assert.AreEqual(warning, verpara.WarningValue );
            Assert.AreEqual(ratchet, verpara.RatchetValue);
            Assert.AreEqual(RatchetingDirections.TowardsHundred , verpara.Direction );
            
            
            //Assert
        }
    }
}
