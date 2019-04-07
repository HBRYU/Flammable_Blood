using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShell : MonoBehaviour
{
    private Rigidbody2D rb;
    public float lifeTime;
    public float randomAngleOffset;
    public float minSpeed;
    public float randomSpeed;

    [HideInInspector]
    public GameObject wielder;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 angles = new Vector3(0, 0, transform.rotation.z + Random.Range(-randomAngleOffset, randomAngleOffset));
        transform.Rotate(angles);
        transform.localScale = new Vector3(wielder.transform.localScale.x, transform.localScale.y, transform.localScale.z);
        rb.velocity = -transform.right * transform.localScale.x * (minSpeed + Random.Range(-randomSpeed, randomSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
