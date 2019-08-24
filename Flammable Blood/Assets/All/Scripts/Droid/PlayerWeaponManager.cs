using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    /// <summary>
    /// 플레이어 무기 매니저 스크립트
    /// -무기 전환
    /// -무기 줍기
    /// -무기/플레이어 애니메이션 싱크로
    /// -WeaponStats 와 PlayerAnimControl 사이의 통로 역할
    /// </summary>

    private MasterWeaponManagement _WM_;

    public int maxWeaponCount;
    private Animator anim;
    private PlayerAnimControl ac;
    private PlayerMove pm;

    public Transform gunFolder_Default;
    public Transform gunFolder_Crouched;
    public Transform activeGunFolder;

    public List<GameObject> weapons;
    public GameObject activeWeapon;
    private GameObject lastActiveWeapon;
    public GameObject defaultWeapon;

    private bool reloading;

    //각 무기 애니메이션 변수 설정
    [HideInInspector]
    public WeaponStats AW_WS;
    private string AW_category;
    private bool AW_rapidFire = true;
    private float AW_reloadDelay;
    private float AW_reloadDelayTimer;
    [HideInInspector]
    public string AW_ammoType;

    public List<string> ammo_type;
    public List<int> ammo_count;
    public List<int> ammo_max;


    void Start()    ////////////////////////    셋업      //////////////////////////
    {
        _WM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<MasterWeaponManagement>();   //  마스터 무기 매니저 스크립트
        anim = GetComponent<Animator>();    // 플레이어 애니메이터
        ac = GetComponent<PlayerAnimControl>();     // 플레이어 애니메이션 매니저 스크립트
        pm = GetComponent<PlayerMove>();
        activeGunFolder = gunFolder_Default;
        //defaultWeapon.GetComponent<WeaponStats>().PickUp(transform);

        //  무기 수 제한
        while (weapons.Count > maxWeaponCount)
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

        if (pm.crouched)
        {
            foreach(GameObject weapon in weapons)
                SetWeaponsFolder(weapon, gunFolder_Crouched);
            //SetWeaponsFolder(defaultWeapon, gunFolder_Crouched);
        }
        else
        {
            foreach (GameObject weapon in weapons)
                SetWeaponsFolder(weapon, gunFolder_Default);
            //SetWeaponsFolder(defaultWeapon, gunFolder_Default);
        }

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
            if (Input.GetKeyDown("`") && weapons.Count > 0)
            {
                activeWeapon = lastActiveWeapon;
                activeWeapon.SetActive(true);
                SetCategoryAnimWeight(anim.GetLayerIndex(AW_category), true);
            }
        }
    }

    public void Arm(GameObject weapon)      //무기 줍기
    {
        if (weapons.Count > maxWeaponCount)
        {
            if (activeWeapon != null)
            {
                activeWeapon.SetActive(true);
                activeWeapon.GetComponent<WeaponStats>().Drop();
                weapons.Remove(activeWeapon);
            }
            else if (activeWeapon == null)
            {
                lastActiveWeapon.SetActive(true);
                lastActiveWeapon.GetComponent<WeaponStats>().Drop();
                weapons.Remove(lastActiveWeapon);
            }
        }
        else if (activeWeapon != null)
        {
            activeWeapon.SetActive(false);
        }
        activeWeapon = weapon;
        SetVariables();
        SetCategoryAnimWeight(anim.GetLayerIndex(AW_category), true);
    }

    void SwitchWeapons()        //무기 전환
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
            SetVariables();
            SetCategoryAnimWeight(anim.GetLayerIndex(AW_category), true);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(activeWeapon == defaultWeapon)
            {
                activeWeapon.SetActive(false);
                activeWeapon = weapons[0];
                activeWeapon.SetActive(true);
            }
            else
            {
                activeWeapon.SetActive(false);
                activeWeapon = defaultWeapon;
                activeWeapon.SetActive(true);
            }
            SetVariables();
            SetCategoryAnimWeight(anim.GetLayerIndex(AW_category), true);

        }
    }

    void SetCategoryAnimWeight(int index, bool useLegs)     //애니메이션 종류 설정
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(index, 100);
        if(useLegs == true)
            anim.SetLayerWeight(anim.GetLayerIndex("Legs Only"), 100);   //다리는 따로 움직이게
    }

    void Animate()
    {
        if(AW_rapidFire == true)        //연사 발사 애니메이션 플레이
        {
            if (AW_WS.is_shooting == true)
                ac.Shoot(activeWeapon, true, true);
            else
                ac.Shoot(activeWeapon, false, true);
        }
        
        if (AW_WS.is_reloading == true)       //일정 딜레이 후 재장전 애니메이션 플레이
        {
            if(reloading == false)
            {
                AW_reloadDelayTimer += Time.deltaTime;
                if (AW_reloadDelayTimer >= AW_reloadDelay)
                {
                    ac.Reload();
                    reloading = true;
                    AW_reloadDelayTimer = 0.0f;
                }
            }
        }
        else
        {
            reloading = false;
        }

        if(AW_WS.is_aiming == true)
            ac.Aim(true);
        else
            ac.Aim(false);

    }

    public void Shoot()     //한번 쏘는 애니메이션 플레이
    {
        ac.Shoot(activeWeapon, true, false);
    }

    void SetVariables()     //변수 설정
    {
        AW_WS = activeWeapon.GetComponent<WeaponStats>();
        AW_category = AW_WS.category;
        AW_rapidFire = AW_WS.rapidFire;
        AW_reloadDelay = AW_WS.reloadAnimDelay;
        AW_ammoType = AW_WS.ammoType;
    }

    void SetWeaponsFolder(GameObject weapon, Transform folder)
    {
        weapon.transform.parent = folder;
        weapon.transform.position = folder.position;
        weapon.transform.rotation = folder.rotation;
        weapon.transform.localScale = folder.localScale;
    }
}
