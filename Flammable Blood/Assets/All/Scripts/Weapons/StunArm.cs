using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunArm : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;
    private WeaponStats ws;

    public Collider2D hitArea;
    public float damage;
    public float rechargeSpeed;
    public float stunDuration;
    public float knockbackForce;

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
            Shock();
        }
    }

    void Shock()
    {
        
    }
}
