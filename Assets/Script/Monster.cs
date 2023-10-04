using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    [SerializeField]private List<ItemSO> dropList;
    public static GameObject itemPref;
    private Vector3 moveTo;
    bool readyToAkkack = true;
    protected override void Death()
    {
        var gm = Instantiate(itemPref, transform.position, transform.rotation);
        gm?.GetComponent<Item>().GetData(dropList[Random.Range(0, dropList.Count)]);
        Destroy(gameObject);
    }
    public virtual void Aggro(Vector3 agroPos)
    {
        moveTo = agroPos;
        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, agroPos, step);
        animator.SetBool("IsAgro", true);
        if (agroPos.x < transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
        else if(agroPos.x > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
    }
    protected override void Start()
    {
        base.Start();
        Damage = 5;
        moveTo = transform.position;
        speed = 1;
    }
    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(1.0f);
        readyToAkkack = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() is Player player && readyToAkkack)
        {
            player.TakeDmg(Damage);
            readyToAkkack = false;
            StartCoroutine(AttackCD());
        }
    }
    void Update()
    {
        if (moveTo != gameObject.transform.position)
            Aggro(moveTo);
        else
            animator.SetBool("IsAgro", false);
    }
}
