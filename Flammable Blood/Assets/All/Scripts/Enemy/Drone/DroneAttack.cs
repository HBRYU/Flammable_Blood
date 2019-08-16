using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;

    public GameObject gun;
    public GameObject bullet;
    public Transform barrelEnd;
    public float aimSpeed;

    public float accuracy;
    public float damage;
    public float bulletSpeed;
    public float fireRate;
    private float fireRate_timer;

    public float maxAmmoCount;
    public float reloadSpeed;
    private float reloadSpeed_timer;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AimGun();
    }

    private void Update()
    {
        fireRate_timer += Time.deltaTime;
        if(fireRate_timer >= fireRate)
        {
            GameObject thisBullet = bullet;
            EnemyBullet thisBulletScript = thisBullet.GetComponent<EnemyBullet>();

            thisBulletScript.accuracy = accuracy;
            thisBulletScript.damage = damage;
            thisBulletScript.speed = bulletSpeed;
            Instantiate(thisBullet, barrelEnd.position, gun.transform.rotation);
        }
    }

    void AimGun()
    {
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = gun.transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, (angle + 180))), aimSpeed);
    }
}
