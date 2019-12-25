using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Rotation : MonoBehaviour
{
    public Transform pivot;
    public Transform gun;
    public float rotSpeed;

    public GameObject IKs;
    public Transform IK_L;
    public Transform IK_R;

    public Transform L_Hand;
    public Transform R_Hand;

    private bool flip_flag;

    void Update()
    {
        if(GetComponent<PlayerWeaponManager>().activeWeapon != null)
        {
            Arm();
            IKs.transform.position = GetComponent<PlayerWeaponManager>().activeWeapon.transform.position;
            //IKs.transform.parent = GetComponent<PlayerWeaponManager>().activeWeapon.transform;
            //IK_L.transform.position = GetComponent<PlayerWeaponManager>().activeWeapon.GetComponent<WeaponStats>().IK_L.position;
            //IK_R.transform.position = GetComponent<PlayerWeaponManager>().activeWeapon.GetComponent<WeaponStats>().IK_R.position;
        }
        else
        {
            Disarm();
            IKs.transform.position = new Vector3(0, 0, 0);
        }
        //IK_L.transform.position = new Vector3(0, 0, 0);

        if(transform.localScale.x == -1)
        {
            Vector3 scaler = pivot.transform.localScale;
            scaler.x = -1;
            pivot.transform.localScale = scaler;
            
            Quaternion rotator = L_Hand.localRotation;
            rotator.y = 180;
            rotator.x = 180;
            rotator.z = IK_L.rotation.z;
            L_Hand.localRotation = rotator;
            rotator = R_Hand.localRotation;
            rotator.y = 180;
            rotator.x = 180;
            rotator.z = IK_R.rotation.z;
            R_Hand.localRotation = rotator;
            
        }
        if (transform.localScale.x == 1)
        {
            Vector3 scaler = pivot.transform.localScale;
            scaler.x = 1;
            pivot.transform.localScale = scaler;

            Quaternion rotator = L_Hand.localRotation;
            rotator.y = 0;
            rotator.x = 0;
            rotator.z = IK_L.rotation.z;
            L_Hand.localRotation = rotator;
            rotator = R_Hand.localRotation;
            rotator.y = 0;
            rotator.x = 0;
            rotator.z = IK_R.rotation.z;
            R_Hand.localRotation = rotator;

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
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), rotSpeed * Time.deltaTime);
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
