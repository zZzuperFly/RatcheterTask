using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Ratcheter
{
    public interface IXmlHandler
    {
        void WriteOutputParametersToFile(string filePath, List<OutputParameter > outputParameters );
        List<Project > ReadVerifyerParametersFromFile(string filePath);
        List<Project> ReadParameterProjectsFromFile(string filePath);
        void WriteUpdatedVerifyerParametersToFile(string filePath, List<Project> verifyerParameters);

    }

    public class XmlHandler : IXmlHandler
    {
        public void WriteOutputParametersToFile(string filePath, List<OutputParameter> outputParameters)
        {
            throw new NotImplementedException();
        }

        public List<Project> ReadVerifyerParametersFromFile(string filePath)
        {
            var projects = new List<Project>();
            XElement element = XElement.Load(filePath);

            foreach (XElement project in element.Elements())
            {
                //i am sure this can be more elegant - but never mind
                var projectName = project.Element("Name").Value;
                var verifiers = new List<VerifyerParameter>();
                foreach (var parameter in project.Elements("Parameters"))
                {

                    foreach (var xElement in parameter.Elements("Parameter"))
                    {
                        var PARNAME = xElement.Element("Name").Value;
                        var value = xElement.Element("Value").Value;
                        var ratchet = xElement.Element("Ratchet").Value;
                        var warning = xElement.Element("Warning").Value;
                        var direction = xElement.Element("Direction").Value;
                        verifiers.Add(new VerifyerParameter(PARNAME, int.Parse(value), int.Parse(ratchet), int.Parse(warning), (RatchetingDirections)Enum.Parse(typeof(RatchetingDirections), direction , true)));
                    }
                }
                var proj = new Project(projectName, verifiers);
                projects.Add(proj);

            }
            return projects;
        }

        public List<Project> ReadParameterProjectsFromFile(string filePath)
        {
            var projects = new List<Project>();
            XElement element = XElement.Load(filePath);

            foreach (XElement project in element.Elements())
            {
                //i am sure this can be more elegant - but never mind
                var projectName = project.Element("Name").Value;
                var verifiers = new List<Parameter>();
                foreach (var parameter in project.Elements("Parameters"))
                {

                    foreach (var xElement in parameter.Elements("Parameter"))
                    {
                        var PARNAME = xElement.Element("Name").Value ;
                        var value = xElement.Element("Value").Value ;
                        verifiers.Add(new Parameter(PARNAME, int.Parse(value)));
                    }
                }
                var proj = new Project(projectName, verifiers);
                projects.Add(proj);

            }
            return projects;
        }


        public void WriteUpdatedVerifyerParametersToFile(string filePath, List<Project > verifyerProjects)
        {
          if(System.IO.File.Exists(filePath ))
          {
              System.IO.File.Delete(filePath);
          }
            XDocument document = new XDocument();
            XElement projects = new XElement("Projects");
            document.Add(projects);
            foreach (var verifyerproject in verifyerProjects)
            {
                XElement project = new XElement("Project");



                

            }


        }

    
    }
}
