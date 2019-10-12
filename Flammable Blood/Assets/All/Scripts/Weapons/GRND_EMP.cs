using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRND_EMP : MonoBehaviour
{
    public float throwForce;
    public float delay;
    public float shockDamage;
    public float shockDuration;
    public float remainDuration;
    public float shockRange;
    public float shockEffect;

    public bool on;

    public GameObject victimEffects;
    public Sprite explosionSprite;
    private Collider2D[] victims;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;

        //Debug.Log(delay);

        if (delay <= 0)
        {
            if (!on)
                GetComponent<AudioSource>().Play();
            Explode();

            GetComponent<SpriteRenderer>().sprite = explosionSprite;
        }
    }

    public void Explode()
    {
        on = true;
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, shockRange);

        victims = col;
        remainDuration -= Time.deltaTime;

        if(remainDuration > 0)
        {
            foreach (Collider2D victim in victims)
            {
                if (victim.GetComponent<EnemyStats>() != null)
                {
                    victim.GetComponent<EnemyStats>().Stunned(shockEffect, shockDuration, shockDamage, victimEffects);
                    victim.GetComponent<EnemyStats>().TakeDamage(shockDamage);
                }
            }
        }
        else
            Destroy(gameObject);

        /*
        on = true;
        shockDuration -= Time.deltaTime;
        if(shockDuration > 0)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, shockRange);

            victims = col;

            foreach(Collider2D victim in victims)
            {
                if(victim.GetComponent<EnemyStats>() != null)
                {
                    victim.GetComponent<EnemyStats>().Stunned(true, shockEffect, victimEffects);
                    victim.GetComponent<EnemyStats>().TakeDamage(shockDamage);
                }
            }
        }

        else
        {
            foreach (Collider2D victim in victims)
            {
                if (victim.GetComponent<EnemyStats>() != null)
                {
                    victim.GetComponent<EnemyStats>().Stunned(false, shockEffect, victimEffects);
                }
            }

            Destroy(gameObject);
        }
        */
    }
}
