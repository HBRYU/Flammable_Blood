using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Rifle : MonoBehaviour
{
    /// <summary>
    /// 실제 총알을 발사하는 무기 스크립트
    /// -AR, SR 담당
    /// -데미지, 정확도, 연사 속도, 재장전 속도, 탄창 크기 등
    /// -WeaponStats 에 필요한 정보 가지고 있음
    /// </summary>
    private GM _GM_;
    private GameObject player;
    public WeaponStats ws;

    public bool useAim, autoReload;

    public Bullet bullet;
    public GameObject barrelEnd;
    public float bulletSpeed;
    public float fireRate;
    public float accuracy;
    public float damage;
    public float magSize;
    public float reloadSpeed;

    public bool spawnBulletShell;
    public GameObject bulletShell;
    public Transform bulletShellSpawnPoint;
    public float bs_RAO;
    public float bs_MS;
    public float bs_RS;

    [HideInInspector]
    public float fire_Timer, reload_Timer, ammo;

    [HideInInspector]
    public bool reloading;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        ammo = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        switch (ws.category)
        {
            case "AR":
                AR();
                break;
            case "SR":
                SR();
                break;
            default:
                Debug.Log("ERR: Unknown wsapon category (" + ws.category + "). Using AR function instead");
                break;
        }

        if(reloading == true)
        {
            Reload();
        }
        else
        {
            fire_Timer += Time.deltaTime;

            if (Input.GetKeyDown("r") || autoReload == true && ammo <= 0)
            {
                reloading = true;
            }
        }

        if(useAim == true)
        {
            if (Input.GetMouseButton(1))
            {
                ws.is_aiming = true;
            }
            else
            {
                ws.is_aiming = false;
            }
        }
    }

    void Fire()
    {
        //Debug.Log("FIRED");
        Bullet thisBullet = bullet;
        thisBullet.damage = damage;
        thisBullet.accuracy = accuracy;
        thisBullet.speed = bulletSpeed;
        thisBullet.wielder = player;
        Instantiate(thisBullet, barrelEnd.transform.position, Quaternion.identity);

        if (spawnBulletShell) { SpawnBulletShell(); }
    }

    void Reload()
    {
        ws.is_reloading = true;
        ws.is_shooting = false;
        reload_Timer += Time.deltaTime;
        if (reload_Timer >= reloadSpeed)
        {
            ammo = magSize;
            reload_Timer = 0;
            reloading = false;
            ws.is_reloading = false;
        }
    }

    public void SpawnBulletShell()
    {
        GameObject thisBulletShell = bulletShell;
        BulletShell script = thisBulletShell.GetComponent<BulletShell>();

        script.wielder = player;
        script.randomAngleOffset = bs_RAO;
        script.minSpeed = bs_MS;
        script.randomSpeed = bs_RS;

        Instantiate(thisBulletShell, bulletShellSpawnPoint.transform.position, Quaternion.identity);
    }

    void AR()
    {
        if (Input.GetMouseButton(0) && ammo > 0)
        {
            if (fire_Timer >= fireRate)
            {
                Fire();
                fire_Timer = 0;
                ammo -= 1;
            }
            ws.is_shooting = true;
        }
        else
        {
            ws.is_shooting = false;
        }
    }

    void SR()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            if (fire_Timer >= fireRate)
            {
                Fire();
                ws.Shoot();
                fire_Timer = 0;
                ammo -= 1;
            }
            ws.is_shooting = true;
        }
        else
        {
            ws.is_shooting = false;
        }
    }
}
