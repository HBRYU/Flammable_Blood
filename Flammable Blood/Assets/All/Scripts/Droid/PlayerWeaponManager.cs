﻿using System.Collections;
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
    private GameObject lastActiveWeapon;

    private bool reloading;

    private WeaponStats AW_WS;
    private string AW_category;
    public bool AW_rapidFire = true;
    private float AW_reloadDelay;
    private float AW_reloadDelayTimer;
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
            SetCategoryAnimWeight(anim.GetLayerIndex("Default [Type-0]"), false);
        }
    }

    void Update()   ///////////////////////     업데이트        /////////////////////////
    {
        if(weapons.Count == 0) { activeWeapon = null; }

        // 총 발사하고 있다면 발사 애니메이션 플레이
        if(activeWeapon != null)
        {
            SetVariables();
            Animate();
            SwitchWeapons();

            if (Input.GetKeyDown("`"))
            {
                lastActiveWeapon = activeWeapon;
                activeWeapon.SetActive(false);
                activeWeapon = null;
                SetCategoryAnimWeight(anim.GetLayerIndex("Default [Type-0]"), false);
            }
        }
        else
        {
            if (Input.GetKeyDown("`"))
            {
                activeWeapon = lastActiveWeapon;
                activeWeapon.SetActive(true);
                SetCategoryAnimWeight(anim.GetLayerIndex(AW_category), true);
            }
        }
    }

    public void Arm(GameObject weapon)
    {
        if(weapons.Count > maxWeaponCount)
        {
            if (activeWeapon != null)
            {
                activeWeapon.SetActive(true);
                activeWeapon.GetComponent<WeaponStats>().Drop();
                weapons.Remove(activeWeapon);
            }
            else
            {
                lastActiveWeapon.SetActive(true);
                lastActiveWeapon.GetComponent<WeaponStats>().Drop();
                weapons.Remove(lastActiveWeapon);
            }
        }
        else if (activeWeapon != null) { activeWeapon.SetActive(false); }
        activeWeapon = weapon;
        SetVariables();
        SetCategoryAnimWeight(anim.GetLayerIndex(AW_category), true);
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
            SetCategoryAnimWeight(anim.GetLayerIndex(AW_category), true);
        }
    }

    void SetCategoryAnimWeight(int index, bool useLegs)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(index, 100);
        if(useLegs == true) { anim.SetLayerWeight(anim.GetLayerIndex("Legs Only"), 100); }
    }

    void Animate()
    {
        if(AW_rapidFire == true)
        {
            if (AW_WS.is_shooting == true)          { ac.Shoot(activeWeapon, true, true); }
            else                                                           { ac.Shoot(activeWeapon, false, true); }
        }
        
        if (AW_WS.is_reloading == true)
        {
            if (reloading == false)
            {
                AW_reloadDelayTimer += Time.deltaTime;
                if (AW_reloadDelayTimer >= AW_reloadDelay)
                {
                    ac.Reload(true);
                    reloading = true;
                    AW_reloadDelayTimer = 0.0f;
                }
            }
            
            
        }
        else
        {
            //ac.Reload(false);
            reloading = false;
        }

        if(AW_WS.is_aiming == true)       { ac.Aim(true); }
        else                                                    {ac.Aim(false);}

    }

    public void Shoot()
    {
        ac.Shoot(activeWeapon, true, false);
    }

    void SetVariables()
    {
        AW_WS = activeWeapon.GetComponent<WeaponStats>();
        AW_category = AW_WS.category;
        AW_rapidFire = AW_WS.rapidFire;
        AW_reloadDelay = AW_WS.reloadAnimDelay;
    }
}
