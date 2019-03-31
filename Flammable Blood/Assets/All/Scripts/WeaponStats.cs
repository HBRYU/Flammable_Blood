using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    private GM _GM_;
    public string name;
    public int ID;

    public float pickUpRadius;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        if(transform.parent == null || transform.parent.tag != "Player/Inventory/Weapons")
        {
            Drop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D checkArea = Physics2D.OverlapCircle(transform.position, pickUpRadius, LayerMask.NameToLayer("Player/Hitbox"));
        if (checkArea != null && checkArea.gameObject.CompareTag("Player/Hitbox"))
        {
            if (Input.GetKeyDown("r"))
            {
                PickUp(_GM_.player.transform);
            }
        }
    }

    public void Drop()
    {
        transform.parent = null;
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    public void PickUp(Transform parent)
    {
        transform.parent = parent;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }
}
