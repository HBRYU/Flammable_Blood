using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private PlayerMove pm;

    private bool hadJumpeds;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMove>();
    }


    void Update()
    {
        //////////////////////////// Movement Input     좌 우 움직임 인풋
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey("left shift"))
            {
                anim.SetInteger("Speed", 2);
            }
            else
            {
                anim.SetInteger("Speed", 1);
            }
        }
        else { anim.SetInteger("Speed", 0); }

        ///////////////////////// Jump Input    점프 인풋
        bool onGround;
        if (pm.onGround != null) { onGround = true; }
        else { onGround = false; }

        anim.SetBool("OnGround", onGround);
        anim.SetFloat("YVel", rb.velocity.y);
    }
    

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }

    public void Shoot(GameObject gun, bool shoot, bool repeat)
    {
        Debug.Log("Shoot(ac)");
        if (repeat == true)
        {
            if(shoot == true)
            {
                gun.GetComponent<Animator>().SetBool("Shooting", true);
                anim.SetBool("Shooting", true);
            }
            else
            {
                gun.GetComponent<Animator>().SetBool("Shooting", false);
                anim.SetBool("Shooting", false);
            }
        }
        else
        {
            Debug.Log("Triggered");
            gun.GetComponent<Animator>().SetTrigger("Shoot");
            anim.SetTrigger("Shoot");
        }
    }

    public void Reload()
    {
        anim.SetTrigger("Reload");
    }

    public void Aim(bool aim)
    {
        if(aim == true)
        {
            anim.SetBool("Aim", true);
        }
        else
        {
            anim.SetBool("Aim", false);
        }
    }

}
