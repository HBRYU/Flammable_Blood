using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject wielder;
    public float accuracy;
    public float speed;
    public float damage;

    public float life;

    public List<string> ignoreCollisionTags;

    public List<string> particleName;
    public List<GameObject> particles;

    public bool hitEnemy_Flag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(0, 0, Random.Range(-1.0f, 1.0f) * accuracy);
        //Debug.Log("SCALE: " + wielder.transform.localScale.x);
        transform.localScale = wielder.transform.localScale;
        rb.velocity = transform.right * transform.localScale.x * speed;
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
        switch (other.tag)
        {
            case "Ground":
                SpawnParticles();
                Destroy(gameObject);
                break;

            default:

                if (!ignoreCollisionTags.Contains(other.tag))
                {
                    if (other.CompareTag("Enemy") && !hitEnemy_Flag)
                    {
                        HitEnemy();
                    }

                    Destroy(gameObject);
                }
                break;
        }

        /////////////////////////// Functions   함수들

        void SpawnParticles()   ///////////// 벽, 땅 등 파편
        {
            switch (other.GetComponent<GroundStats>().particle)
            {
                case "Default":
                    Instantiate(particles[particleName.IndexOf("Default")], transform.position, Quaternion.identity);
                    break;

                default:
                    Debug.Log("ERR: Unknown particle name [" + other.GetComponent<GroundStats>().particle + "]");
                    Instantiate(particles[particleName.IndexOf("Default")], transform.position, Quaternion.identity);
                    break;
            }
        }

        void HitEnemy()     ///////////// 적을 맞췄을 때: 
        {
            GameObject enemy = other.gameObject;

            enemy.GetComponent<EnemyStats>().TakeDamage(damage);
            
            hitEnemy_Flag = true;
        }
    }
}
