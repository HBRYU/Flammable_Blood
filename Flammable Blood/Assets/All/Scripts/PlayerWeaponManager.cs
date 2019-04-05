using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private MasterWeaponManagement _WM_;

    public GameObject gunFolder;
    public int maxWeaponCount;
    private Animator anim;
    private PlayerAnimControl ac;

    public List<GameObject> weapons;
    public GameObject activeWeapon;
    // Start is called before the first frame update
    void Start()
    {
        _WM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<MasterWeaponManagement>();   //  마스터 무기 매니저 스크립트
        anim = GetComponent<Animator>();    // 플레이어 애니메이터
        ac = GetComponent<PlayerAnimControl>();     // 플레이어 애니메이션 매니저 스크립트

        //  무기 수 제한
        while(weapons.Count > maxWeaponCount)
        {
            weapons[weapons.Count - 1].GetComponent<WeaponStats>().Drop();
            weapons.Remove(weapons[weapons.Count - 1]);
        }

        // 총이 없을 때
        if (activeWeapon == null)
        {
            SetTypeAnimWeight(anim.GetLayerIndex("Default [Type-0]"), false);
        }
    }

    void Update()   ///////////////////////     업데이트        /////////////////////////
    {
        if(weapons.Count == 0) { activeWeapon = null; }

        // 총 발사하고 있다면 발사 애니메이션 플레이
        if(activeWeapon != null)
        {
            SwitchWeapons();
            if (activeWeapon.GetComponent<WeaponStats>().is_shooting == true)
            {
                ac.Shoot(activeWeapon, true, true);
            }
            else
            {
                ac.Shoot(activeWeapon, false, true);
            }
        }
    }

    public void Arm(GameObject weapon)
    {
        activeWeapon = weapon;
        SetTypeAnimWeight(anim.GetLayerIndex(activeWeapon.GetComponent<WeaponStats>().category), true);
    }

    void SwitchWeapons()
    {
        if (Input.GetKeyDown("q"))
        {
            if (activeWeapon != weapons[weapons.Count - 1])
            {
                activeWeapon.SetActive(false);
                activeWeapon = weapons[weapons.IndexOf(activeWeapon) + 1];
                activeWeapon.SetActive(true);
            }
            else
            {
                activeWeapon.SetActive(false);
                activeWeapon = weapons[0];
                activeWeapon.SetActive(true);
            }
            SetTypeAnimWeight(anim.GetLayerIndex(activeWeapon.GetComponent<WeaponStats>().category), true);
        }
    }

    void SetTypeAnimWeight(int index, bool useLegs)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(index, 100);
        if(useLegs == true) { anim.SetLayerWeight(anim.GetLayerIndex("Legs Only"), 100); }
    }
}
