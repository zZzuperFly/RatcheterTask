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

        public bool CheckDirectInput(int current, int target, int warning)
        {
            if (Checker.CurrentValueIsWorseThanTargetValue(current, target))
            {
                _logProxy.LogThis(MessageImportance.Normal, "Fail: Current value is worse than target");
                return false;
            }
            if (Checker.CurrentValueIsEqualToTargetValue(current, target))
            {
                _logProxy.LogThis(MessageImportance.Normal, "Warning: Current and Target equal - you are close to fail");
                return true;

            }
            if (Checker.CurrentValueIsBetterThanTargetValue(current, target) &
                !Checker.CurrentValueIsBetterThanTargetValueAndWarning(current, target, warning))
            {
                _logProxy.LogThis(MessageImportance.Normal,
                                  "Warning: Current better than target but within warning - you are close to fail");
                return true;

            }
            if (Checker.CurrentValueIsBetterThanTargetValue(current, target))
            {
                _logProxy.LogThis(MessageImportance.Normal, "Success: Current is better than Target");
                return true;

            }

            _logProxy.LogThis(MessageImportance.Normal, "Error: something is outside my plan");
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