using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConroll : MonoBehaviour
{
    float speed;
    Animator animator;
    Player Data;
    [SerializeField]ShotTrigger shotTrigger;
    [SerializeField]InputActionReference joystic;
    void Start()
    {
        Data = GetComponent<Player>();
        animator = Data.animator;
    }
    public void Shoot()
    {
        if(shotTrigger.isRanged && Data.TakeShot())
        {
            animator.SetTrigger("Shoot");  
            GetComponent<SpriteRenderer>().flipX = shotTrigger.flip;
            shotTrigger.targetMonster.TakeDmg(Data.Damage);
        }
    }
    void FixedUpdate()
    {
        Camera.main.transform.position = transform.position + new Vector3(0,0,-10);
        Vector2 moveDir = joystic.action.ReadValue<Vector2>();
        #region testKeyboardContoll
        float horisontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 keyboardInput = new Vector2(horisontalInput, verticalInput);
        if (keyboardInput != Vector2.zero) moveDir = keyboardInput;
        #endregion
        speed = Data.speed;
        //GetComponent<Rigidbody2D>().AddForce(moveDir);
        transform.Translate(moveDir * speed * Time.deltaTime);
        animator.SetFloat("MoveSpeed", Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y));
        if(!Data.animator.GetCurrentAnimatorStateInfo(1).IsTag("shot"))
        {
            if (moveDir.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else if (moveDir.x > 0)
                GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
