using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private MasterWeaponManagement _WM_;

    public GameObject gunFolder;
    public int maxGunCount;
    private Animator anim;
    private PlayerAnimControl ac;

    public List<GameObject> guns;
    public GameObject activeGun;
    // Start is called before the first frame update
    void Start()
    {
        _WM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<MasterWeaponManagement>();
        anim = GetComponent<Animator>();
        ac = GetComponent<PlayerAnimControl>();

        if(guns.Count > maxGunCount)
        {
            while(guns.Count > maxGunCount)
            {
                guns[guns.Count - 1].GetComponent<WeaponStats>().Drop();
                guns.Remove(guns[guns.Count - 1]);
            }
        }

        if (activeGun == null)
        {
            SetTypeAnimWeight(anim.GetLayerIndex("Default [Type-0]"), false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(guns.Count == 0) { activeGun = null; }

        if(activeGun != null)
        {
            SwitchWeapons();
            if (activeGun.GetComponent<WeaponStats>().is_shooting == true)
            {
                ac.Shoot(activeGun, true, true);
            }
            else
            {
                ac.Shoot(activeGun, false, true);
            }
        }
    }

    public void Arm(GameObject gun)
    {
        activeGun = gun;
        SetTypeAnimWeight(anim.GetLayerIndex(activeGun.GetComponent<WeaponStats>().category), true);
        Debug.Log(activeGun.GetComponent<WeaponStats>().category);
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
            Debug.Log(activeGun.GetComponent<WeaponStats>().category);
            SetTypeAnimWeight(anim.GetLayerIndex(activeGun.GetComponent<WeaponStats>().category), true);
        }
    }

    void SetTypeAnimWeight(int index, bool useLegs)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(index, 100);
        Debug.Log("Fuck you");
        if(useLegs == true) { anim.SetLayerWeight(anim.GetLayerIndex("Legs Only"), 100); }
    }
}
