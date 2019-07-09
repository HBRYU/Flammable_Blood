using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;
    private Rigidbody2D rb;

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
        rb = GetComponent<Rigidbody2D>();
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

    public void TakeDamage(float damage, float stunDuration)
    {
        health -= damage;
        Debug.Log("Stunned for " + stunDuration + "sec");
    }

    public void Knockback(float force, string type, float val1)
    {
        switch (type)
        {
            case "simpleX":
                // val1 = wether origin is higher or low X value (1 or -1)
                force = force * val1;
                rb.velocity += new Vector2(rb.velocity.x + force, rb.velocity.y);
                break;

            default:
                Debug.Log("Knockback: Unknown knockback type: " + type + ", calling simpleX [" + gameObject.name + "]");
                force = force * val1;
                rb.velocity += new Vector2(rb.velocity.x + force, rb.velocity.y);
                break;
        }
    }

    public void Die()
    {
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
