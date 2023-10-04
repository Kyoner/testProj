using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemData { get; private set; }
    public void GetData(ItemSO data)
    {
        itemData = data;
        GetComponent<SpriteRenderer>().sprite = itemData.Sprite;
    }
}
