using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScale : MonoBehaviour
{
    public Transform reference;
    private Vector3 lastRef;

    void Start()
    {
        lastRef = reference.localScale;
    }

    void Update()
    {
        if(reference.localScale.x != lastRef.x)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        lastRef = reference.localScale;
    }
}
