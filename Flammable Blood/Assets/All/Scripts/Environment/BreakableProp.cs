using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProp : MonoBehaviour
{
    public float health;
    public bool use_breakSFX;
    public AudioClip breakSFX;
    public GameObject[] pieces;
    public GameObject smallExplosion;

    private float deathTimer = 3f;

    void Update()
    {
        if (health <= 0)
        {
            foreach(GameObject g in pieces)
            {
                if (GetComponent<SpriteRenderer>().enabled)
                {
                    Instantiate(g, transform.position, Quaternion.identity);
                }
            }
            if(GetComponent<SpriteRenderer>().enabled)
                Instantiate(smallExplosion, transform.position, transform.rotation);
            if (use_breakSFX)
            {
                if(GetComponent<SpriteRenderer>().enabled)
                    GetComponent<AudioSource>().PlayOneShot(breakSFX);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().simulated = false;
                deathTimer -= Time.deltaTime;
                if (deathTimer <= 0)
                    Destroy(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
