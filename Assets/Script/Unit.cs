using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected int maxHealth;
    public int Damage { get; protected set; }
    public int CurrHealth { get; protected set; }
    public float speed { get; protected set; }
    public Animator animator { get; protected set; }
    protected HBar healhBar;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        healhBar = GetComponentInChildren<HBar>();
    }
    public void TakeDmg(int dmg)
    {    
        CurrHealth -= dmg;
        if(CurrHealth > 0 )
        {
            healhBar.ChangeHPBar(maxHealth, CurrHealth);
        }
        if (CurrHealth <= 0)
        {
            CurrHealth = 0;
            Death();
        }
    }
    protected virtual void Death() 
    {
        Destroy(gameObject);
    }
    protected virtual void Start()
    {
        maxHealth = 100;
        CurrHealth = 100;
        healhBar.ChangeHPBar(maxHealth, CurrHealth);
    }
}
