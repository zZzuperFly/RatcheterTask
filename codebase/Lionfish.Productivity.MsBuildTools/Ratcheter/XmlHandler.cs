using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ratcheter
{
    public interface IXmlHandler
    {
        void WriteOutputParametersToFile(string filePath, List<OutputParameter > outputParameters );
        List<VerifyerParameter> ReadVerifyerParametersFromFile(string filePath);
        List<Parameter> ReadParametersFromFile(string filePath);
        void UpdateVerifyerParameters(string filePath, List<VerifyerParameter> verifyerParameters);

    }

    public class XmlHandler : IXmlHandler
    {
        public void WriteOutputParametersToFile(string filePath, List<OutputParameter> outputParameters)
        {
            throw new NotImplementedException();
        }

        public List<VerifyerParameter> ReadVerifyerParametersFromFile(string filePath)
        {
            var list = new List<VerifyerParameter>();
            //XmlDocument doc = new XmlDocument();
            //doc.Load(filePath );
            //foreach (XmlElement xmlElement in doc)
            //{
            //    if(xmlElement.InnerText(""))
            //}



            var o = new VerifyerParameter("", 0, 0, 0, RatchetingDirections.TowardsHundred);
            list.Add(o);
            return list;
        }

        public List<Parameter> ReadParametersFromFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public void UpdateVerifyerParameters(string filePath, List<VerifyerParameter> verifyerParameters)
        {
            throw new NotImplementedException();
        }
    }
}
