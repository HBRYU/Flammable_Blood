using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    private Quaternion initRot;
    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = initRot;
    }
}
