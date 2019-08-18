using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimation : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Alerted(bool alerted)
    {
        if (alerted)
            anim.SetBool("Alerted", true);
        else
            anim.SetBool("Alerted", false);
    }
}
