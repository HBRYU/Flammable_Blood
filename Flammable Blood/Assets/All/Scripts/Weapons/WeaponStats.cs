using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    /// <summary>
    /// PlayerWeaponManager에서 필요한 정보를 담는 스크립트
    /// -무기 이름
    /// -무기 종류
    /// -무기 특성
    /// -무기 아이템화, 무기화
    /// -해당 무기 스크립트와 PlayerWeaponManager 사이의 통로 역할
    /// </summary>
    [HideInInspector]
    public GM _GM_;
    private MasterWeaponManagement _WM_;

    public string name;

    [Header("Melee, AR, SMG, SG, SR, etc")]
    public string category;
    public Sprite IMG;
    public GameObject gun;
    public SpriteRenderer pointer;

    public Collider2D col;

    public Transform IK_L;
    public Transform IK_R;

    public string ammoType;
    public int alertAmmoCount;
    public bool rapidFire, hideOnEquip;
    public float reloadAnimDelay;

    [HideInInspector]
    public int ID, ammoCount, magSize;

    [HideInInspector]
    public bool is_shooting, is_reloading, is_aiming;


    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        _WM_ = _GM_.gameObject.GetComponent<MasterWeaponManagement>();

        ID = _WM_.gunIndex.IndexOf(name);

        if(transform.parent == null || transform.parent.tag != "Player/Inventory/Weapons")
        {
            Drop();
        }
    }

    public void Drop()
    {
        if (hideOnEquip)
            GetComponent<SpriteRenderer>().enabled = true;
        transform.parent = null;
        GetComponent<Rigidbody2D>().simulated = true;
        col.enabled = true;
        GetComponent<Animator>().SetBool("Shooting", false);
        pointer.enabled = true;
        gun.SetActive(false);
    }

    public void PickUp(Transform parent)
    {
        //Debug.Log(_GM_.player.GetComponent<PlayerWeaponManager>().weapons.IndexOf(gameObject));
        bool exists = false;
        foreach(GameObject gun in _GM_.player.GetComponent<PlayerWeaponManager>().weapons)
        {
            if(gun.GetComponent<WeaponStats>().ID == ID)
            {
                exists = true;
            }
        }
        if (!exists)
        {
            if (hideOnEquip)
                GetComponent<SpriteRenderer>().enabled = false;
            transform.SetParent(parent, true);
            transform.position = parent.position;
            transform.rotation = parent.rotation;
            transform.localScale = parent.localScale;
            GetComponent<Rigidbody2D>().simulated = false;
            col.enabled = false;
            pointer.enabled = false;
            gun.SetActive(true);
            _GM_.player.GetComponent<PlayerWeaponManager>().weapons.Add(gameObject);
            _GM_.player.GetComponent<PlayerWeaponManager>().Arm(gameObject);
        }
        else
        {
            GM.DisplayText("Weapon already exists", false);
        }
    }

    public void Shoot()
    {
        _GM_.player.GetComponent<PlayerWeaponManager>().Shoot();
    }

    public void SpawnBulletShell(string script)
    {
        switch (script)
        {
            case "Gun_Rifle":
                gun.GetComponent<Gun_Rifle>().SpawnBulletShell();
                break;
            default:
                Debug.Log("ERR: Unknown gun script (" + script + ")");
                break;
        }
    }
}
