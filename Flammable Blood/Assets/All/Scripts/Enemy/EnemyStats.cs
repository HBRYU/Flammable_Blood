using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;

    public float maxHealth;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("[" + gameObject.name + "] : Taken damage: " + damage);
        health -= damage;
    }

    public void Die()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        GetComponent<EnemyMovement>().state = "Idle";
    }
}
