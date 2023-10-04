using System;
using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
[System.Serializable]
public class ItemSO : ScriptableObject
{
    public int Id;
    public Sprite Sprite;
    public itemType Type;
    public int Amount;

    public ItemSO(int Id, Sprite Sprite, itemType Type, int Amount)
    {
        this.Id = Id;
        this.Sprite = Sprite;
        this.Type = Type;
        this.Amount = Amount;
    }

    public ItemSO(int Id, string jsonSprite, itemType Type, int Amount) 
    {
        this.Id = Id;
        this.Type = Type;
        this.Amount = Amount;
        Sprite = SpriteSerializator.DeserializeSprite(jsonSprite);
    }
    public void Init(ItemSO template)
    {
        Id= template.Id;
        Sprite = template.Sprite;
        Type = template.Type;
        Amount = template.Amount;
    }
    public void ChangeSprite(Sprite sprite)
    {
        Sprite = sprite;
    }
    public void AddAmount(int amount)
    {
        Amount += amount;
    }
    public void DeleteOne()
    {
        Amount--;
    }
}
public enum itemType
{
    bullet, item
}