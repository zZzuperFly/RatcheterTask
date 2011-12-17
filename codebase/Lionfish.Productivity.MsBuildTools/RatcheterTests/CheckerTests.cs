using NUnit.Framework;
using Ratcheter;

namespace RatcheterTests
{
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