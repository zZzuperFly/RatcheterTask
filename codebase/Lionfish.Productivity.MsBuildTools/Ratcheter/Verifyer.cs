using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace Ratcheter
{
    public class Verifyer
    {
        private ILogProxy _logProxy;
        private IChecker _checker;

        public Verifyer(ILogProxy logProxy)
        {
            _logProxy = logProxy;

    
        }


        public IChecker Checker
        {
            get { return _checker; }
        }

        public bool CheckDirectInput(Parameter parameter, VerifyerParameter verifyerParameter)
        {

            if (parameter.ParameterName == verifyerParameter.ParameterName)
            {
                var result = CheckInput(parameter, verifyerParameter);
                _logProxy.LogThis(MessageImportance.Normal, result.VeriferResult);
                return result.IsOk;
            }
            _logProxy.LogThis(MessageImportance.Normal,
                              string.Format("Warning: no comparison due to differing parameternames {0} <-> {1}",
                                            parameter.ParameterName, verifyerParameter.ParameterName));
            return false;
        }

        private OutputParameter CheckInput(Parameter parameter, VerifyerParameter verifyerParameter)
        {
            if (verifyerParameter.Direction  == RatchetingDirections.TowardsZero)
            {
                _checker = new TowardsZeroChecker();
            }
            else
            {
                _checker = new TowardsHundredChecker();
            }
            _logProxy.LogThis(MessageImportance.Low, string.Format("Checking {0}", parameter.ParameterName));
            var par = new OutputParameter(parameter.ParameterName, "", false);

            if (Checker.CurrentValueIsWorseThanTargetValue(parameter.CurrentValue, verifyerParameter.TargetValue))
            {

                par.IsOk = false;
                par.VeriferResult = string.Format("Fail: {0} -> Current value is worse than target",
                                                  parameter.ParameterName);
                return par;
            }
            if (Checker.CurrentValueIsEqualToTargetValue(parameter.CurrentValue, verifyerParameter.TargetValue))
            {

                par.IsOk = true;
                par.VeriferResult = string.Format(
                    "Warning: {0} -> Current and Target equal - you are close to fail",
                    parameter.ParameterName);
                return par;
            }
            if (Checker.CurrentValueIsBetterThanTargetValue(parameter.CurrentValue, verifyerParameter.TargetValue) &
                !Checker.CurrentValueIsBetterThanTargetValueAndWarning(parameter.CurrentValue,
                                                                       verifyerParameter.TargetValue,
                                                                       verifyerParameter.WarningValue))
            {

                par.IsOk = true;
                par.VeriferResult = string.Format(
                    "Warning: {0} -> Current better than target but within warning - you are close to fail",
                    parameter.ParameterName);
                return par;
            }
            if (Checker.CurrentValueIsBetterThanTargetValue(parameter.CurrentValue, verifyerParameter.TargetValue) && verifyerParameter.RatchetValue > 0)
            {
                par.IsOk = true;
                par.VeriferResult = string.Format("Success: {0} -> Current is better than Target and can be ratcheted to {1}",
                                                  parameter.ParameterName,
                                                  Checker.NewRatchetValue(verifyerParameter.TargetValue,
                                                                          verifyerParameter.RatchetValue));
                return par;
            }

            if (Checker.CurrentValueIsBetterThanTargetValue(parameter.CurrentValue, verifyerParameter.TargetValue))
            {
                par.IsOk = true;
                par.VeriferResult = string.Format("Success: {0} -> Current is better than Target", parameter.ParameterName);
                return par;
            }

            par.IsOk = false;
            par.VeriferResult = "Error in verification: somehow i didn't  foresee this";
            return par;
        }




        public IEnumerable<OutputParameter> CheckDirectVsParameterList(Parameter parameter,
                                                                       IEnumerable<VerifyerParameter> verifyerParameters)
        {
            foreach (var verifyerParameter in verifyerParameters)
            {
                if (verifyerParameter.ParameterName.ToLower() == parameter.ParameterName.ToLower())
                {

                }
            }
            //bool borde räcka
            //CHEXCK DIRECT BORDE FUNKA
            return new List<OutputParameter> {new OutputParameter("a", "b", false)};
        }
    }
}