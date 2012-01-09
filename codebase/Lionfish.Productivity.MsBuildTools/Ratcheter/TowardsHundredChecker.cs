namespace Ratcheter
{
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

        public bool CanBeRatcheted(int current, int target, int warning, int ratchet)
        {
            return current >= NewRatchetValue(target , ratchet);
        }

        public int NewRatchetValue(int target, int ratchet)
        {
            return (target + ratchet);
        }
    }
}