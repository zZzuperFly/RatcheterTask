using Microsoft.Build.Framework;
using NUnit.Framework;
using Ratcheter;
using Rhino.Mocks;

namespace RatcheterTests
{
    [TestFixture]
    public class VerifyerTests
    {

        [Test]
        public void DirectionAndLoggerIsKnownAndDirectionClassIsChosen()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsHundred);
            //Act
            Assert.IsInstanceOf<TowardsHundredChecker>(verifyer.Checker);

            //Assert
        }

        [Test]
        public void CheckDirectInputLogsAsSuccessIfCurrentValueIsHigherThanTargetPlusWarningAndDirectionTowardsHundred()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsHundred);
            //Act
            logProxy.Expect( x => x.LogThis(MessageImportance.Normal, "Success: para -> Current is better than Target"));
            var parameter = new Parameter("para", 6);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 0);
            //Assert
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsSuccessIfCurrentValueIsHigherThanTargetbutnotWarningAndDirectionTowardsHundred()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsHundred);
            //Act
            logProxy.Expect(
                x =>
                x.LogThis(MessageImportance.Normal,
                          "Warning: para -> Current better than target but within warning - you are close to fail"));
            //Assert
            var parameter = new Parameter("para", 6);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2);
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsFailIfCurrentValueIslowerThanTargetAndDirectionTowardsHundred()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsHundred);
            //Act
            logProxy.Expect(
                x =>
                x.LogThis(MessageImportance.Normal,
                          "Fail: para -> Current value is worse than target"));
            var parameter = new Parameter("para", 4);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2);
            //Assert
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsFailIfCurrentValueIsHigherThanTargetAndDirectionTowardsZero()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsZero);
            //Act
            logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Fail: para -> Current value is worse than target"));
            var parameter = new Parameter("para", 6);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2);
            //Assert
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsSuccessIfCurrentValueIslowerThanTargetPlusWarningAndDirectionTowardsZero()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsZero);
            //Act
            logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Success: para -> Current is better than Target"));
            var parameter = new Parameter("para", 1);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2);
            //Assert
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsSuccessIfCurrentValueIslowerThanTargetbutnotWarningAndDirectionTowardsZero()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsZero);
            //Act
            logProxy.Expect(
                x =>
                x.LogThis(MessageImportance.Normal,
                          "Warning: para -> Current better than target but within warning - you are close to fail"));
            var parameter = new Parameter("para", 4);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2);
            //Assert
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test,Ignore]
        public void CheckDirectVsFileInputTakesAListOfParametersAndAParameterObjectAndStartsVerify()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsZero);
            //Act

            //Assert
        }
    }
}
