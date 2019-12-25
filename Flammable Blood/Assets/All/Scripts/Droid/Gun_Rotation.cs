using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Rotation : MonoBehaviour
{
    public Transform pivot;
    public Transform gun;

    public GameObject IKs;
    public Transform IK_L;
    public Transform IK_R;

    private bool flip_flag;

    void Update()
    {
        if(GetComponent<PlayerWeaponManager>().activeWeapon != null)
        {
            Arm();
            IKs.transform.parent = GetComponent<PlayerWeaponManager>().activeWeapon.transform;
            IK_L.transform.position = GetComponent<PlayerWeaponManager>().activeWeapon.GetComponent<WeaponStats>().IK_L.position;
            IK_R.transform.position = GetComponent<PlayerWeaponManager>().activeWeapon.GetComponent<WeaponStats>().IK_R.position;
        }
        else
        {
            Disarm();
        }


        if(transform.localScale.x == -1)
        {
            Vector3 scaler = pivot.transform.localScale;
            scaler.x = -1;
            pivot.transform.localScale = scaler;
            /*
            Quaternion rotator = IK_L.rotation;
            rotator.y += pivot.transform.rotation.y;
            rotator.x += pivot.transform.rotation.x;
            IK_L.rotation = rotator;
            rotator = IK_R.rotation;
            rotator.y = 180;
            rotator.x = 180;
            IK_R.rotation = rotator;
            */
        }
        if (transform.localScale.x == 1)
        {
            Vector3 scaler = pivot.transform.localScale;
            scaler.x = 1;
            pivot.transform.localScale = scaler;
            /*
            Quaternion rotator = IK_L.rotation;
            rotator.y = 0;
            rotator.x = 0;
            IK_L.rotation = rotator;
            rotator = IK_R.rotation;
            rotator.y = 0;
            rotator.x = 0;
            IK_R.rotation = rotator;
            */
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (mousePos.x >= Camera.main.transform.position.x)
        {
            Vector3 scaler = pivot.transform.localScale;
            scaler.y = 1;
            pivot.transform.localScale = scaler;
        }
        if (mousePos.x < Camera.main.transform.position.x)
        {
            Vector3 scaler = pivot.transform.localScale;
            scaler.y = -1;
            pivot.transform.localScale = scaler;
        }

        mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(pivot.position);
        if (Input.GetKey("s"))
        {
            objectPos = Camera.main.WorldToScreenPoint(pivot.GetChild(1).position);
        }
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Arm()
    {
        IKs.SetActive(true);
    }
    public void Disarm()
    {
        IKs.SetActive(false);
    }
}
