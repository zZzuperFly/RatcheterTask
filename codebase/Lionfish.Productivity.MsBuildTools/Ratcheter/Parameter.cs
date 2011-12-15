namespace Ratcheter
{
   public class Parameter
   {
       private string _parameterName;
       private int _currentValue;
       private int _targetValue;
       private int _warning;
       private int _ratchetValue;

       public Parameter(string parameterName, int currentValue, int targetValue, int warning, int ratchetValue)
       {
           _parameterName = parameterName;
           _currentValue = currentValue;
           _targetValue = targetValue;
           _warning = warning;
           _ratchetValue = ratchetValue;
       }

       public string ParameterName
       {
           get { return _parameterName; }
       }

       public int CurrentValue
       {
           get { return _currentValue; }
       }

       public int TargetValue
       {
           get { return _targetValue; }
       }

       public int Warning
       {
           get { return _warning; }
       }

       public int RatchetValue
       {
           get { return _ratchetValue; }
       }
   }
}
