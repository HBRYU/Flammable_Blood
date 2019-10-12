using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRND_HomingMine : MonoBehaviour
{
    private Rigidbody2D rb;

    public LayerMask whatIsGround;

    public float throwForce;
    public float explosionForce;
    public float damage;

    public float detectionRange;
    public float chaseSpeed;
    public float rotationSpeed;

    public float explosionDistance;
    public float damageRange;

    public Sprite chaseSprite;
    private Sprite defaultSprite;

    private GameObject prey;

    // Start is called before the first frame update
    void Start()
    {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * throwForce;
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach(Collider2D target in col)
        {
            if(target.tag == "Enemy")
            {
                GetComponent<SpriteRenderer>().sprite = chaseSprite;
                RaycastHit2D wallInSight = Physics2D.Raycast(transform.position, target.transform.position - transform.position, Vector2.Distance(transform.position, target.transform.position), whatIsGround);
                if (wallInSight.collider == null)
                {
                    if (closestDistance > Vector2.Distance(transform.position, target.transform.position))
                    {
                        closestEnemy = target.gameObject;
                        closestDistance = Vector2.Distance(transform.position, target.transform.position);
                    }
                }
            }
        }

        if(closestEnemy != null)
        {
            GetComponent<SpriteRenderer>().sprite = chaseSprite;
            if (prey == null)
            {
                prey = closestEnemy;
                transform.rotation = Quaternion.identity;
            }

            Chase(prey);
        }
        else
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

    void Chase(GameObject prey)
    {
        Vector3 targ = prey.transform.position;
        targ.z = 0f;

        Vector2 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle - 90)), rotationSpeed);

        rb.MovePosition(Vector2.MoveTowards(transform.position, prey.transform.position, chaseSpeed * Time.deltaTime));

        if(Vector2.Distance(transform.position, prey.transform.position) <= explosionDistance)
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, damageRange);

        foreach (Collider2D target in col)
        {
            if (target.GetComponent<EnemyStats>() != null)
            {
                RaycastHit2D wallInSight = Physics2D.Raycast(transform.position, target.transform.position - transform.position, Vector2.Distance(transform.position, target.transform.position), whatIsGround);
                if (wallInSight.collider == null)
                {
                    target.GetComponent<EnemyStats>().TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}
