using global.cloudis.RealexHPP.sdk.domain;
using global.cloudis.RealexHPP.sdk.extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace global.cloudis.RealexHPP.sdk.utils
{
    public class JsonUtils
    {
        public static string NameValueCollectionToJson(NameValueCollection form)
        {
            string json = JsonConvert.SerializeObject(NvcToDictionary(form, false));
            return json;
        }

        public static Dictionary<string, object> NvcToDictionary(NameValueCollection nvc, bool handleMultipleValuesPerKey)
        {
            var result = new Dictionary<string, object>();
            foreach (string key in nvc.Keys)
            {
                if (handleMultipleValuesPerKey)
                {
                    string[] values = nvc.GetValues(key);
                    if (values.Length == 1)
                    {
                        result.Add(key, values[0]);
                    }
                    else
                    {
                        result.Add(key, values);
                    }
                }
                else
                {
                    result.Add(key, nvc[key]);
                }
            }
            return result;
        }
    }

    public class HPPResponseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(HPPResponse))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            HPPResponse resp = new HPPResponse();

            var attrType = typeof(JsonPropertyAttribute);

            Dictionary<string, string> fields = new Dictionary<string, string>();
            Dictionary<string, string> supplementaryData = new Dictionary<string, string>();


            foreach (PropertyInfo pi in typeof(HPPResponse).GetProperties().Where(p => p.GetCustomAttributes(attrType, false).Count() > 0))
            {
                fields.Add(((JsonPropertyAttribute)(pi.GetCustomAttributes(attrType, false).First())).PropertyName, pi.Name);
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject) continue;

                string attribute = reader.Value.ToString();

                if (fields.Keys.Contains(attribute))
                {
                    resp.GetType().GetProperty(fields[attribute]).SetValue(resp, reader.ReadAsString());
                }
                else
                {
                    supplementaryData.Add(attribute, reader.ReadAsString());
                }
            }
            resp.SupplementaryData = supplementaryData;
            return resp;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
