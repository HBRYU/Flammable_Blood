
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    private GM _GM_;
    public float speed;
    public GameObject target;
    Vector3 movePos;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GM.GetGM();
        _GM_.AddShootingActiveSwitch("CameraMan");
        movePos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Input.GetKey("f"))
        {
            movePos = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
            //_GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("CameraMan")] = true;
        }
        else
        {
            movePos = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, speed * Time.deltaTime);
            //_GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("CameraMan")] = false;
        }
        
        movePos.z = transform.position.z;
        transform.position = movePos;
    }
}
