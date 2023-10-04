using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Unit
{
    private ItemSlot bullets;
    [SerializeField]private TextMeshProUGUI BulletUI;
    public event Action DeathSubEvent;
    public static Inventory Inventory { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        Inventory = GetComponent<Inventory>();
        Inventory.ItemAdded += SlotCheck;
        GameManager.LoadEvent += LoadHealth;
    }
    public void LoadHealth(SavedData data)
    {
        CurrHealth = data.health;
        healhBar.ChangeHPBar(maxHealth, CurrHealth);
    }
    protected override void Death()
    {
        Inventory.Clean();
        base.Death();
        DeathSubEvent?.Invoke();
        CurrHealth = 100;
    }
    public bool TakeShot()
    {
        if(bullets != null && bullets.ItemData?.Amount > 0)
        {
            Inventory.DeleteOne(bullets.ItemData);
            return true;
        }else
            return false;
    }
    private void SlotCheck()
    {
        if (Array.Find(Inventory.ItemSlots, x => x.ItemData?.Type == itemType.bullet) is ItemSlot slot)
            bullets = slot;
        else
            bullets = null;
    }
    protected override void Start()
    {
        base.Start();
        Damage = 20;
        speed = 5.0f;
    }
    void Update()
    {
        if (bullets != null)
            BulletUI.text = bullets.ItemData.Amount.ToString();
        else
            BulletUI.text = "0";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Item>() is Item item)
        {
            if(Inventory.AddItem(item.itemData))
                Destroy(item.gameObject);
        }
    }
}
