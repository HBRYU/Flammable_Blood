using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    public float lifeSpan;
    public float randomRange;

    void Start()
    {
        lifeSpan += Random.Range(0, randomRange);
    }

    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }
}
