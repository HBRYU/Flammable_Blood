using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private GM _GM_;

    public GameObject shield;
    public AudioClip shieldOnSFX;
    public bool on;
    private bool on_flag;
    public float duration;
    private float duration_timer;
    public float delay;
    private float delay_timer;

    void Start()
    {
        //_GM_ = GM.GetGM();
        //_GM_.AddShootingActiveSwitch("PlayerShield");
        //_GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("PlayerShield")] = true;
        shield.GetComponent<BoxCollider2D>().enabled = false;
        shield.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKey("s") && on && delay_timer >= delay && on_flag)
        {
            //_GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("PlayerShield")] = false;
            if (shield.GetComponent<BoxCollider2D>().enabled == false)
                shield.GetComponent<AudioSource>().PlayOneShot(shieldOnSFX);

            shield.GetComponent<BoxCollider2D>().enabled = true;
            shield.GetComponent<Animator>().SetBool("Shield", true);
            duration_timer += Time.deltaTime;
            if(duration_timer >= duration)
            {
                duration_timer = 0;
                delay_timer = 0;
                on_flag = false;
            }
        }
        else
        {
            //_GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("PlayerShield")] = true;
            duration_timer = 0;
            shield.GetComponent<BoxCollider2D>().enabled = false;
            shield.GetComponent<Animator>().SetBool("Shield", false);
            delay_timer += Time.deltaTime;
        }

        if (!Input.GetKey("s"))
            on_flag = true;

        if (Input.GetKeyDown("v"))
        {
            GM.DisplayText("Shield Toggled", false);
            on = !on;
        }
    }
}
