using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRND_Puncture : MonoBehaviour
{
    public GameObject explosion;
    public GameObject fragment;
    public Transform[] spawnPoss;
    public float throwForce;
    public float explosionForce;
    public float delay;
    public float damage;
    public int fragmentCountEach;
    public float fragPositionOffset;
    public float fragLifeTime;
    public bool fragRicochet;

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
        for(int j = 0; j < spawnPoss.Length; j++)
        {
            for (int i = 0; i < fragmentCountEach; i++)
            {
                Vector2 spawnPos = new Vector2(spawnPoss[j].position.x + Random.Range(-fragPositionOffset, fragPositionOffset), spawnPoss[j].position.y + Random.Range(-fragPositionOffset, fragPositionOffset));
                GameObject frag = fragment;
                frag.GetComponent<GRND_Fragment>().damage = damage;
                frag.GetComponent<GRND_Fragment>().life = fragLifeTime;
                frag.GetComponent<GRND_Fragment>().ricochet = fragRicochet;
                Instantiate(frag, spawnPos, Quaternion.identity);
            }
        }
        
        explosion.GetComponent<PointEffector2D>().forceMagnitude = explosionForce;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
