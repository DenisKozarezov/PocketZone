using System.IO;
using Newtonsoft.Json.Linq;
using UnityEditor;
using Zenject;

namespace Core.Services.Serialization
{
    public class GameState
    {
        private readonly DiContainer _container;
        private readonly ILocalStateSerializer _serializer;

        public static bool IsLoadingGame = false;

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
        public bool HasAnySave() => File.Exists(FilePath);
        public void Serialize()
        {
            JObject token = new JObject();
            foreach (var obj in _container.ResolveAll<ISerializableObject>())
            {
                token.Add(obj.GetType().Name, obj.Serialize());
            }

            _serializer.Clear(FilePath);
            _serializer.Serialize(FilePath, token);

#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
        public void Deserialize()
        {
            JToken save = _serializer.Deserialize(FilePath);
            if (save != null)
            {
                foreach (var obj in _container.ResolveAll<ISerializableObject>())
                {
                    obj.Deserialize(save);
                }
            }
        }
    }
}