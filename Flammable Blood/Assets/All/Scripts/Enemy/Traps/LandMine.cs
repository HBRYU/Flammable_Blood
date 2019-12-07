using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    public bool explode;

    public LayerMask whatIsGround;
    public GameObject warning;

    public float fragDamage;
    public float damage;
    public float delay;
    public float damageRange;
    public GameObject fragment;
    public int fragmentCount;
    public float fragPositionOffset;
    public float fragLifeTime;
    public GameObject explosion;
    public float explosionForce;

    void Update()
    {
        if (explode)
            Explode();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player/Hitbox"))
        {
            if (!explode)
            {
                GetComponent<AudioSource>().Play();
                warning.SetActive(true);
            }
            explode = true;
        }
    }

    public void Explode()
    {
        delay -= Time.deltaTime;
        if(delay <= 0)
        {
            for (int i = 0; i < fragmentCount; i++)
            {
                Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-fragPositionOffset, fragPositionOffset), transform.position.y + Random.Range(-fragPositionOffset, fragPositionOffset));
                GameObject frag = fragment;
                frag.GetComponent<GRND_Fragment>().damage = fragDamage;
                frag.GetComponent<GRND_Fragment>().life = fragLifeTime;
                Instantiate(frag, spawnPos, Quaternion.identity);
            }
            explosion.GetComponent<PointEffector2D>().forceMagnitude = explosionForce;
            Instantiate(explosion, transform.position, transform.rotation);

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

                if (target.GetComponent<PlayerStats>() != null)
                {
                    RaycastHit2D wallInSight = Physics2D.Raycast(transform.position, target.transform.position - transform.position, Vector2.Distance(transform.position, target.transform.position), whatIsGround);
                    if (wallInSight.collider == null)
                    {
                        target.GetComponent<PlayerStats>().TakeDamage(damage);
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
