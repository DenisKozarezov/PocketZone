using Newtonsoft.Json.Linq;
using Zenject;

namespace Core.Services.Serialization
{
    public class GameState
    {
        private readonly DiContainer _container;
        private readonly ILocalStateSerializer _serializer;

#if UNITY_EDITOR
        private readonly string FilePath = "Assets\\PlayerData.json";
#else
        private readonly string FilePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
#endif
        public GameState(DiContainer container, ILocalStateSerializer serializer) 
        {
            _container = container;
            _serializer = serializer;
        }

        public void Serialize()
        {
            JObject token = new JObject();
            foreach (var obj in _container.ResolveAll<ISerializableObject>())
            {
                token.Add(obj.GetType().Name, obj.Serialize());
            }

            _serializer.Clear(FilePath);
            _serializer.Serialize(FilePath, token);
        }
    }
}