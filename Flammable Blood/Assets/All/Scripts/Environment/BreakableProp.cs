using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProp : MonoBehaviour
{
    public float health;
    public GameObject[] pieces;
    public GameObject smallExplosion;

    void Update()
    {
        if (health <= 0)
        {
            foreach(GameObject g in pieces)
            {
                Instantiate(g, transform.position, Quaternion.identity);
            }
            Instantiate(smallExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
