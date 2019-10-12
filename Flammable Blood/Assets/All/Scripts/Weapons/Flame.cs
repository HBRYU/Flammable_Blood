using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float damage;
    public float duration;

    public GameObject victimEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy/Hitbox"))
        {
            other.transform.parent.GetComponent<EnemyStats>().Burn(duration, damage, victimEffect);
            //Destroy(gameObject);
        }
    }
}
