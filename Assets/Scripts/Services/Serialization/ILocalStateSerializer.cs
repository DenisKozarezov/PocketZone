using Newtonsoft.Json.Linq;

namespace Core.Services.Serialization
{
    public interface ILocalStateSerializer
    {
        JToken Deserialize(string path);
        void Serialize(string path, object value);
        void Serialize(string path, JToken value);
        void Clear(string path);
    }
}