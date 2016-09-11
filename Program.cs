using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XmlValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            var registry = ValidationUtilities.MakeRegistry();

            // 1. Write XSD
            var xsdExporter = new XsdDataContractExporter();
            xsdExporter.Export(registry.GetType());

            // 2. Write XML
            var document = new XDocument(new XDeclaration("1.0", Encoding.UTF8.WebName, "yes"));
            var serializer = new DataContractSerializer(registry.GetType());
            using (var writer = document.CreateWriter())
            {
                serializer.WriteObject(writer, registry);
            }

            Console.WriteLine("Initial document");
            document.Save(Console.Out);
            Console.WriteLine();

            // 3. Validate XML
            Console.WriteLine("Validation of initial document");
            document.Validate(xsdExporter.Schemas, ValidationUtilities.ValidationEventHandler);

            // 4. Break the document
            document.Root?.AddFirst(new XElement("lala"));
            Console.WriteLine("Modified document");
            document.Save(Console.Out);
            Console.WriteLine();

            // 5. Validate again
            Console.WriteLine("Validation of modified document");
            document.Validate(xsdExporter.Schemas, ValidationUtilities.ValidationEventHandler);
        }
    }
}
