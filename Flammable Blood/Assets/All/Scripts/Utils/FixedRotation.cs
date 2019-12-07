using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    private Quaternion initRot;
    private Vector3 initScale;

    private bool rot_flag;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;
        initRot = transform.rotation;
        initScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = initRot;
        if (transform.parent.localScale.x == -1 && !rot_flag)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            rot_flag = true;
        }
        if(transform.parent.localScale.x == 1 && rot_flag)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            rot_flag = false;
        }
    }
}
