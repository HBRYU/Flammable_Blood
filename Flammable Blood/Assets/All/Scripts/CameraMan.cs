
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    public float speed;
    public GameObject target;
    Vector3 movePos;

    // Start is called before the first frame update
    void Start()
    {
        movePos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePos = Vector3.Lerp(transform.position, target.transform.position, speed);
        movePos.z = transform.position.z;
        transform.position = movePos;
    }
}
