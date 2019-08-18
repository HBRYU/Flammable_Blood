using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;
    private Rigidbody2D rb;

    [Header("Droid, Drone etc")]
    public string type;

    public float maxHealth;
    public float health;
    public bool alive = true;

    public bool destroyOnDeath;
    public GameObject[] corpse;
    public float corpseSpawnOffset;

    public bool explodeOnDeath;
    public GameObject explosion;
    public float explosionForce;
    public float explosionRadius;
    public float explosionDuration;


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

        if (type == "Drone")
        {
            GetComponent<DroneMovement>().alertDistance = GetComponent<DroneMovement>().shot_alertDistance;
        }
    }

    public void TakeDamage(float damage, float stunDuration)
    {
        health -= damage;
        Debug.Log("Stunned for " + stunDuration + "sec");
    }

    /*
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
    */

    public void Die()
    {
        if(type == "Droid")
        {
            GetComponent<EnemyMovement>().state = "Idle";
            GetComponent<Enemy1_Attack>().enabled = false;
        }
        if(type == "Drone")
        {
            GetComponent<DroneMovement>().enabled = false;
            GetComponent<DroneAttack>().enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        //rb.simulated = false;

        foreach(GameObject bodyPart in corpse)
        {
            //bodyPart.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-corpseExplosionForce, corpseExplosionForce), Random.Range(-corpseExplosionForce, corpseExplosionForce));
            Instantiate(bodyPart, transform.position + new Vector3(Random.Range(-corpseSpawnOffset, corpseSpawnOffset), Random.Range(-corpseSpawnOffset, corpseSpawnOffset), 0.0f), Quaternion.identity);
        }

        if (explodeOnDeath)
        {
            GameObject thisExplosion = explosion;
            CircleCollider2D thisCollider =  thisExplosion.GetComponent<CircleCollider2D>();
            PointEffector2D thisEffector = thisExplosion.GetComponent<PointEffector2D>();

            thisCollider.radius = explosionRadius;
            thisEffector.forceMagnitude = explosionForce;

            thisExplosion.GetComponent<Explosion>().duration = explosionDuration;

            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        if (destroyOnDeath)
            Destroy(gameObject);
    }
}
