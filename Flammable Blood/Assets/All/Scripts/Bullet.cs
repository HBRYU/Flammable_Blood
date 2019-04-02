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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(0, 0, Random.Range(-1, 1) * accuracy);
        //Debug.Log("SCALE: " + wielder.transform.localScale.x);
        rb.velocity = transform.right * wielder.transform.localScale.x * speed;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if(life <= 0) { Destroy(gameObject); }
    }
}
