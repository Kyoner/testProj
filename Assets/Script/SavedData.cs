using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SavedData
{
    public string[] itemJsonMass = new string[15];
    public int health;
    public string[] spriteJson = new string[15];
    public SavedData(ItemSlot[] items, int health) 
    {
        for (int i = 0; i < items.Length; i++)
        {
            itemJsonMass[i] = JsonUtility.ToJson(items[i].ItemData);
            if(items[i].ItemData?.Sprite is Sprite sprite)
                spriteJson[i] = SpriteSerializator.SerializeSprite(sprite);
        }
        this.health = health;
    }
}
