// created by: Östen Petersson (SIS)
// at: 15:41: 28/03: 2012
// ------------------------------------------------------------
// copyright SIS

using System.Collections.Generic;

namespace Ratcheter
{
    public class Project:IProject
    {
        private List<Parameter> _parameters;

        private List<VerifyerParameter> _verifyerParameters;

        private List<OutputParameter> _outputParameters;
        private string _projectName;

        public Project(string projectName, List<Parameter> parameters)
        {
            _projectName = projectName;
            _parameters = parameters;
        }

        public Project(string projectName,List<VerifyerParameter> verifyerParameters)
        {
            _verifyerParameters = verifyerParameters;
            _projectName = projectName;
        }

        public Project(string projectName, List<OutputParameter> outputParameters)
        {
            _projectName = projectName;
            _outputParameters = outputParameters;
        }

        public List<Parameter> Parameters
        {
            get { return _parameters; }
        }

        public List<VerifyerParameter> VerifyerParameters
        {
            get { return _verifyerParameters; }
        }

        public List<OutputParameter> OutputParameters
        {
            get { return _outputParameters; }
        }

        public string ProjectName
        {
            get { return _projectName; }
        }
    }

    public interface IProject
    {
        List<Parameter> Parameters { get; }
        List<VerifyerParameter> VerifyerParameters { get; }
        List<OutputParameter> OutputParameters { get; }

        string ProjectName { get;  }

    }
}