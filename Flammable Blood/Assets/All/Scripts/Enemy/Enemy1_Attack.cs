using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Attack : MonoBehaviour
{
    private Enemy1_Animation e1anim;

    public GameObject projectile;
    public Transform barrelEnd;
    public float damage;
    public float bulletSpeed;
    public float accuracy;

    public float reloadSpeed;
    private float reload_timer;

    public int magSize;
    public float magReloadSpeed;
    private float magReload_timer;
    public int ammo;

    public bool attack;

    private void Start()
    {
        e1anim = GetComponent<Enemy1_Animation>();
        ammo = magSize;
    }

    void Update()
    {
        if (attack)
            Attack();
    }

    void Attack()
    {
        reload_timer += Time.deltaTime;
        if(ammo > 0)
        {
            if (reload_timer >= reloadSpeed)
            {
                e1anim.Shoot();
                GameObject thisBullet = projectile;
                EnemyBullet thisBulletScript = thisBullet.GetComponent<EnemyBullet>();
                thisBulletScript.wielder = gameObject;
                thisBulletScript.damage = damage;
                thisBulletScript.speed = bulletSpeed;
                thisBulletScript.accuracy = accuracy;
                Instantiate(thisBullet, barrelEnd.position, Quaternion.identity);
                ammo -= 1;
                reload_timer = 0;
            }
        }
        else
        {
            magReload_timer += Time.deltaTime;
            if(magReload_timer >= magReloadSpeed)
            {
                ammo = magSize;
                magReload_timer = 0;
            }
        }
        
    }
}
