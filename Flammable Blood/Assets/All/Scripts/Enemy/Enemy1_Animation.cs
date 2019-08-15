using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Animation : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(bool enable)
    {
        if (enable)
            anim.SetInteger("Speed", 1);
        else
            anim.SetInteger("Speed", 0);
    }
    public void Shoot()
    {
        anim.SetTrigger("Shoot");
    }
}
