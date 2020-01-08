using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
    private bool BC_spawn_flag;

    public bool hitEnemy_Flag;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(0, 0, Random.Range(-1.0f, 1.0f) * accuracy);
        //Debug.Log("SCALE: " + wielder.transform.localScale.x);
        transform.localScale = wielder.transform.localScale;
        rb.velocity = transform.right * speed;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        BulletCollision(other.collider);
    }

    void BulletCollision(Collider2D other)
    {
        
        if(other.GetComponent<Particle_Stats>() != null)
        {
            SpawnParticles();
            
        }

        if(other.GetComponent<BulletImpact_Stats>() != null)
        {
            GameObject contact = Instantiate(bulletContact, transform.position, transform.rotation);
            List<AudioClip> sfx = new List<AudioClip>();
            for (int i = 0; i < contact.GetComponent<BulletContact>().SFXs_names.Count; i++)
            {
                if (contact.GetComponent<BulletContact>().SFXs_names[i] == other.GetComponent<BulletImpact_Stats>().type)
                    sfx.Add(contact.GetComponent<BulletContact>().SFXs[i]);
            }
            if (sfx.Count > 0)
                contact.GetComponent<BulletContact>().SFX = sfx[Random.Range(0, sfx.Count - 1)];
            else
                contact.GetComponent<BulletContact>().SFX = null;
            BC_spawn_flag = true;
        }
        if(other.GetComponentInParent<BulletImpact_Stats>() != null)
        {
            GameObject contact = Instantiate(bulletContact, transform.position, transform.rotation);
            List<AudioClip> sfx = new List<AudioClip>();
            for (int i = 0; i < contact.GetComponent<BulletContact>().SFXs_names.Count; i++)
            {
                if (contact.GetComponent<BulletContact>().SFXs_names[i] == other.GetComponentInParent<BulletImpact_Stats>().type)
                    sfx.Add(contact.GetComponent<BulletContact>().SFXs[i]);
            }
            if (sfx.Count > 0)
                contact.GetComponent<BulletContact>().SFX = sfx[Random.Range(0, sfx.Count - 1)];
            else
                contact.GetComponent<BulletContact>().SFX = null;
            BC_spawn_flag = true;
        }

        switch (other.tag)
        {
            case "Ground":
                if(!BC_spawn_flag)
                    Instantiate(bulletContact, transform.position, transform.rotation);
                Destroy(gameObject);
                break;

            default:

                if (!ignoreCollisionTags.Contains(other.tag))
                {
                    if ((other.CompareTag("Enemy/Hitbox")) || (other.tag.Contains("Enemy/")) && !hitEnemy_Flag)
                    {
                        Instantiate(bulletContact, transform.position, transform.rotation);
                        HitEnemy();
                    }

                    if((other.gameObject.layer == LayerMask.NameToLayer("Prop/Interactive")))
                    {
                        Instantiate(bulletContact, transform.position, transform.rotation);
                        if(other.GetComponent<BreakableProp>() != null)
                        {
                            other.GetComponent<BreakableProp>().TakeDamage(damage);
                        }
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
            GameObject enemy;
            if (other.CompareTag("Enemy/Hitbox"))
                enemy = other.transform.parent.gameObject;
            else
                enemy = other.gameObject;
            enemy.GetComponent<EnemyStats>().TakeDamage(damage);
            
            hitEnemy_Flag = true;
        }
    }
}
