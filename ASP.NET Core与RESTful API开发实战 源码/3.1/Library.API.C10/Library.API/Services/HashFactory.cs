using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Library.API.Services
{
    public interface IHashFactory
    {
        string GetHash(object entity);
    }

    public class HashFactory : IHashFactory
    {
        public string GetHash(object entity)
        {
            string result = string.Empty;

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var json = JsonConvert.SerializeObject(entity, serializerSettings);
            var bytes = Encoding.UTF8.GetBytes(json);

            using (var hasher = MD5.Create())
            {
                var hash = hasher.ComputeHash(bytes);
                result = BitConverter.ToString(hash);
                result = result.Replace("-", "");
            }

            return result;
        }
    }
}