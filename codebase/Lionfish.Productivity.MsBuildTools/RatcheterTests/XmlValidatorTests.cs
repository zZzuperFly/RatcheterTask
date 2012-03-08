// created by: Östen Petersson 
// at: 10:18: 14/02: 2012
// ------------------------------------------------------------

using Microsoft.Build.Framework;
using Ratcheter;
using Rhino.Mocks;

namespace RatcheterTests
{
    using NUnit.Framework;
    using Rhino;

    [TestFixture]
    public class XmlValidatorTests
    {
        [Test]
        public void ASchemaIsValidated()
        {
            //Arrange
            var logProxy = MockRepository.GenerateStub<ILogProxy>();
            var xmlValidator = new XmlValidator(logProxy );
            var result = xmlValidator.ValidateXml(
                    @"..\..\..\Ratcheter\files\testFile.xml",
                    @"..\..\..\Ratcheter\files\testFile.xsd");

            Assert.IsTrue(result);
            logProxy.AssertWasNotCalled(x => x.LogThis(MessageImportance.High, ""));
        }
        //copy paste programming - just made one test so far - most likely a couple of bad paths in here to test

    }
}