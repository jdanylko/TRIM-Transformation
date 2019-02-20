using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TransformationExample.Extensions
{
    public static class XmlDocumentExtensions
    {
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }

        public static XmlDocument ToXmlDocument(this XElement xElement)
        {
            var sb = new StringBuilder();
            var xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false };
            using (var xw = XmlWriter.Create(sb, xws))
            {
                xElement.WriteTo(xw);
            }
            var doc = new XmlDocument();
            doc.LoadXml(sb.ToString());
            return doc;
        }

        public static Stream ToMemoryStream(this XmlDocument doc)
        {
            var xws = new XmlWriterSettings
            {
                NewLineChars = String.Empty,
                OmitXmlDeclaration = true,
                Indent = false
            };

            using (MemoryStream stream = new MemoryStream())
            using (XmlWriter writer = XmlWriter.Create(stream, xws))
            {
                doc.WriteTo(writer);
                writer.Flush();

                return stream;
            }
        }
    }
}
