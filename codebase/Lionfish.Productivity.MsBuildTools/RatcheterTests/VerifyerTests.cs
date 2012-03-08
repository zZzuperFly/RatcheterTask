using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        [Test, Ignore("not sure it is worth the added complexity")]
        public void WhenCurrentIsMuchBetterThanTargetItWillSuggestCurrentWith2TimesWarningAsBuffer()
        {
            //Arrange
            var logProxy = MockRepository.GenerateStrictMock<ILogProxy>();
            var verifyer = new Verifyer(logProxy);
            logProxy.Expect(x => x.LogThis(MessageImportance.Low, "Checking para"));
            logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Success: para -> Current is better than Target and can be ratcheted to 14"));
            var parameter = new Parameter("para", 10);
            var verifyerParameter = new VerifyerParameter("para", 20, 2, 2, RatchetingDirections.TowardsZero);
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
            logProxy.Expect(
                x =>
                x.LogThis(MessageImportance.Normal,
                          "Success: towards100 -> Current is better than Target and can be ratcheted to 95"));
            var parameter = new Parameter("towards100", 90);
            var verifyerParameters = CreateVerifyParameterList("towards100",85,10,0,RatchetingDirections.TowardsHundred );
            //Act
           List<OutputParameter> resultingList = ( List<OutputParameter>)verifyer.CheckDirectVsParameterList(parameter, verifyerParameters);
            var reesult = resultingList.FirstOrDefault(x => x.ParameterName == "towards100");
            Assert.AreEqual(true, reesult.IsOk );
            Assert.AreEqual("Success: towards100 -> Current is better than Target and can be ratcheted to 95", reesult.VeriferResult );
            
            
           // logProxy.VerifyAllExpectations();
        }

        [Test]
        public void CheckDirectVsFileWith5ItemsListReturnAnOutputlistwith1item()
        {
            //Arrange
            var logProxy = MockRepository.GenerateStub<ILogProxy>();
            var verifyer = new Verifyer(logProxy);
          
            var parameter = new Parameter("towards100", 90);
            var verifyerParameters = CreateVerifyParameterList("towards100", 85, 10, 0, RatchetingDirections.TowardsHundred);
            //Act
            List<OutputParameter> resultingList = (List<OutputParameter>)verifyer.CheckDirectVsParameterList(parameter, verifyerParameters);

            Assert.AreEqual(1, resultingList.Count );
            
        }

        [Test]
        public void FilevsFileWith5ItemsGeneratesAnOutputListWith5Items()
        {
            //Arrange
            var logProxy = MockRepository.GenerateStub<ILogProxy>();
            var verifyer = new Verifyer(logProxy);

            var parameters = CreateParameterList("towards100", 89);
            var verifyerParameters = CreateVerifyParameterList("towards100", 85, 10, 0, RatchetingDirections.TowardsHundred);
            //Act
            var resultingList = verifyer.CheckFileVsFile(parameters, verifyerParameters);
            var text = new StringBuilder().Append("resulting input: ");
            foreach (var outputParameter in resultingList)
            {
                text.Append(" \n *verifyerlog: " + outputParameter.VeriferResult );
            }
            Assert.AreEqual(5, resultingList.Count, text.ToString( ));
        }

        private List<Parameter> CreateParameterList()
        {
            return new List<Parameter>()
                       {
                           new Parameter("ett", 1),
                           new Parameter("två", 12),
                           new Parameter("tre", 77),
                           new Parameter("fyra", 7),

                       };

        }

        private List<Parameter> CreateParameterList(string specialParameterName, int currentValue)
        {
            var list =CreateParameterList();
            list.Add(new Parameter(specialParameterName, currentValue));
            return list;
        }

        

        private List<VerifyerParameter> CreateVerifyParameterList(string specialParameter, int targetValue, int ratchet, int warning, RatchetingDirections direction)
        {
            var list = CreateVerifyParameterList();
    
            var specPar = new VerifyerParameter(specialParameter, targetValue, ratchet, warning, direction);
            list.Add(specPar);
            return list;
        }

        private List<VerifyerParameter> CreateVerifyParameterList()
        {
            return new List<VerifyerParameter>()
                       {
                           new VerifyerParameter("ett", 3, 2, 3,RatchetingDirections.TowardsZero),
                           new VerifyerParameter("två", 10, 0, 0,RatchetingDirections.TowardsZero ),
                           new VerifyerParameter("tre", 4, 2, 2,RatchetingDirections.TowardsHundred ),
                           new VerifyerParameter("fyra", 4, 2, 2,RatchetingDirections.TowardsHundred ),


                       };

        }
    }
}
