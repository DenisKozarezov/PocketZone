namespace Core.Services.Serialization
{
    public interface ILocalStateSerializer
    {
        void Serialize(string path, object value);
        void Serialize(string path, string value);
        void Clear(string path);
    }
}