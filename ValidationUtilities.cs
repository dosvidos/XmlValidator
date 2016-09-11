using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace XmlValidator
{
    static class ValidationUtilities
    {
        public const String Namespace = "http://example.com/xml/datacontractserializer";

        public static Registry MakeRegistry()
        {
            var directory = new DirectoryEntry("Windows", new[] { new FileEntry("autoexec.bat", 32, DateTime.UtcNow) });
            return new Registry(12345, new [] { directory });
        }

        public static void ValidationEventHandler(Object sender, ValidationEventArgs eventArgs)
        {
            Console.WriteLine($"XML validation failed: {eventArgs.Message}");
        }

        public static void WriteToFile(XDocument document)
        {
            using (var writer = File.CreateText(Path.GetRandomFileName()))
            {
                document.Save(writer);
            }
        }

        public static void WriteToMemoryStream(XDocument document)
        {
            using (var memoryStream = new MemoryStream())
            {
                document.Save(memoryStream, SaveOptions.OmitDuplicateNamespaces);
                Console.WriteLine(Encoding.UTF8.GetString(memoryStream.GetBuffer()));
                document.Save(Console.Out);
            }
        }

        private static IEnumerable<XmlSchema> GetXsdForType<T>(XsdDataContractExporter exporter)
        {
            XmlQualifiedName xmlNameValue = exporter.GetRootElementName(typeof(T));
            foreach (XmlSchema schema in exporter.Schemas.Schemas(xmlNameValue?.Namespace ?? String.Empty))
            {
                yield return schema;
            }
        }
    }
}