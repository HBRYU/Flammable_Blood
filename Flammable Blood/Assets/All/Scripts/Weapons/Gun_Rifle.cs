using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Rifle : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;
    public WeaponStats ws;

    public Bullet bullet;
    public GameObject barrelEnd;
    public float bulletSpeed;
    public float fireRate;
    public float accuracy;
    public float damage;
    public float magSize;
    public float reloadSpeed;

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
        if(reloading == true)
        {
            ws.is_reloading = true;
            ws.is_shooting = false;
            reload_Timer += Time.deltaTime;
            if(reload_Timer >= reloadSpeed)
            {
                ammo = magSize;
                reload_Timer = 0;
                reloading = false;
                ws.is_reloading = false;
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && ammo > 0)
            {
                fire_Timer += Time.deltaTime;
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

            if (Input.GetKeyDown("r"))
            {
                reloading = true;
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

        SpawnBulletShell();
    }

    void SpawnBulletShell()
    {
        GameObject thisBulletShell = bulletShell;
        BulletShell script = thisBulletShell.GetComponent<BulletShell>();

        script.wielder = player;
        script.randomAngleOffset = bs_RAO;
        script.minSpeed = bs_MS;
        script.randomSpeed = bs_RS;

        Instantiate(thisBulletShell, bulletShellSpawnPoint.transform.position, Quaternion.identity);
    }
}
