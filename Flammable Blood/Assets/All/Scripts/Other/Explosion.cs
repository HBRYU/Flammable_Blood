using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration;
    public float lifeSpan;

    void FixedUpdate()
    {
        duration -= 1;
        lifeSpan -= 1;

        if(lifeSpan <= 0)
        {
            Destroy(gameObject);
        }

        if(duration <= 0)
        {
            GetComponent<PointEffector2D>().enabled = false;
        }
    }
}
