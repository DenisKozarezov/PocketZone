using System.IO;
using System.Text;
using UnityEngine;

namespace Core.Services.Serialization
{
    public sealed class LocalJSONSerializer : ILocalStateSerializer
    {
        private object _lock = new object();
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
        public void Serialize(string path, string value)
        {
            lock (_lock)
            {
                using (var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(value);
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