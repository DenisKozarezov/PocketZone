using System;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Core.Services.Serialization
{
    [Serializable]
    public class SerializedSprite
    {
        public byte[] TextureData;
        public int TextureWidth;
        public int TextureHeight;
        public float PixelsPerUnit;
        public Vector2 Pivot;
        public Rect Rect;

        public SerializedSprite(Sprite sprite) 
        {
            Texture2D texture = sprite.texture;
            TextureData = texture.EncodeToPNG();
            TextureWidth = texture.width;
            TextureHeight = texture.height;
            PixelsPerUnit = sprite.pixelsPerUnit;
            Pivot = sprite.pivot;
            Rect = sprite.rect;
        }
        public JToken Serialize()
        {
            JObject obj = new JObject();
            obj.Add("textureData", JToken.FromObject(TextureData));
            obj.Add("textureWidth", JToken.FromObject(TextureWidth));
            obj.Add("textureHeight", JToken.FromObject(TextureHeight));
            obj.Add("pixelsPerUnit", JToken.FromObject(PixelsPerUnit));
            obj.Add("pivot", JToken.FromObject(Pivot));
            obj.Add("rect", JToken.FromObject(Rect));
            return obj;
        }
        public static Sprite Deserialize(JToken token)
        {
            byte[] textureData = token["textureData"].ToObject<byte[]>();
            int textureWidth = token["textureWidth"].Value<int>();
            int textureHeight = token["textureHeight"].Value<int>();
            float pixelsPerUnit = token["pixelsPerUnit"].Value<float>();
            Vector2 pivot = token["pivot"].ToObject<Vector2>();
            Rect rect = token["rect"].ToObject<Rect>();

            Texture2D texture = new Texture2D(textureWidth, textureHeight);
            texture.LoadImage(textureData);
            return Sprite.Create(texture, rect, pivot, pixelsPerUnit);
        }
    }
}