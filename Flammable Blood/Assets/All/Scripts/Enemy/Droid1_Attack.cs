using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid1_Attack : MonoBehaviour
{
    public GameObject projectile;
    public Transform barrelEnd;
    public float damage;
    public float bulletSpeed;

    public float reloadSpeed;
    private float reload_timer;

    public bool attack;

    void Update()
    {
        if (attack)
            Attack();
    }

    void Attack()
    {
        reload_timer += Time.deltaTime;
        if(reload_timer >= reloadSpeed)
        {
            GameObject thisBullet= projectile;
            EnemyBullet thisBulletScript = thisBullet.GetComponent<EnemyBullet>();
            thisBulletScript.wielder = gameObject;
            thisBulletScript.damage = damage;
            thisBulletScript.speed = bulletSpeed;
            Instantiate(thisBullet, barrelEnd.position, Quaternion.identity);
            reload_timer = 0;
        }
    }
}
