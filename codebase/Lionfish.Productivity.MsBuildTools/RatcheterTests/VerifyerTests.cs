using System.Collections.Generic;
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
        public void CheckDirectInputLogsAsSuccessIfCurrentValueIsHigherThanTargetPlusWarningAndDirectionTowardsHundred()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);
            //Act
            logProxy.Expect( x => x.LogThis(MessageImportance.Normal, "Success: para -> Current is better than Target"));
            var parameter = new Parameter("para", 6);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 0,RatchetingDirections.TowardsHundred );
            //Assert
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsSuccessIfCurrentValueIsHigherThanTargetbutnotWarningAndDirectionTowardsHundred()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);
            //Act
            logProxy.Expect(
                x =>
                x.LogThis(MessageImportance.Normal,
                          "Warning: para -> Current better than target but within warning - you are close to fail"));
            //Assert
            var parameter = new Parameter("para", 6);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2, RatchetingDirections.TowardsHundred);
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsFailIfCurrentValueIslowerThanTargetAndDirectionTowardsHundred()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);
            //Act
            logProxy.Expect(
                x =>
                x.LogThis(MessageImportance.Normal,
                          "Fail: para -> Current value is worse than target"));
            var parameter = new Parameter("para", 4);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2, RatchetingDirections.TowardsHundred);
            //Assert
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsFailIfCurrentValueIsHigherThanTargetAndDirectionTowardsZero()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);
            //Act
            logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Fail: para -> Current value is worse than target"));
            var parameter = new Parameter("para", 6);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2, RatchetingDirections.TowardsZero);
            //Assert
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsSuccessIfCurrentValueIslowerThanTargetPlusWarningAndDirectionTowardsZero()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);

            logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Success: para -> Current is better than Target"));
            var parameter = new Parameter("para", 1);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2, RatchetingDirections.TowardsZero);
            //Act
            verifyer.CheckDirectInput(parameter, verifyerParameter);
            //Assert
            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectInputLogsAsSuccessIfCurrentValueIslowerThanTargetbutnotWarningAndDirectionTowardsZero()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);
            //Act
            logProxy.Expect(
                x =>
                x.LogThis(MessageImportance.Normal,
                          "Warning: para -> Current better than target but within warning - you are close to fail"));
            var parameter = new Parameter("para", 4);
            var verifyerParameter = new VerifyerParameter("para", 5, 0, 2, RatchetingDirections.TowardsZero);
            //Assert
            verifyer.CheckDirectInput(parameter,verifyerParameter );

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirektInputLogsAsRatchetableWhenSoIsTheCase()
        {
            // since my take around direct input is that you usually don´t use xmlfiles 
            // i want it to just suggest the new value in the log and let the user handle that as he wants


            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);

            logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Success: para -> Current is better than Target and can be ratcheted to 3"));
            var parameter = new Parameter("para", 1);
            var verifyerParameter = new VerifyerParameter("para", 5, 2, 2, RatchetingDirections.TowardsZero);
            //Act
            verifyer.CheckDirectInput(parameter, verifyerParameter);
            //Assert

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirektInputWillNotSuggestRatchetBelowZero()
        {


            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);

            logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Success: para -> Current is better than Target and can be ratcheted to 0"));
            var parameter = new Parameter("para", 1);
            var verifyerParameter = new VerifyerParameter("para", 5, 7, 2, RatchetingDirections.TowardsZero);
            //Act
            verifyer.CheckDirectInput(parameter, verifyerParameter);
            //Assert

            logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectVsFileInputTakesAListOfVerifyParametersAndADirectInputParameterAndStartsVerify()
        {
            //Arrange
            var logProxy = MockRepository.GenerateMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);

            //Act

            //Assert
        }

        private List<VerifyerParameter> CreateVerifyParameterList()
        {
            return new List<VerifyerParameter>()
                       {
                           new VerifyerParameter("ett", 1, 2, 3,RatchetingDirections.TowardsZero),
                           new VerifyerParameter("två", 2, 0, 0,RatchetingDirections.TowardsZero ),
                           new VerifyerParameter("tre", 4, 2, 2,RatchetingDirections.TowardsHundred )

                       };
        }
    }
}
