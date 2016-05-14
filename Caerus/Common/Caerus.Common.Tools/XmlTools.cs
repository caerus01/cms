using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Caerus.Common.Tools
{
    public static class XmlTools
    {
        public static string GetXmlString(Dictionary<string, string> valueDictionary)
        {
            var result = string.Empty;

            foreach (var pair in valueDictionary)
            {
                var value = string.Empty;
                if (pair.Value != null)
                {
                    value = pair.Value
                        .Replace('"', ' ')
                        .Replace('&', ' ')
                        .Replace('\'', ' ')
                        .Replace('<', ' ')
                        .Replace('>', ' ');
                }
                result += "<" + pair.Key + ">" + value + "</" + pair.Key + ">";
            }

            return result;
        }

        public static string GetElementInnerText(string inputXml, string elementName)
        {
            if (string.IsNullOrEmpty(inputXml) || string.IsNullOrEmpty(elementName))
            {
                return string.Empty;
            }

            var resultXmlDoc = new XmlDocument();
            resultXmlDoc.LoadXml(inputXml);

            var nodeList = resultXmlDoc.GetElementsByTagName(elementName);

            return nodeList.Count > 0 ? nodeList[0].InnerText : string.Empty;
        }

        public static XmlNodeList GetXmlNodesByName(string inputXml, string nameOfNodeToGet)
        {
            var xdoc = new XmlDocument();
            xdoc.LoadXml(inputXml);
            return xdoc.SelectNodes(nameOfNodeToGet);
        }

        public static String SerializeObjectToXml<T>(T requestObject, Encoding encoding = null, String xmlPrefix = "", String xmlNamespace = "")
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            var serializer = new XmlSerializer(typeof(T));
            var sww = new StringWriterWithEncoding(encoding);
            var writer = XmlWriter.Create(sww);
            var xmlnsEmpty = new XmlSerializerNamespaces();
            xmlnsEmpty.Add(xmlPrefix, xmlNamespace);
            serializer.Serialize(writer, requestObject, xmlnsEmpty);
            var xml = sww.ToString();
            return xml;
        }

        public static T DeserializeXmlToObject<T>(string xml, bool stripXSI = false)
        {
            if (stripXSI)
                xml = Regex.Replace(xml, "(xsi:type=\".*?\")", "", RegexOptions.Multiline);
            var reader = new StringReader(xml);
            var responseSerializer = new XmlSerializer(typeof(T));
            return (T)responseSerializer.Deserialize(reader);
        }

        public sealed class StringWriterWithEncoding : StringWriter
        {
            private readonly Encoding encoding;

            public StringWriterWithEncoding(Encoding encoding)
            {
                this.encoding = encoding;
            }

            public override Encoding Encoding
            {
                get { return encoding; }
            }
        }
    }
}
