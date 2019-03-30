using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private PlayerMove pm;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
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

        if (Input.GetKeyDown("w") && onGround == true)
        {
            anim.SetTrigger("Jump");
        }

        //////////////////////// Mouse Input    마우스 인풋
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("Shooting", true);
        }
        else
        {
            anim.SetBool("Shooting", false);
        }


        anim.SetBool("OnGround", onGround);
        anim.SetFloat("YVel", rb.velocity.y);
    }
}
