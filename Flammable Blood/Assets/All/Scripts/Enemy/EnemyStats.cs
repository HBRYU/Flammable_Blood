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
    public bool deathCount;

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

    //STUN effect
    public bool resistStun;
    [Header("damage * (1 - resistance) = applied damage")]
    public float stunResistance;
    
    private float stunDuration;
    private float stunDamage;
    private GameObject stun_effect;

    //FLAME effect
    public bool resistBurn;
    [Header("damage * (1 - resistance) = applied damage")]
    public float burnResistance;

    private float burnDuration;
    private float burnDamage;
    private GameObject burn_effect;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        rb = GetComponent<Rigidbody2D>();

        //TakeDamage(999999999);
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

        //Burn effect
        if(burnDuration > 0)
        {
            TakeDamage(burnDamage * Time.deltaTime * (1 - burnResistance), true);
            burnDuration -= Time.deltaTime;
        }
        else
        {
            if (burn_effect != null)
                Destroy(burn_effect);
            burnDuration = 0;
            burnDamage = 0;
        }

        //Stun effect
        if(stunDuration > 0)
        {
            TakeDamage(stunDamage * Time.deltaTime * (1 - stunResistance), true);
            stunDuration -= Time.deltaTime;
        }
        else
        {
            if (GetComponent<DroneAttack>() != null)
            {
                DroneMovement move = GetComponent<DroneMovement>();
                move.hover = true;

                GetComponent<DroneAttack>().enabled = true;
            }

            if (GetComponent<Drone2Attack>() != null)
            {
                DroneMovement move = GetComponent<DroneMovement>();
                move.hover = true;

                GetComponent<Drone2Attack>().enabled = true;
            }

            if (GetComponent<EnemyMovement>() != null)
            {
                EnemyMovement move = GetComponent<EnemyMovement>();

                move.active = true;
                move.move = true;

                GetComponent<Enemy1_Attack>().enabled = true;
            }
            if (type == "Turret")
            {
                GetComponent<Turret1>().enabled = true;
            }

            if (stun_effect != null)
                Destroy(stun_effect);
            stunDamage = 0;
            stunDuration = 0;
        }
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log("[" + gameObject.name + "] : Taken damage: " + damage);
        health -= damage;

        if ((type == "Drone" ) || (type == "Drone2"))
        {
            GetComponent<DroneMovement>().alertDistance = GetComponent<DroneMovement>().shot_alertDistance;

            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, GetComponent<DroneMovement>().alertDistance);
            if (col != null)
            {
                foreach (Collider2D obj in col)
                {
                    if (obj.GetComponent<DroneMovement>() != null && Physics2D.Raycast(transform.position, obj.transform.position, Vector2.Distance(transform.position, obj.transform.position), GetComponent<DroneMovement>().whatIsGround).collider == null)
                    {
                        obj.GetComponent<DroneMovement>().alertDistance = obj.GetComponent<DroneMovement>().shot_alertDistance;
                    }
                    if (obj.GetComponent<Turret1>() != null && Physics2D.Raycast(transform.position, obj.transform.position, Vector2.Distance(transform.position, obj.transform.position), GetComponent<DroneMovement>().whatIsGround).collider == null)
                    {
                        obj.GetComponent<Turret1>().alertDistance = obj.GetComponent<Turret1>().alertDistance_shot;
                    }
                }
            } 
        }
        if((type == "Turret"))
        {
            GetComponent<Turret1>().alertDistance = GetComponent<Turret1>().alertDistance_shot;
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, GetComponent<Turret1>().alertDistance);
            if (col != null)
            {
                foreach (Collider2D obj in col)
                {
                    if (obj.GetComponent<DroneMovement>() != null && Physics2D.Raycast(transform.position, obj.transform.position, Vector2.Distance(transform.position, obj.transform.position), GetComponent<Turret1>().whatIsGround).collider == null)
                    {
                        obj.GetComponent<DroneMovement>().alertDistance = obj.GetComponent<DroneMovement>().shot_alertDistance;
                    }
                    if (obj.GetComponent<Turret1>() != null && Physics2D.Raycast(transform.position, obj.transform.position, Vector2.Distance(transform.position, obj.transform.position), GetComponent<Turret1>().whatIsGround).collider == null)
                    {
                        obj.GetComponent<Turret1>().alertDistance = obj.GetComponent<Turret1>().alertDistance_shot;
                    }
                }
            }
        }
    }

    public void TakeDamage(float damage, bool NoAlert)
    {
        //Debug.Log("[" + gameObject.name + "] : Taken damage: " + damage);
        health -= damage;

        if ((type == "Drone") || (type == "Drone2"))
        {
            GetComponent<DroneMovement>().alertDistance = GetComponent<DroneMovement>().shot_alertDistance;
        }
        if ((type == "Turret"))
        {
            GetComponent<Turret1>().alertDistance = GetComponent<Turret1>().alertDistance_shot;
        }
    }

    public void Stunned(float randomizer, float duration, float damage, GameObject effect)
    {
        if (!resistStun)
        {
            if (GetComponent<DroneAttack>() != null)
            {
                DroneMovement move = GetComponent<DroneMovement>();
                move.hoverForce += Random.Range(-randomizer, randomizer);
                move.hoverHeight += Random.Range(-randomizer, randomizer);
                move.speed += Random.Range(-randomizer, randomizer);
                move.speed += Random.Range(-randomizer, randomizer);
                move.acceleration += Random.Range(-randomizer, randomizer);
                move.retreat_acceleration += Random.Range(-randomizer, randomizer);

                move.hover = false;

                GetComponent<DroneAttack>().enabled = false;
                GetComponent<DroneAttack>().aimSpeed += Random.Range(-randomizer, randomizer);
            }

            if (GetComponent<Drone2Attack>() != null)
            {
                DroneMovement move = GetComponent<DroneMovement>();
                move.hoverForce += Random.Range(-randomizer, randomizer);
                move.hoverHeight += Random.Range(-randomizer, randomizer);
                move.speed += Random.Range(-randomizer, randomizer);
                move.speed += Random.Range(-randomizer, randomizer);
                move.acceleration += Random.Range(-randomizer, randomizer);
                move.retreat_acceleration += Random.Range(-randomizer, randomizer);

                move.hover = false;

                GetComponent<Drone2Attack>().enabled = false;
            }

            if (GetComponent<EnemyMovement>() != null)
            {
                EnemyMovement move = GetComponent<EnemyMovement>();

                //Debug.Log("ZAAAP");
                move.active = false;
                move.move = false;
                rb = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(0, rb.velocity.y);

                GetComponent<Enemy1_Attack>().enabled = false;
                GetComponent<Enemy1_Animation>().Move(false);
            }

            if (type == "LandMine")
            {
                GetComponent<LandMine>().explode = true;
            }

            if (type == "Turret")
            {
                GetComponent<Turret1>().enabled = false;
            }

            if (stunDuration <= 0)
            {
                stun_effect = Instantiate(effect, transform);
                stun_effect.transform.parent = transform;
                stun_effect.transform.localPosition = new Vector2(0, 0);
            }
            if (duration > stunDuration)
                stunDuration = duration;
            if (damage > stunDamage)
                stunDamage = damage;
        }
    }

    public void Burn(float duration, float damage, GameObject effect)
    {
        if (!resistBurn)
        {
            if (burnDuration <= 0)
            {
                burn_effect = Instantiate(effect, transform);
                burn_effect.transform.parent = transform;
                burn_effect.transform.localPosition = new Vector2(0, 0);
            }
            if (duration > burnDuration)
                burnDuration = duration;
            if (damage > burnDamage)
                burnDamage = damage;
        }
    }

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
        if (type == "Drone2")
        {
            GetComponent<DroneMovement>().enabled = false;
            Destroy(GetComponent<Drone2Attack>().cp);
            GetComponent<Drone2Attack>().enabled = false;
        }
        if(type == "LandMine")
        {
            GetComponent<LandMine>().delay = 0;
            GetComponent<LandMine>().explode = true;
        }

        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(0, 0);
        //rb.simulated = false;

        if(corpse != null)
        {
            foreach (GameObject bodyPart in corpse)
            {
                //bodyPart.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-corpseExplosionForce, corpseExplosionForce), Random.Range(-corpseExplosionForce, corpseExplosionForce));
                Instantiate(bodyPart, transform.position + new Vector3(Random.Range(-corpseSpawnOffset, corpseSpawnOffset), Random.Range(-corpseSpawnOffset, corpseSpawnOffset), 0.0f), Quaternion.identity);
            }
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
        {
            Destroy(gameObject);
            if (deathCount)
                _GM_.killCount++;
        }
    }
}
