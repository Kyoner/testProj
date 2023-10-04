using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]private GameObject inventoryUI;
    public ItemSlot[] ItemSlots { get; private set; } = new ItemSlot[15];
    private int emptyTarget = 0;
    public event Action ItemAdded;
    public void DeleteOne(ItemSO item)
    {
        item.DeleteOne();
        if (item.Amount == 0)
        {
            ItemSlot slot = Array.Find(ItemSlots, x => x.ItemData == item);
            slot.RemoveSlot();
        }
    }
    public void Clean()
    {
        foreach(ItemSlot slot in ItemSlots)
        {
            slot.RemoveSlot();
        }
        emptyTarget = 0;
    }
    public bool AddItem(ItemSO item)
    {
        if (emptyTarget != 15)
        {
            if (Array.Find(ItemSlots, x => x.ItemData?.Id == item.Id) is ItemSlot oldslot)
            {
                oldslot.ItemData.AddAmount(item.Amount);
                ItemAdded?.Invoke();
            }
            else
            {
                var newSo = ScriptableObject.CreateInstance<ItemSO>();
                newSo.Init(item);
                ItemSlots[emptyTarget].GetData(newSo, emptyTarget);
                ItemAdded?.Invoke();
                for (int i = emptyTarget + 1; i < 15; i++)
                {
                    if (ItemSlots[i].ItemData == null)
                    {
                        emptyTarget = i;
                        break;
                    }
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public void LoadItems(SavedData data)
    {
        emptyTarget = 15;
        int emptyCheck;
        for(int i = ItemSlots.Length - 1; i >= 0; i--)
        {
            ItemSlots[i].LoadData(data.itemJsonMass[i], data.spriteJson[i], i, out emptyCheck);
            if (emptyCheck < emptyTarget)
                emptyTarget = emptyCheck;
        }
    }
    private void Awake()
    {
        ItemSlots = inventoryUI.GetComponentsInChildren<ItemSlot>();
        LostOfFocus();
        foreach (ItemSlot slot in ItemSlots)
        {
            slot.gameObject.SetActive(false);
        }       
        inventoryUI.SetActive(false);
        GameManager.LoadEvent += LoadItems;
    }
    public void LostOfFocus()
    {
        foreach (ItemSlot slot in ItemSlots)
            slot.LostFocus();
    }
    public void DeleteFull(ItemSlot item)
    {
        emptyTarget = item.NumSlot;
        ItemAdded?.Invoke();
    }
    public void Open()
    {
        inventoryUI.SetActive(true);
        foreach(ItemSlot slot in ItemSlots)
        {
            if (slot.ItemData != null)
                slot.UpdateData();
        }
    }
    public void Close()
    {
        inventoryUI.SetActive(false);
    }
}
