using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Rifle : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;

    public Bullet bullet;
    public GameObject barrelEnd;
    public float bulletSpeed;
    public float fireRate;
    public float accuracy;
    public float damage;
    public float magSize;
    public float reloadSpeed;
    
    [HideInInspector]
    public float fire_Timer, reload_Timer;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
    }

    // Update is called once per frame
    void Update()
    {
        fire_Timer += Time.deltaTime;
        if(fire_Timer >= fireRate)
        {
            if (Input.GetMouseButton(0))
            {
                Fire();
                fire_Timer = 0;
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
    }
}
