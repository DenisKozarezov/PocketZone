using Newtonsoft.Json.Linq;

namespace Core.Services.Serialization
{
    public interface ISerializableObject
    {
        JToken Serialize();
    }
}