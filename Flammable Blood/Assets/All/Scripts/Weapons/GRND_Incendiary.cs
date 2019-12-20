using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRND_Incendiary : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip explosionSFX;
    public GameObject fire;
    public float throwForce;
    public float explosionForce;
    public float delay;
    public float damage;
    public int fireCount;
    public float firePositionOffset;
    public float fireLifeTime;
    public float burnDuration;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;

        //Debug.Log(delay);

        if (delay <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        for (int i = 0; i < fireCount; i++)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-firePositionOffset, firePositionOffset), transform.position.y + Random.Range(-firePositionOffset, firePositionOffset));
            GameObject thisFire = fire;
            thisFire.GetComponent<Flame>().damage = damage;
            thisFire.GetComponent<Flame>().duration = burnDuration;
            thisFire.GetComponent<Decay>().lifeSpan = fireLifeTime;
            Instantiate(fire, spawnPos, Quaternion.identity);
        }
        explosion = Instantiate(explosion, transform.position, transform.rotation);
        explosion.GetComponent<PointEffector2D>().forceMagnitude = explosionForce;
        explosion.GetComponent<Explosion>().SFXs = new List<AudioClip>();
        explosion.GetComponent<Explosion>().SFXs.Add(explosionSFX);
        Destroy(gameObject);
    }
}
