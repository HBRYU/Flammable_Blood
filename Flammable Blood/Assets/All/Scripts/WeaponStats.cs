using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    private GM _GM_;
    public string name;
    public int ID;

    public float pickUpRadius;
    public LayerMask playerLayer;

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
        Collider2D checkArea = Physics2D.OverlapCircle(transform.position, pickUpRadius, playerLayer);
        if (checkArea == true)
        {
            Debug.Log("Contact");
            if (Input.GetKeyDown("r"))
            {
                PickUp(_GM_.player.GetComponent<PlayerWeaponManager>().gunFolder.transform);
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
        transform.position = new Vector2(0, 0);
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }
}
