﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;

    public AudioClip shotSFX;

    public GameObject gun;
    public GameObject bullet;
    public Transform barrelEnd;

    public bool attack;
    public LayerMask whatIsGround;
    public float alertDistance;
    public float alertDistance_shot;
    public float attackDistance;

    private Quaternion gunInitRot;
    public float aimSpeed;

    public float accuracy;
    public float damage;
    public float bulletSpeed;
    public float fireRate;
    private float fireRate_timer;

    public float maxAmmoCount;
    public float ammoCount;
    public float reloadSpeed;
    private float reloadSpeed_timer;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        ammoCount = maxAmmoCount;
        gunInitRot = gun.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D wallInSight = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Vector2.Distance(transform.position, player.transform.position), whatIsGround);
        if (Vector2.Distance(transform.position, player.transform.position) < alertDistance && wallInSight.collider == null)
        {
            attack = true;
        }
        if (Vector2.Distance(transform.position, player.transform.position) > attackDistance || wallInSight.collider != null)
            attack = false;
        if (attack)
        AimGun();
        else
        {
            if (transform.localEulerAngles.y != 0)
                gunInitRot.y = 180;
            else
                gunInitRot.y = 0;
            gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, gunInitRot, aimSpeed);
        }
    }

    private void Update()
    {
        if (!_GM_.playerAlive)
            attack = false;

        if (attack)
            UseGun();
    }

    void AimGun()
    {
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector2 objectPos = gun.transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, Quaternion.Euler(new Vector3(gun.transform.rotation.x, 0, angle)), aimSpeed);
    }

    void UseGun()
    {
        fireRate_timer += Time.deltaTime;
        if (fireRate_timer >= fireRate && ammoCount > 0)
        {
            Fire();

            ammoCount -= 1;

            fireRate_timer = 0;
        }

        else if (ammoCount <= 0)
        {
            reloadSpeed_timer += Time.deltaTime;
            if (reloadSpeed_timer >= reloadSpeed)
            {
                ammoCount = maxAmmoCount;
                reloadSpeed_timer = 0;
            }
        }

        /////////////////////////////   Internal Functions
        void Fire()
        {
            GameObject thisBullet = bullet;
            EnemyBullet2 thisBulletScript = thisBullet.GetComponent<EnemyBullet2>();

            thisBulletScript.accuracy = accuracy;
            thisBulletScript.damage = damage;
            thisBulletScript.speed = bulletSpeed;

            Vector3 bulletRotation = gun.transform.localEulerAngles - transform.localEulerAngles;
            bulletRotation.y = 0;

            GetComponent<AudioSource>().PlayOneShot(shotSFX);

            Instantiate(thisBullet, barrelEnd.position, Quaternion.Euler(bulletRotation));
        }
    }
}
