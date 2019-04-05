using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private MasterWeaponManagement _WM_;

    public GameObject gunFolder;
    public int maxGunCount;
    private Animator anim;

    public List<GameObject> guns;
    public GameObject activeGun;
    // Start is called before the first frame update
    void Start()
    {
        _WM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<MasterWeaponManagement>();

        anim = GetComponent<Animator>();

        if(guns.Count > maxGunCount)
        {
            while(guns.Count > maxGunCount)
            {
                guns[guns.Count - 1].GetComponent<WeaponStats>().Drop();
                guns.Remove(guns[guns.Count - 1]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(guns.Count == 0) { activeGun = null; }

        if(activeGun == null)
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Default [Type-0]"), 100);
            for(int i = 0;  i < anim.layerCount; i++)
            {
                if(anim.GetLayerIndex("Default [Type-0]") != i)
                {
                    anim.SetLayerWeight(i, 0);
                }
            }
        }
        else
        {
            SwitchWeapons();
        }

    }

    public void Arm(GameObject gun)
    {
        activeGun = gun;
    }

    void SwitchWeapons()
    {
        if (Input.GetKeyDown("q"))
        {
            if (activeGun != guns[guns.Count - 1])
            {
                activeGun.SetActive(false);
                activeGun = guns[guns.IndexOf(activeGun) + 1];
                activeGun.SetActive(true);
            }
            else
            {
                activeGun.SetActive(false);
                activeGun = guns[0];
                activeGun.SetActive(true);
            }
            SetTypeAnimWeight(activeGun.GetComponent<WeaponStats>().ID + _WM_.gunIndexOffset);
        }
    }

    void SetTypeAnimWeight(int ID)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(ID, 100);
    }
}
