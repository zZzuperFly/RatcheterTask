namespace Ratcheter
{
    public  interface IChecker
    {
        bool CurrentValueIsBetterThanTargetValue(int current, int target);
        bool CurrentValueIsBetterThanTargetValueAndWarning(int current, int target, int warning);
        
        bool CurrentValueIsEqualToTargetValue(int current, int target);
        bool CurrentValueIsWorseThanTargetValue(int current, int target);

        bool CanBeRatcheted(int current, int target, int warning, int ratchet);
        int NewRatchetValue(int target, int ratchet);

    }
}