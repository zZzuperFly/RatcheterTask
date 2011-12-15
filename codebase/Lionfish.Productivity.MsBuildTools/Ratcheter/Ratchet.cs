﻿// created by: Östen Petersson (SIS)
// at: 15:51: 08/12: 2011
// ------------------------------------------------------------
// copyright SIS

using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Ratcheter
{
    public enum RatchetingDirections
    {
        TowardsZero,
        TowardsHundred
    }

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

        public ILogProxy MyLogger
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
            var verifyer = new Verifyer(MyLogger, RatchetingDirection  );
            bool result = false ;
            switch (MyInputType)
            {
                case InputTypes.Direct:
                    result = verifyer.CheckDirectInput(MyCurrentValue, MyTargetValue, MyWarningValue);
                    break;
                case InputTypes.DirectVsFile:
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
    }
}