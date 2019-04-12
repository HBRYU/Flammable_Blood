﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Rifle : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;
    public WeaponStats ws;

    public bool useAim;

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
            fire_Timer += Time.deltaTime;

            if (Input.GetKeyDown("r"))
            {
                reloading = true;
            }
        }

        if(useAim == true)
        {
            if (Input.GetMouseButton(2))
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
            Debug.Log("Mouse Pressed(SR)");
            if (fire_Timer >= fireRate)
            {
                Fire();
                Debug.Log("Shot(SR)");
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
