namespace Ratcheter
{
   public class Parameter
   {
       private string _parameterName;
       private int _currentValue;

       public Parameter(string parameterName, int currentValue)
       {
           _parameterName = parameterName;
           _currentValue = currentValue;
       }

       public string ParameterName
       {
           get { return _parameterName; }
       }

       public int CurrentValue
       {
           get { return _currentValue; }
       }

   }
    public class VerifyerParameter
    {
        private string _parameterName;
        private int _targetValue;
        private int _ratchetValue;
        private int _warningValue;

        public VerifyerParameter(string parameterName, int targetValue, int ratchetValue, int warningValue)
        {
            _parameterName = parameterName;
            _targetValue = targetValue;
            _ratchetValue = ratchetValue;
            _warningValue = warningValue;
        }

        public string ParameterName
        {
            get { return _parameterName; }
        }

        public int TargetValue
        {
            get { return _targetValue; }
        }

        public int RatchetValue
        {
            get { return _ratchetValue; }
        }

        public int WarningValue
        {
            get { return _warningValue; }
        }
    }
}
