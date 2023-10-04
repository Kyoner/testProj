using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]private Button deleteButton;
    [SerializeField]private TextMeshProUGUI AmountUI;
    public ItemSO ItemData { get; private set; }
    public int NumSlot { get; private set; }
    public void GetData(ItemSO data, int num)
    {
        gameObject.SetActive(true);
        NumSlot = num;
        ItemData = data;
        UpdateData();
    }
    public void UpdateData()
    {
        GetComponent<Image>().sprite = ItemData.Sprite;
        AmountUI.text = ItemData.Amount.ToString();
        if (ItemData.Amount == 1)
        {
            AmountUI.gameObject.SetActive(false);
        }
        else
        {
            AmountUI.gameObject.SetActive(true);
        }
    }
    public void LoadData(string datajson, string spritejson, int num, out int emptySlot)
    {
        NumSlot = num;
        if (datajson != "")
        {
            gameObject.SetActive(true);
            ItemData = ScriptableObject.CreateInstance<ItemSO>();
            JsonUtility.FromJsonOverwrite(datajson, ItemData);
            ItemData.ChangeSprite(SpriteSerializator.DeserializeSprite(spritejson));
            UpdateData();
            emptySlot = 15;
        }
        else
            emptySlot = num;
    }
    public void LostFocus()
    {
        deleteButton.gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Player.Inventory.LostOfFocus();
        deleteButton.gameObject.SetActive(true);
    }
    public void RemoveSlot() 
    {
        ItemData = null;
        Player.Inventory.DeleteFull(this);
        LostFocus();
        gameObject.SetActive(false);
    }
}
