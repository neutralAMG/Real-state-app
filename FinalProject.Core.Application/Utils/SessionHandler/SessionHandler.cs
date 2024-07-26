using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FinalProject.Core.Application.Utils.SessionHandler
{
    public static class SessionHandler
    {
        public static void Set<TValue>(this ISession session, TValue value, string key)
        {
            string serializedValue = JsonConvert.SerializeObject(value);

            session.SetString(key, serializedValue);
        }

        public static TValue Get<TValue>(this ISession session, string key)
        {
            string storeValue = session.GetString(key);

            TValue deserializedValue = JsonConvert.DeserializeObject<TValue>(storeValue);

            return deserializedValue == null ? default : deserializedValue;
        }
    }
}
