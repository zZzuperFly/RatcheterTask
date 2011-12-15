using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Ratcheter;
using Rhino.Mocks;
using TechTalk.SpecFlow;

namespace RatcheterSpecifications
{
    [Binding]
    public class SimpleSteps
    {
        private Ratchet _ratchet;
        private ILogProxy _logProxy;
        public SimpleSteps()
        {
            _ratchet = new Ratchet();
            _logProxy  = MockRepository.GenerateMock<ILogProxy>();
            _ratchet.MyLogger = _logProxy;
            _ratchet.InputType = "direct";
        }
     
        [Given(@"that the parameter is set to towardsZero")]
        public void GivenThatTheParameterIsSetToTowardsZero()
        {
            
            _ratchet.Direction = "towardsZero";
        }

            [Given(@"Current input is lower than Target")]
        public void GivenCurrentInputIsLowerThanTarget()
            {
                int target = 10;
                int current = target - 2;
                _ratchet.TargetValue = 10.ToString( );
                _ratchet.CurrentValue =current.ToString();
            }

        [When(@"I run the application")]
        public void WhenIRunTheApplication()
        {
            _logProxy.Expect(x => x.LogThis(MessageImportance.Normal, "Success: Current is better than Target"));
            _ratchet.Execute();
        }

        [Then(@"it logs to output as success")]
        public void ThenItLogsToOutputAsSuccess()
        {
            _logProxy.VerifyAllExpectations();
        }




    }
}
