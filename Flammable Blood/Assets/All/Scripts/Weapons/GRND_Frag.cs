using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRND_Frag : MonoBehaviour
{
    public GameObject explosion;
    public GameObject fragment;
    public float throwForce;
    public float explosionForce;
    public float delay;
    public float damage;
    public int fragmentCount;
    public float fragPositionOffset;
    public float fragLifeTime;
    

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

        if(delay <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        for(int i = 0; i < fragmentCount; i++)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-fragPositionOffset, fragPositionOffset), transform.position.y + Random.Range(-fragPositionOffset, fragPositionOffset));
            GameObject frag = fragment;
            frag.GetComponent<GRND_Fragment>().damage = damage;
            frag.GetComponent<GRND_Fragment>().life = fragLifeTime;
            Instantiate(frag, spawnPos, Quaternion.identity);
        }
        explosion.GetComponent<PointEffector2D>().forceMagnitude = explosionForce;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
