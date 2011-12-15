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
            logProxy.Expect( x => x.LogThis(MessageImportance.Normal, "Success: Current is better than Target"));
            //Assert
            verifyer.CheckDirectInput(5, 1,3);

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
                          "Warning: Current better than target but within warning - you are close to fail"));
            //Assert
            verifyer.CheckDirectInput(4, 1, 3);

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
                          "Fail: Current value is worse than target"));
            //Assert
            verifyer.CheckDirectInput(1, 4, 3);

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsFailIfCurrentValueIsHigherThanTargetAndDirectionTowardsZero()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsZero);
            //Act
            logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Fail: Current value is worse than target"));
            //Assert
            verifyer.CheckDirectInput(10, 5, 3);

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsSuccessIfCurrentValueIslowerThanTargetPlusWarningAndDirectionTowardsZero()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy, RatchetingDirections.TowardsZero);
            //Act
            logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Success: Current is better than Target"));
            //Assert
            verifyer.CheckDirectInput(1, 5, 3);

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
                          "Warning: Current better than target but within warning - you are close to fail"));
            //Assert
            verifyer.CheckDirectInput(1, 4, 3);

            logProxy.VerifyAllExpectations();
        }
    }


    [TestFixture]
    public class CheckerTests
    {
        [Test]
        public void TowardsHUndredAndCheckerReturnsTrueIfCurrentIsHigherThanTarget()
        {
            //Arrange
            var towardsHundredChecker = new TowardsHundredChecker();

            //Act
            var result = towardsHundredChecker.CurrentValueIsBetterThanTargetValue(4, 2);
            //Assert
            Assert.IsTrue(result);

        }

        [Test]
        public void TowardsHUndredAndCheckerReturnsTrueIfCurrentIsEqualToTarget()
        {
            //Arrange
            var towardsHundredChecker = new TowardsHundredChecker();

            //Act
            var result = towardsHundredChecker.CurrentValueIsEqualToTargetValue(4, 4);
            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TowardsHundredCheckerReturnsTrueIfcurrentIsmorehanTargetplusWarning()
        {
            //Arrange
            var towardsHundredChecker = new TowardsHundredChecker();
            //Act
            var result = towardsHundredChecker.CurrentValueIsBetterThanTargetValueAndWarning(10, 5, 3);
            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TowardsHundredCheckerReturnsfalseIfcurrentIsLessThanTargetplusWarning()
        {
            //Arrange
            var towardsHundredChecker = new TowardsHundredChecker();
            //Act
            var result = towardsHundredChecker.CurrentValueIsBetterThanTargetValueAndWarning(7, 5, 3);
            //Assert
            Assert.IsFalse(result);
        }


        [Test]
        public void TowardsZeroCheckerreturnsTrueWhenCurrentIsLowerThanTarget()
        {
            //Arrange
            var towardsZeroChecker = new TowardsZeroChecker();
            //Act
            var result = towardsZeroChecker.CurrentValueIsBetterThanTargetValue(2, 4);
            //Assert
            Assert.IsTrue(result);

        }

        [Test]
        public void TowardsZeroCheckerreturnsTrueWhenCurrentIsEqualToTarget()
        {
            //Arrange
            var towardsZeroChecker = new TowardsZeroChecker();
            //Act
            var result = towardsZeroChecker.CurrentValueIsEqualToTargetValue(2, 2);
            //Assert
            Assert.IsTrue(result);

        }

        [Test]
        public void TowardsZeroCheckerReturnsTrueIfcurrentIsLessThanTargetMinusWarning()
        {
            //Arrange
            var towardsZeroChecker = new TowardsZeroChecker();
            //Act
            var result = towardsZeroChecker.CurrentValueIsBetterThanTargetValueAndWarning(5, 10, 3);
            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TowardsZeroCheckerReturnsfalseIfcurrentIsMoreThanTargetMinusWarning()
        {
            //Arrange
            var towardsZeroChecker = new TowardsZeroChecker();
            //Act
            var result = towardsZeroChecker.CurrentValueIsBetterThanTargetValueAndWarning(5, 7, 3);
            //Assert
            Assert.IsFalse(result);
        }



    }
}
