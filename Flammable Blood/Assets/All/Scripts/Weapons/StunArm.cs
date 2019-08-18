using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunArm : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;
    private WeaponStats ws;

    public LayerMask enemyLayers;

    public float damage;
    public float rechargeSpeed;
    public float stunDuration;
    public float knockbackForce;

    public Transform damagePoint;
    public float damageRadius;

    [HideInInspector]
    public float recharge_Timer;

    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        ws = transform.parent.GetComponent<WeaponStats>();
    }

    void Update()
    {
        if(recharge_Timer >= rechargeSpeed)
        {
            if (Input.GetMouseButton(0))
            {
                Shock();
                recharge_Timer = 0.0f;
            }
        }
        else
        {
            recharge_Timer += Time.deltaTime;
        }
    }

    void Shock()
    {
        ws.Shoot();

        Collider2D[] hit = Physics2D.OverlapCircleAll(damagePoint.position, damageRadius, enemyLayers);

        foreach(Collider2D enemy in hit)
        {
            EnemyStats enemyStats = enemy.gameObject.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage, stunDuration);

                int val1 = 0;
                if (enemy.transform.position.x >= transform.position.x)
                    val1 = 1;
                else
                    val1 = -1;

                //enemyStats.Knockback(knockbackForce, "simpleX", val1);
            }
        }
    }
}
