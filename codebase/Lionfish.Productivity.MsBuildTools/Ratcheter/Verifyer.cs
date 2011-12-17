using Microsoft.Build.Framework;

namespace Ratcheter
{
    public class Verifyer
    {
        private ILogProxy _logProxy;
        private IChecker _checker;

        public Verifyer(ILogProxy  logProxy, RatchetingDirections direction)
        {
            _logProxy = logProxy;

            if (direction == RatchetingDirections.TowardsZero )
            {
                _checker = new TowardsZeroChecker();
            }
            else
            {
                _checker = new TowardsHundredChecker();
            }
        }


        public IChecker Checker
        {
            get { return _checker; }
        }

        public bool CheckDirectInput(Parameter parameter , VerifyerParameter verifyerParameter )
        {
            if (parameter.ParameterName == verifyerParameter.ParameterName)
            {
                _logProxy.LogThis(MessageImportance.Low , string.Format("Checking {0}",parameter.ParameterName ));

                if (Checker.CurrentValueIsWorseThanTargetValue(parameter.CurrentValue ,verifyerParameter.TargetValue ))
                {
                    _logProxy.LogThis(MessageImportance.Normal, string.Format("Fail: {0} -> Current value is worse than target",parameter.ParameterName ));
                    return false;
                }
                if (Checker.CurrentValueIsEqualToTargetValue(parameter.CurrentValue, verifyerParameter.TargetValue))
                {
                    _logProxy.LogThis(MessageImportance.Normal,
                                     string.Format( "Warning: {0} -> Current and Target equal - you are close to fail",parameter.ParameterName ));
                    return true;

                }
                if (Checker.CurrentValueIsBetterThanTargetValue(parameter.CurrentValue, verifyerParameter.TargetValue) &
                    !Checker.CurrentValueIsBetterThanTargetValueAndWarning(parameter.CurrentValue, verifyerParameter.TargetValue, verifyerParameter.WarningValue))
                {
                    _logProxy.LogThis(MessageImportance.Normal,
                                     string.Format(  "Warning: {0} -> Current better than target but within warning - you are close to fail",parameter.ParameterName ));
                    return true;

                }
                if (Checker.CurrentValueIsBetterThanTargetValue(parameter.CurrentValue, verifyerParameter.TargetValue))
                {
                    _logProxy.LogThis(MessageImportance.Normal, string.Format( "Success: {0} -> Current is better than Target",parameter.ParameterName ));
                    return true;

                }

                _logProxy.LogThis(MessageImportance.Normal, "Error: something is outside my plan");
                return false;
            }
            _logProxy.LogThis(MessageImportance.Normal,string.Format( "Warning: no comparison due to differing parameternames {0} <-> {1}",parameter.ParameterName ,verifyerParameter.ParameterName ));
            return false;
        }
    }

    public  interface IChecker
    {
        bool CurrentValueIsBetterThanTargetValue(int current, int target);
        bool CurrentValueIsBetterThanTargetValueAndWarning(int current, int target, int warning);
        
        bool CurrentValueIsEqualToTargetValue(int current, int target);
        bool CurrentValueIsWorseThanTargetValue(int current, int target);

    }

    internal class TowardsHundredChecker:IChecker
    {
        public bool CurrentValueIsBetterThanTargetValue(int current, int target)
        {
            return current > target;
        }

        public bool CurrentValueIsBetterThanTargetValueAndWarning(int current, int target, int warning)
        {
            return current > (target + warning);
        }

        public bool CurrentValueIsEqualToTargetValue(int current, int target)
        {
            return current == target;
        }

        public bool CurrentValueIsWorseThanTargetValue(int current, int target)
        {
            return current < target;
        }
    }
    internal class TowardsZeroChecker : IChecker
    {
        public bool CurrentValueIsBetterThanTargetValue(int current, int target)
        {
            return current < target;
        }

        public bool CurrentValueIsBetterThanTargetValueAndWarning(int current, int target, int warning)
        {

            return current < (target - warning);
        }

        public bool CurrentValueIsEqualToTargetValue(int current, int target)
        {
            return current == target;
        }

        public bool CurrentValueIsWorseThanTargetValue(int current, int target)
        {
            return current > target;
        }
    }
}