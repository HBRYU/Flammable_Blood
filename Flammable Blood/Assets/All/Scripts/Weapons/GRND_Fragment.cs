using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRND_Fragment : MonoBehaviour
{
    private GM _GM_;
    public float damage;

    public float life;
    public bool ricochet;

    public List<string> ignoreCollisionTags;

    public List<string> particleName;
    public List<GameObject> particles;

    public GameObject bulletContact;

    public bool hitEnemy_Flag;
    public bool hitPlayer_Flag;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        //Debug.Log("SCALE: " + wielder.transform.localScale.x);

        particleName = _GM_.chunkParticles_names;
        particles = _GM_.chunkParticles;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletCollision(other);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletCollision(collision.collider);
    }

    void BulletCollision(Collider2D other)
    {

        if (other.GetComponent<Particle_Stats>() != null)
            SpawnParticles();
        //Debug.Log(other.gameObject.name);
        switch (other.tag)
        {
            case "Ground":
                Instantiate(bulletContact, transform.position, transform.rotation);
                if(!ricochet)
                    Destroy(gameObject);
                break;

            default:

                if (!ignoreCollisionTags.Contains(other.tag))
                {
                    if (other.CompareTag("Enemy/Hitbox") && !hitEnemy_Flag)
                    {
                        Instantiate(bulletContact, transform.position, transform.rotation);
                        HitEnemy();
                    }

                    if (other.CompareTag("Player/Hitbox") && !hitPlayer_Flag)
                    {
                        //Debug.Log("Hit Player");
                        
                        Instantiate(bulletContact, transform.position, transform.rotation);
                        HitPlayer();
                    }

                    Destroy(gameObject);
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

        void HitEnemy()     ///////////// 적을 맞췄을 때: 
        {
            GameObject enemy = other.transform.parent.gameObject;

            enemy.GetComponent<EnemyStats>().TakeDamage(damage);

            hitEnemy_Flag = true;
        }

        void HitPlayer()     ///////////// 적을 맞췄을 때: 
        {
            GameObject player = _GM_.player;

            player.GetComponent<PlayerStats>().TakeDamage(damage);

            hitPlayer_Flag = true;
        }
    }
}
