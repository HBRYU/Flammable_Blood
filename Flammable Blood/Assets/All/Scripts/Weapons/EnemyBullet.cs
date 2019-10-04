using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GM _GM_;
    private Rigidbody2D rb;
    public GameObject wielder;
    public float accuracy;
    public float speed;
    public float damage;

    public float life;

    public List<string> ignoreCollisionTags;

    public List<string> particleName;
    public List<GameObject> particles;
    public GameObject bulletContact;
    public bool hitPlayer_Flag;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        rb = GetComponent<Rigidbody2D>();

        transform.Rotate(0, 0, Random.Range(-1.0f, 1.0f) * accuracy);
        //Debug.Log("SCALE: " + wielder.transform.localScale.x);

        transform.localScale = wielder.transform.localScale;
        rb.velocity = transform.right * transform.localScale.x * speed;

        particleName = _GM_.chunkParticles_names;
        particles = _GM_.chunkParticles;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if(life <= 0) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletCollision(other);
    }

    void BulletCollision(Collider2D other)
    {
        
        if (other.GetComponent<Particle_Stats>() != null)
            SpawnParticles();

        switch (other.tag)
        {
            case "Ground":
                Instantiate(bulletContact, transform.position, transform.rotation);
                Destroy(gameObject);
                break;

            default:

                if (!ignoreCollisionTags.Contains(other.tag))
                {
                    if (other.CompareTag("Player/Hitbox") && !hitPlayer_Flag)
                    {
                        //Debug.Log("Hit Player");
                        Instantiate(bulletContact, transform.position, transform.rotation);
                        HitPlayer();
                    }
                    Destroy(gameObject);
                    //Debug.Log("Bullet Destroyed");
                }
                break;
        }

        /////////////////////////// Functions   함수들

        void SpawnParticles()   ///////////// 벽, 땅 등 파편
        {
            if (particleName.Contains(other.GetComponent<Particle_Stats>().particle))
            {
                Instantiate(particles[particleName.IndexOf(other.GetComponent<Particle_Stats>().particle)], transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("ERR: Unknown particle name [" + other.GetComponent<Particle_Stats>().particle + "]");
                Instantiate(particles[particleName.IndexOf("Default")], transform.position, Quaternion.identity);
            }

        }

        void HitPlayer()     ///////////// 적을 맞췄을 때: 
        {
            GameObject Player = other.gameObject.transform.parent.gameObject;

            Player.GetComponent<PlayerStats>().TakeDamage(damage);
            
            hitPlayer_Flag = true;
        }
    }
}
