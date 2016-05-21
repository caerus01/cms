using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Caerus.Common.ContractResolvers;
using Caerus.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Caerus.Common.Extentions
{
   public static class ObjectionExtensions
   {
       public static string ToJson(this object source, bool includeNull = true, JsonContractResolver camelCase = JsonContractResolver.Default, bool enumToString = false, Newtonsoft.Json.Formatting? formatting = null)
       {
           var converter = new JsonConverter[] { };
           if (enumToString)
               converter = new JsonConverter[] { new StringEnumConverter() };
           IContractResolver resolver = new DefaultContractResolver();
           switch (camelCase)
           {
               case JsonContractResolver.CamelCase:
                   {
                       resolver = new CamelCasePropertyNamesContractResolver();
                       break;
                   }
               case JsonContractResolver.SnakeCase:
                   {
                       resolver = new SnakeCasePropertyNamesContractResolver();
                       break;
                   }
               case JsonContractResolver.LowerCase:
                   {
                       resolver = new LowercaseContractResolver();
                       break;
                   }
           }

           var settings = new JsonSerializerSettings()
           {
               ContractResolver = resolver,
               Converters = converter,
               NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore
           };
           if (formatting == null)
               return JsonConvert.SerializeObject(source, settings);
           else
               return JsonConvert.SerializeObject(source, formatting.Value, settings);
       }

       public static string ToXml<T>(this object source, Encoding encoding = null, String xmlPrefix = "", String xmlNamespace = "")
       {
           if (encoding == null)
               encoding = Encoding.UTF8;
           var serializer = new XmlSerializer(typeof(T));
           var sww = new StringWriterWithEncoding(encoding);
           var writer = XmlWriter.Create(sww);
           var xmlnsEmpty = new XmlSerializerNamespaces();
           xmlnsEmpty.Add(xmlPrefix, xmlNamespace);
           serializer.Serialize(writer, source, xmlnsEmpty);
           var xml = sww.ToString();
           return xml;
       }

       public static void CopyProperties(this object source, object destination)
       {
           // If any this null throw an exception
           if (source == null || destination == null)
             return ;
           // Getting the Types of the objects
           Type typeDest = destination.GetType();
           Type typeSrc = source.GetType();
           // Collect all the valid properties to map
           var results = from srcProp in typeSrc.GetProperties()
                         let targetProperty = typeDest.GetProperty(srcProp.Name)
                         where srcProp.CanRead
                         && targetProperty != null
                         && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
                         && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                         && (targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                         || typeof(Enum).IsAssignableFrom(targetProperty.PropertyType)
                         || typeof(Enum).IsAssignableFrom(srcProp.PropertyType)
                         || Nullable.GetUnderlyingType(srcProp.PropertyType) == targetProperty.PropertyType)
                         select new { sourceProperty = srcProp, targetProperty = targetProperty };
           //map the properties
           foreach (var props in results)
           {
               var value = props.sourceProperty.GetValue(source, null);
               if (Nullable.GetUnderlyingType(props.sourceProperty.PropertyType) != null &&
                   Nullable.GetUnderlyingType(props.targetProperty.PropertyType) == null && value == null)
                   continue;

               props.targetProperty.SetValue(destination, value, null);
           }
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
