using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRND_C4 : MonoBehaviour
{
    public GameObject explosion;
    public GameObject fire;
    public GameObject fragment;

    public float delay;
    public float throwForce;

    public int fragmentCount;
    public float fragPositionOffset;
    public float fragLifeTime;

    public int fireCount;
    public float firePositionOffset;
    public float fireLifeTime;
    public float burnDuration;
    public float fireDamage;

    public LayerMask whatIsGround;
    public float damage;
    public float damageRange;
    
    public float explosionForce;
    public float explosionRange;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
            Explode();

    }

    public void Explode()
    {
        for (int i = 0; i < fragmentCount; i++)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-fragPositionOffset, fragPositionOffset), transform.position.y + Random.Range(-fragPositionOffset, fragPositionOffset));
            GameObject frag = fragment;
            frag.GetComponent<GRND_Fragment>().damage = damage;
            frag.GetComponent<GRND_Fragment>().life = fragLifeTime;
            Instantiate(frag, spawnPos, Quaternion.identity);
        }

        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, damageRange);

        foreach (Collider2D target in col)
        {
            if (target.GetComponent<EnemyStats>() != null)
            {
                RaycastHit2D wallInSight = Physics2D.Raycast(transform.position, target.transform.position - transform.position, Vector2.Distance(transform.position, target.transform.position), whatIsGround);
                if (wallInSight.collider == null)
                {
                    target.GetComponent<EnemyStats>().TakeDamage(damage);
                }
            }
        }

        for (int i = 0; i < fireCount; i++)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-firePositionOffset, firePositionOffset), transform.position.y + Random.Range(-firePositionOffset, firePositionOffset));
            GameObject thisFire = fire;
            thisFire.GetComponent<Flame>().damage = fireDamage;
            thisFire.GetComponent<Flame>().duration = burnDuration;
            thisFire.GetComponent<Decay>().lifeSpan = fireLifeTime;
            Instantiate(fire, spawnPos, Quaternion.identity);
        }

        explosion.GetComponent<PointEffector2D>().forceMagnitude = explosionForce;
        explosion.GetComponent<CircleCollider2D>().radius = explosionRange;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
