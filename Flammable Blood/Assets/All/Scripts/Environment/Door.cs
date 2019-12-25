using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool opened;
    private bool opened_flag;

    public float delay;
    private float delay_timer;

    public AudioClip SFX;

    private void Start()
    {
        if (opened)
            Open();
    }

    private void Update()
    {
        if (opened_flag)
        {
            delay_timer += Time.deltaTime;
            if(delay_timer >= delay)
            {
                delay_timer = 0;
                opened_flag = false;
            }
        }
    }

    public void Open()
    {
        if (!opened_flag)
        {
            GetComponent<Animator>().SetBool("Opened", true);
            opened = true;
            opened_flag = true;
            GetComponent<AudioSource>().PlayOneShot(SFX);
        }
    }
    public void Close()
    {
        if (!opened_flag)
        {
            GetComponent<Animator>().SetBool("Opened", false);
            opened = false;
            opened_flag = true;
            GetComponent<AudioSource>().PlayOneShot(SFX);
        }
    }
}
