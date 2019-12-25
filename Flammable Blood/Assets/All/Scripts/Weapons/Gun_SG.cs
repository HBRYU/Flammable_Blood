using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_SG : MonoBehaviour
{
    /// <summary>
    /// 실제 총알을 발사하는 무기 스크립트
    /// -AR, SR 담당
    /// -데미지, 정확도, 연사 속도, 재장전 속도, 탄창 크기 등
    /// -WeaponStats 에 필요한 정보 가지고 있음
    /// </summary>
    private GM _GM_;
    private GameObject player;
    public WeaponStats ws;

    public bool useAim, autoReload;

    public AudioClip shotSFX;
    public AudioClip reloadSFX;
    public AudioSource audioSource;

    public Bullet bullet;
    public int maxPelletCount;
    public int minPelletCount;
    public GameObject barrelEnd;
    public float bulletSpeed;
    public float fireRate;
    public float accuracy;
    public float damagePerPellet;
    public int magSize;
    public float reloadSpeed;

    public bool autoSpawnBulletShell;
    public GameObject bulletShell;
    public Transform bulletShellSpawnPoint;
    public float bs_RAO;
    public float bs_MS;
    public float bs_RS;

    [HideInInspector]
    public float fire_Timer, reload_Timer;

    [HideInInspector]
    public int ammo;

    [HideInInspector]
    public bool reloading;

    public float camShake_force;
    public float camShake_duration;

    List<int> ammoCount;
    List<string> ammoType;
    private int availableAmmo;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        ammo = magSize;
        ws.magSize = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (_GM_.shooting_active)
            ACTIVE();
    }

    void ACTIVE()
    {
        ammoCount = player.GetComponent<PlayerWeaponManager>().ammo_count;
        ammoType = player.GetComponent<PlayerWeaponManager>().ammo_type;

        availableAmmo = ammoCount[ammoType.IndexOf(ws.ammoType)];

        if (availableAmmo > 0)
        {
            SG();

            if (reloading == true)
            {
                Reload();
            }
            else
            {
                fire_Timer += Time.deltaTime;

                if ((Input.GetKeyDown("r") || autoReload == true && ammo <= 0 && reloading == false) && ammo < magSize)
                {
                    GM.DisplayText2("RELOADING. . .", true);
                    audioSource.PlayOneShot(reloadSFX);
                    reloading = true;
                }
            }
        }

        if (useAim == true)
        {
            if (Input.GetMouseButton(1))
            {
                ws.is_aiming = true;
            }
            else
            {
                ws.is_aiming = false;
            }
        }

        ws.ammoCount = ammo;
    }

    void Fire()
    {
        int pelletCount = Random.Range(minPelletCount, maxPelletCount);
        
        for(int i = 0; i < pelletCount; i++)
        {
            Bullet thisBullet = bullet;
            thisBullet.damage = damagePerPellet;
            thisBullet.accuracy = accuracy;
            thisBullet.speed = bulletSpeed;
            thisBullet.wielder = player.GetComponent<Gun_Rotation>().pivot.gameObject;


            if (player.transform.localScale.x == -1)
            {
                Quaternion scaler = thisBullet.transform.localRotation;
                scaler.y = 180;
                thisBullet.transform.localRotation = scaler;
            }
            if (player.transform.localScale.x == 1)
            {
                Quaternion scaler = thisBullet.transform.localRotation;
                scaler.y = 0;
                thisBullet.transform.localRotation = scaler;
            }


            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(player.GetComponent<Gun_Rotation>().pivot.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            thisBullet.transform.rotation = Quaternion.Euler(new Vector3(0, thisBullet.transform.rotation.y, angle));

            Instantiate(thisBullet, barrelEnd.transform.position, thisBullet.transform.localRotation);
        }

        audioSource.PlayOneShot(shotSFX);

        _GM_.camShakeManager.CameraShake(camShake_force, camShake_duration, false);

        ////////    Update ammo count
        player.GetComponent<PlayerWeaponManager>().ammo_count[ammoType.IndexOf(ws.ammoType)] -= 1;

        if (autoSpawnBulletShell) { SpawnBulletShell(); }
    }

    void Reload()
    {
        ws.is_reloading = true;
        ws.is_shooting = false;
        reload_Timer += Time.deltaTime;
        if (reload_Timer >= reloadSpeed)
        {
            ammo = magSize;
            reload_Timer = 0;
            reloading = false;
            ws.is_reloading = false;
        }
    }

    public void SpawnBulletShell()
    {
        GameObject thisBulletShell = bulletShell;
        BulletShell script = thisBulletShell.GetComponent<BulletShell>();

        script.wielder = player;
        script.randomAngleOffset = bs_RAO;
        script.minSpeed = bs_MS;
        script.randomSpeed = bs_RS;

        Instantiate(thisBulletShell, bulletShellSpawnPoint.transform.position, Quaternion.identity);
    }

    void SG()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            if (fire_Timer >= fireRate)
            {
                Fire();
                ws.Shoot();
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
