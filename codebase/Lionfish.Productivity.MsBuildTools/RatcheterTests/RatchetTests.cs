using Microsoft.Build.Framework;
using NUnit.Framework;
using Ratcheter;
using Rhino.Mocks;

namespace RatcheterTests
{
    [TestFixture]
    public class RatchetTests
    {
        private Ratchet _ratchet;

        [SetUp]
        public virtual void SetUp()
        {
            _ratchet = new Ratchet();
        }

        [Test]
        public void ARatchetShouldGoEitherTowardsZeroOrTowardsHundred()
        {
            //Arrange
          _ratchet .Direction = "towardsZero";
            //Act
            var expected = RatchetingDirections.TowardsZero;
            //Assert
            Assert.AreEqual(expected, _ratchet.RatchetingDirection);
        }

        [Test]
        public void CurrentvalueAndTargetValueIsNeeded()
        {
            //Arrange
            _ratchet .Direction = "towardsZero";
            _ratchet.CurrentValue = "2";
            _ratchet.TargetValue = "4";
            //Act
            int current = _ratchet.MyCurrentValue;
            int target = _ratchet.MyTargetValue;
            //assert
            Assert.AreEqual(2, current);
            Assert.AreEqual(4, target);
        }

        [Test]
        public void TheLoggerCanBeFaked()
        {
            //Arrange
            _ratchet.Direction = "towardsZero";
            _ratchet.InputType = "Direct";
            var logger = MockRepository.GenerateMock<ILogProxy >();
            _ratchet.MyLogger = logger;
            logger.Expect(x => x.LogThis(MessageImportance.Low, "ratchet: started!"));
            //Act
            _ratchet.Execute();
            //Assert
            logger.VerifyAllExpectations();
        }

        [Test]
        public void RatchetValueCanBeSet()
        {
            //Arrange
            _ratchet.RatchetValue = "2";
            int result = _ratchet.MyRatchetValue;
            Assert.AreEqual(2, result);
        }

        [Test]
        public void MyRatchetValueReturnZeroWhenInputIsEmpty()
        {
            //Arrange
            _ratchet.RatchetValue = "";
            int result = _ratchet.MyRatchetValue;
            Assert.AreEqual(0, result);
        }
        
        [Test]
        public void WarningValueCanBeSet()
        {
            //Arrange
            _ratchet.WarningValue = "1";
            int result = _ratchet.MyWarningValue;
            Assert.AreEqual(1, result);
        }
        
        [Test]
        public void MyWarningValueReturnZeroWhenInputIsEmpty()
        {
            //Arrange
            _ratchet.WarningValue = "";
            int result = _ratchet.MyWarningValue;
            Assert.AreEqual(0, result);
        }

        [Test]
        public void InputTypeCanBeSet()
        {
            //Arrange
            _ratchet.InputType = "direct";
            InputTypes result = _ratchet.MyInputType;
            //Act

            //Assert
        }

        [Test,Ignore]
        public void XmlHandlingCanBeFaked()
        {
            //Arrange
            _ratchet.Direction = "towardsZero";
            _ratchet.InputType = "DirectVsFile";
            var xmlHnadler = MockRepository.GenerateStub<IXmlHandler>();
            var logger = MockRepository.GenerateMock<ILogProxy >();
            //Act
            _ratchet.MyLogger = logger;
            _ratchet.MyXmlHandler = xmlHnadler;
            //Assert

        }
    
    }

}
