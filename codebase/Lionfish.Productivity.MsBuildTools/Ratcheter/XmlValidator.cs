using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Build.Framework;

namespace Ratcheter
{
    internal class XmlValidator
    {
        private XmlSchema _xmlSchema;
        private StringBuilder _validationErrMsg;
        private ILogProxy _logProxy;
        private bool isCorrectXml = true;

        public XmlValidator(ILogProxy proxy)
        {
            _logProxy = proxy;
            _validationErrMsg = new StringBuilder();
        }


        /// <summary>
        /// <para>Send in the filepath to both schema and file and they will be validated</para>
        /// <para>Errors in validation will be logged by logger</para>
        /// </summary>
        /// <param name="xmlPath">The XML path</param>
        /// <param name="schemaPath">The schema path</param>
        /// <returns>
        /// System.Boolean
        /// </returns>
        /// <remarks>
        /// written by: OstenP
        /// @ 2012-03-06 - 16:01
        /// </remarks>
        public bool ValidateXml(string xmlPath, string schemaPath)
        {
            bool isSchemaPathOk, isXmlPathOk;
            var schema = new XmlSchema();

            isSchemaPathOk = ValidateFilePath(schemaPath);
            isXmlPathOk = ValidateFilePath(xmlPath);

            if (isSchemaPathOk)
            {
                schema = LoadSchema(schemaPath);
            }

            if (isSchemaPathOk & isXmlPathOk & isCorrectXml )
            {
                isXmlPathOk = ValidateWithSchema(xmlPath, schema);
            }
            else
            {
                return false;
            }

            return isXmlPathOk;
        }

        private XmlSchema LoadSchema(string schemaPath)
        {

            var xmlReader = XmlReader.Create(schemaPath);
            return XmlSchema.Read(xmlReader, new ValidationEventHandler(SchemaValidationEventHandler));
        }


       private bool ValidateFilePath(string filePath)
        {
            return File.Exists(filePath);
        }

       private void SchemaValidationEventHandler(object sender, ValidationEventArgs e)
       {

           if(e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning )
           {
               _logProxy.LogThis(MessageImportance.High, e.Severity + " " + e.Message);
               isCorrectXml = false;
           }
          

       }

        private bool ValidateWithSchema(String filePath, XmlSchema schema)
        {

            var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.Schemas.Add(schema);

            // Connect to the method named ValidateHandler.
            settings.ValidationEventHandler += new ValidationEventHandler(SchemaValidationEventHandler);

            // Create the validating reader.
            XmlReader r = XmlReader.Create(filePath, settings);

            // Read through the document.
            while (r.Read())
            {
            }

            return isCorrectXml;
        }

    }
}
