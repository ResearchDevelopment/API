using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ShadiWebApplication.Helpers
{
    
    public static class JsonHelper
    {
        public static string ToJson<TModel>(TModel model, bool camelCase = false)
        {
            var serializiationSettings = new JsonSerializerSettings();
            var contractResolver       = new DefaultContractResolver();

            try
            {
                if(camelCase)
                {
                    contractResolver.NamingStrategy = new CamelCaseNamingStrategy();
                }

                serializiationSettings.ContractResolver = contractResolver;

                return JsonConvert.SerializeObject(value: model, settings: serializiationSettings);
            }
            catch
            {
                return null;
            }
        }

        public static TModel FromJson<TModel>(string jsonString,bool camelCase = false)
        {
            var serializiationSettings = new JsonSerializerSettings();
            var contractResolver = new DefaultContractResolver();

            try
            {
                if (camelCase)
                {
                    contractResolver.NamingStrategy = new CamelCaseNamingStrategy();
                }

                serializiationSettings.ContractResolver = contractResolver;

                return JsonConvert.DeserializeObject<TModel>(jsonString, settings: serializiationSettings);
            }
            catch
            {
                return default(TModel);
            }
        }
    }
}