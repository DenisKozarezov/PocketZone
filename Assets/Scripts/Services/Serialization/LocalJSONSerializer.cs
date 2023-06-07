using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Core.Services.Serialization
{
    public sealed class LocalJSONSerializer : ILocalStateSerializer
    {
        private object _lock = new object();
        public JToken? Deserialize(string path)
        {
            if (!File.Exists(path))
                return null;

            try
            {
                string json = File.ReadAllText(path);
                return JToken.Parse(json);
            }
            catch
            {
                return null;
            }
        }
        public void Serialize(string path, object value)
        {
            string json = JsonUtility.ToJson(value, true);
            lock (_lock)
            {
                using (var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(json);
                    file.Seek(file.Length, SeekOrigin.Begin);
                    file.Write(bytes, 0, bytes.Length);
                }
            }
        }
        public void Serialize(string path, JToken value)
        {
            string json = value.ToString(Newtonsoft.Json.Formatting.Indented);
            lock (_lock)
            {
                using (var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(json);
                    file.Seek(file.Length, SeekOrigin.Begin);
                    file.Write(bytes, 0, bytes.Length);
                }
            }
        }
        public void Clear(string path)
        {
            lock (_lock)
            {
                using (var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    file.SetLength(0);
                }
            }
        }
    }
}