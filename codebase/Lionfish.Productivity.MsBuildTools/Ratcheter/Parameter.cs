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
        private RatchetingDirections _direction;

        public VerifyerParameter(string parameterName, int targetValue, int ratchetValue, int warningValue, RatchetingDirections direction)
        {
            _parameterName = parameterName;
            _targetValue = targetValue;
            _ratchetValue = ratchetValue;
            _warningValue = warningValue;
            _direction = direction;
        }

        public RatchetingDirections Direction
        {
            get { return _direction; }
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
    public class OutputParameter
    {
        private string _parameterName;
        private string _veriferResult;

        public bool IsOk
        {
            get { return _isOk; }
            set { _isOk = value; }
        }

        private bool _isOk;

        public OutputParameter(string parameterName, string veriferResult, bool isOk)
        {
            _parameterName = parameterName;
            _veriferResult = veriferResult;
            _isOk = isOk;
        }

        public string ParameterName
        {
            get { return _parameterName; }
            set { _parameterName = value; }
        }

        public string VeriferResult
        {
            get { return _veriferResult; }
            set { _veriferResult = value; }
        }
    }

}
