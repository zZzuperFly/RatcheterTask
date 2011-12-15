﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.239
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace RatcheterSpecifications
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Simple Ratchetn Example")]
    public partial class SimpleRatchetnExampleFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SimpleRatchet.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Simple Ratchetn Example", "As a start for the ratcheter \r\nI want to be able to send in all relevant numbers " +
                    "on the commandline \r\nand see the result written to the output", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Current value is lower than Target and is ratcheted")]
        public virtual void CurrentValueIsLowerThanTargetAndIsRatcheted()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Current value is lower than Target and is ratcheted", ((string[])(null)));
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given("that the parameter is set to towardsZero");
#line 10
 testRunner.And("Current input is lower than Target");
#line 11
 testRunner.And("Current plus warning is lower than Target");
#line 12
 testRunner.When("I run the application");
#line 13
 testRunner.Then("it logs to output as success");
#line 14
 testRunner.And("ratchet the current");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion