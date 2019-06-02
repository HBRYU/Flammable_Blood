using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;

    public float maxHealth;
    public float health;
    public bool alive = true;

    public GameObject[] corpse;
    public float corpseSpawnOffset;
    public float corpseExplosionForce;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && alive)
        {
            alive = false;
            Die();
            health = 0;
        }
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log("[" + gameObject.name + "] : Taken damage: " + damage);
        health -= damage;
    }

    public void Die()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(0, 0, 0, 1);
        sr.enabled = false;

        GetComponent<EnemyMovement>().state = "Idle";

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.simulated = false;

        foreach(GameObject bodyPart in corpse)
        {
            Instantiate(bodyPart, transform.position + new Vector3(Random.Range(-corpseSpawnOffset, corpseSpawnOffset), Random.Range(-corpseSpawnOffset, corpseSpawnOffset), 0.0f), Quaternion.identity);
            bodyPart.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(-corpseExplosionForce, corpseExplosionForce), Random.Range(-corpseExplosionForce, corpseExplosionForce));
        }
    }
}
