// created by: Östen Petersson (SIS)
// at: 15:51: 08/12: 2011
// ------------------------------------------------------------
// copyright SIS

using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Ratcheter
{
    /// <summary>
    /// <para>TowardsZero will ratchet downwards and stop when reaching zero</para>
    /// <para>TowardsHundred will ratchet upwards and will NOT stop when reaching hundred</para>
    /// </summary>
    /// <remarks>
    /// written by: OstenP
    /// @ 2012-03-06 - 16:05
    /// </remarks>
    public enum RatchetingDirections
    {
        TowardsZero,
        TowardsHundred
    }

    /// <summary>
    /// <para>direct verifies commandline input only</para>
    /// <para>directVsFile verifies commandline input with file of ratchet values</para>
    /// <para>FileVsFile verifies file with collected input with file of ratchet values</para>
    /// </summary>
    /// <remarks>
    /// written by: OstenP
    /// @ 2012-03-06 - 16:07
    /// </remarks>
    public enum InputTypes
    {
        Direct,
        DirectVsFile,
        FileVsFile
    }


    public class Ratchet : Task
    {
        private string _currentValue;
        private string _direction;
        private ILogProxy _myLogger;
        private string _targetValue;
        private string _ratchetValue;
        private string _warningValue;
        private string  _inputType;
        private IXmlHandler _myXmlHandler;
        private string _parameterName;
        private string _verifyParameterName;


        [Required]
        public string Direction
        {
            set { _direction = value; }
        }

        public string CurrentValue
        {
            set { _currentValue = value; }
        }

        public string TargetValue
        {
            set { _targetValue = value; }
        }

        public string WarningValue
        {

            set { _warningValue = value; }
        }

        public string RatchetValue
        {

            set { _ratchetValue = value; }
        }

        public string InputType
        {

            set { _inputType = value; }
        }

        public string ParameterName
        {
            get { return _parameterName; }
            set { _parameterName = value; }
        }

        public string VerifyParameterName
        {
            get { return _verifyParameterName; }
            set { _verifyParameterName = value; }
        }

        internal  ILogProxy MyLogger
        {
            get
            {
                if (_myLogger == null)
                {
                    _myLogger = new LogProxy(this);
                }
                return _myLogger;
            }
            set { _myLogger = value; }
        }


        internal RatchetingDirections RatchetingDirection
        {
            get { return (RatchetingDirections) Enum.Parse(typeof (RatchetingDirections), _direction, true); }
        }

        internal  InputTypes MyInputType
        {
            get { return (InputTypes) Enum.Parse(typeof (InputTypes), _inputType, true); }
        }

        internal int MyRatchetValue
        {
            get { return CheckNumericInputs(_ratchetValue , "RatchetValue"); }
        }

        internal int MyTargetValue
        {
            get
            {
                return CheckNumericInputs(_targetValue , "TargetValue");
            }
        }

        internal int MyCurrentValue
        {
            get
            {
                return CheckNumericInputs(_currentValue, "CurrentValue");
            }
        }

        internal int MyWarningValue
        {
            get {
                return CheckNumericInputs(_warningValue ,"WarningValue");
            }
        }



        internal IXmlHandler MyXmlHandler
        {
            get
            {
                if (_myXmlHandler == null)
                {
                    _myXmlHandler = new XmlHandler();
                }
                return _myXmlHandler;
            }
            set { _myXmlHandler = value; }
        }


        private int CheckNumericInputs(string input,  string propertyName)
        {
            int result = 0;
            if (string.IsNullOrEmpty(input))
            {
                return result;
            }
            if (int.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                MyLogger.LogThis(MessageImportance.High, string.Format("{0}: incorrect input, numeric expected",propertyName ));
            }
            return result;
        }

        public override bool Execute()
        {
            MyLogger.LogThis(MessageImportance.Low, "ratchet: started!");

            //verify properties
            var verifyer = new Verifyer(MyLogger  );
            bool result = false ;
            switch (MyInputType)
            {
                case InputTypes.Direct:
                    Parameter parameter = CreateParameter();
                    VerifyerParameter verifyerParameter = CreateVerifyParameter();
                    result = verifyer.CheckDirectInput(parameter,verifyerParameter );
                    break;
                case InputTypes.DirectVsFile:
                    var validateXML = new XmlValidator(MyLogger);
                    //validateXML.ValidateXml()
                    break;
                case InputTypes.FileVsFile:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            //read xml
            //iterate through objectlist
            //write and log 


            return result;
        }

        private VerifyerParameter CreateVerifyParameter()
        {
            //verify all necessary input
            return new VerifyerParameter(VerifyParameterName , MyTargetValue, MyRatchetValue, MyWarningValue,RatchetingDirection );
        }

        private Parameter CreateParameter()
        {
            //verify all necessary input
            return new Parameter(ParameterName, MyCurrentValue);
        }
    }
}