using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private GM _GM_;

    public bool god;

    public float maxHealth;
    public float health;
    public bool alive = true;

    public GameObject[] corpse;

    public float corpseSpawnOffset;

    public GameObject explosion;
    public float explosionForce;
    public float explosionRadius;
    public float explosionDuration;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            alive = false;
            Die();
        }
        if (health > maxHealth)
            health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(health > 0 && !god)
            health -= damage;
    }

    public void Die()
    {

        ////////////////////    Drop all items
        PlayerWeaponManager wm = GetComponent<PlayerWeaponManager>();

        List<GameObject> weapons = wm.weapons;

        ////////////////////    Spawn bodyparts
        foreach(GameObject bodyPart in corpse)
        {
            Instantiate(bodyPart, transform.position + new Vector3(Random.Range(-corpseSpawnOffset, corpseSpawnOffset), Random.Range(-corpseSpawnOffset, corpseSpawnOffset), 0.0f), Quaternion.identity);
        }

        ////////////////////    Explode
        GameObject thisExplosion = explosion;
        CircleCollider2D thisCollider = thisExplosion.GetComponent<CircleCollider2D>();
        PointEffector2D thisEffector = thisExplosion.GetComponent<PointEffector2D>();

        thisCollider.radius = explosionRadius;
        thisEffector.forceMagnitude = explosionForce;

        thisExplosion.GetComponent<Explosion>().duration = explosionDuration;

        Instantiate(explosion, transform.position, Quaternion.identity);

        ///////////////////     Self Destruct
        GameObject.FindGameObjectWithTag("PlayerGraphics").SetActive(false);
        GameObject.FindGameObjectWithTag("PlayerJetpack").SetActive(false);
        GetComponent<PlayerMove>().enabled = false;
        GetComponent<PlayerPickUp>().enabled = false;
        GetComponent<PlayerAnimControl>().enabled = false;
        GetComponent<PlayerWeaponManager>().enabled = false;

        GetComponent<Animator>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(true);
            weapon.GetComponent<WeaponStats>().Drop();
            weapon.GetComponent<WeaponStats>().is_shooting = false;
            weapon.GetComponent<Animator>().SetBool("Shooting", false);
            //Debug.Log("Weapon dropped");
        }

        GameObject.FindGameObjectWithTag("UI").GetComponent<UI_Dead>().Dead();

        //GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursory>().enabled = false;
        //GameObject.FindGameObjectWithTag("Cursor").GetComponent<SpriteRenderer>().enabled = false;
        //Cursor.visible = true;

        _GM_.playerAlive = false;

        enabled = false;
    }
}
