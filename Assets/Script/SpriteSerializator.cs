using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteSerializator
{
    public static string SerializeSprite(Sprite sprite)
    {
        SerializeTexture st = new SerializeTexture();
        Texture2D tex = sprite.texture;
        st.x = tex.width;
        st.y = tex.height;
        st.bytes = ImageConversion.EncodeToPNG(tex);
        return JsonUtility.ToJson(st);
    }
    public static Sprite DeserializeSprite(string json)
    {
        SerializeTexture st = new SerializeTexture();
        st = JsonUtility.FromJson<SerializeTexture>(json);
        Texture2D tex = new Texture2D(st.x, st.y);
        ImageConversion.LoadImage(tex, st.bytes);
        Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
        return mySprite;
    }
}
public class SerializeTexture
{
    [SerializeField]
    public int x;
    [SerializeField]
    public int y;
    [SerializeField]
    public byte[] bytes;
}
