using Newtonsoft.Json.Linq;
using UnityEngine;
using Core.Services.Serialization;

namespace Core.Services.Inventory
{
    public class InventoryItemModel : ISerializableObject
    {
        public int ID;
        public string DisplayName;
        public bool Stackable;
        public Sprite Icon;
        public int Stacks;

        public JToken Serialize()
        {
            JObject obj = new JObject();
            obj.Add("id", JToken.FromObject(ID));
            obj.Add("name", JToken.FromObject(DisplayName));
            obj.Add("icon", new SerializedSprite(Icon).Serialize());
            obj.Add("stackable", JToken.FromObject(Stackable));
            obj.Add("stacks", JToken.FromObject(Stacks));
            return obj;
        }
        public void Deserialize(JToken token)
        {
            ID = token["id"].Value<int>();
            DisplayName = token["name"].Value<string>();
            Stackable = token["stackable"].Value<bool>();
            Icon = SerializedSprite.Deserialize(token["icon"]);
            Stacks = token["stacks"].Value<int>();
        }
    }
}