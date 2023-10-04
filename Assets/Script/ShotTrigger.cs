using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrigger : MonoBehaviour
{
    public bool isRanged = false;
    public bool flip = false;
    public Monster targetMonster { get; private set; }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Monster>() != null)
        { 
            isRanged = true;
            targetMonster = collision.gameObject.GetComponent<Monster>();
        }
        if(collision.gameObject.transform.position.x < transform.position.x)
            flip = true;
        else
            flip = false;  
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Monster>() != null)
            isRanged= false;
    }
}
