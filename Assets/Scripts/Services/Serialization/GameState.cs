using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

namespace Core.Services.Serialization
{
    public class GameState
    {
        private readonly DiContainer _container;
        private readonly ILocalStateSerializer _serializer;

        private readonly string FilePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "PlayerData.json";

        public GameState(DiContainer container, ILocalStateSerializer serializer) 
        {
            _container = container;
            _serializer = serializer;
        }

        public void Serialize()
        {
            var serializableObjects = _container.ResolveAll<ISerializableObject>();

            Dictionary<string, object> jsonObject = new Dictionary<string, object>();
            foreach (var obj in serializableObjects)
            {
                jsonObject.TryAdd(nameof(obj), obj);
            }
            string json = JsonUtility.ToJson(jsonObject);

            //_serializer.Serialize(FilePath, json);
        }
    }
}