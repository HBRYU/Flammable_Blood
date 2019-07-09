using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid1_Attack : MonoBehaviour
{
    public GameObject projectile;
    public float damage;
    public float reloadSpeed;
    private float reload_timer;

    public bool attack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
            Instantiate(thisBullet, transform.position, Quaternion.identity);
        }
    }
}
