using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour
{
    /// <summary>
    /// 플레이어 애니메이션 담당
    /// -플레이어 움직임
    /// -플레이어와 해당 아이템 애니메이션 싱크로 맞춰 실제 플레이
    /// -PlayerMove 와 PlayerWeaponManager 등의 스크립트에서 명령하여 애니메이션 플레이
    /// </summary>

    private GM _GM_;

    private Animator anim;
    private Rigidbody2D rb;

    private PlayerMove pm;

    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMove>();
    }


    void Update()
    {
        if (pm.crouched)
        {
            anim.SetBool("Crouched", true);
        }
        else
        {
            anim.SetBool("Crouched", false);

            Move();

        }

        ///////////////////////// Jump Input    점프 인풋
        bool onGround;

        if (pm.onGround != null)
            onGround = true;
        else
            onGround = false;

        anim.SetBool("OnGround", onGround);
        anim.SetFloat("YVel", rb.velocity.y);
    }
    
    void Move()
    {
        if (pm.faceMouseMovement)
        {
            int moveInput_Int;

            if (Input.GetAxis("Horizontal") > 0)
                moveInput_Int = 1;
            else if (Input.GetAxis("Horizontal") < 0)
                moveInput_Int = -1;
            else
                moveInput_Int = 0;

            if(Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.SetInteger("Speed", 0);
            }
            else if (transform.localScale.x != moveInput_Int && moveInput_Int != 0)
            {
                //anim.SetBool("MoonWalk", true);
                if (Input.GetKey("left shift"))
                    anim.SetInteger("Speed", -2);
                else
                    anim.SetInteger("Speed", -1);
                }
            else
            {
                //anim.SetBool("MoonWalk", false);
                if (Input.GetKey("left shift"))
                    anim.SetInteger("Speed", 2);
                else
                    anim.SetInteger("Speed", 1);
            }
        }
        else
        {
            //////////////////////////// Movement Input     좌 우 움직임 인풋
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (Input.GetKey("left shift"))
                    anim.SetInteger("Speed", 2);
                else
                    anim.SetInteger("Speed", 1);
            }
            else
                anim.SetInteger("Speed", 0);

        }
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }

    public void Shoot(GameObject gun, bool shoot, bool repeat)
    {
        if (repeat == true)
        {
            if(shoot == true && _GM_.shooting_active)
            {
                try
                {
                    gun.GetComponent<Animator>().SetBool("Shooting", true);
                }
                catch
                { }
                anim.SetBool("Shooting", true);
            }
            else
            {
                try
                {
                    gun.GetComponent<Animator>().SetBool("Shooting", false);
                }
                catch
                { }
                anim.SetBool("Shooting", false);
            }
        }
        else
        {
            try
            {
                gun.GetComponent<Animator>().SetTrigger("Shoot");
            }
            catch
            { }
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
            anim.SetBool("Aim", true);
        else
            anim.SetBool("Aim", false);
    }

}
