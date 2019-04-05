using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    private GM _GM_;
    private MasterWeaponManagement _WM_;

    public string name;

    [Header("Melee, AR, SMG, SG, SR, etc")]
    public string category;
    public GameObject gun;


    [HideInInspector]
    public int ID;

    [HideInInspector]
    public bool is_shooting;

    public float pickUpRadius;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        _WM_ = _GM_.gameObject.GetComponent<MasterWeaponManagement>();

        ID = _WM_.gunIndex.IndexOf(name);

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
            if (Input.GetKeyDown("r"))
            {
                PickUp(_GM_.player.GetComponent<PlayerWeaponManager>().gunFolder.transform);
                _GM_.player.GetComponent<PlayerWeaponManager>().guns.Add(gameObject);
                _GM_.player.GetComponent<PlayerWeaponManager>().Arm(gameObject);
            }
        }
    }

    public void Drop()
    {
        transform.parent = null;
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<PolygonCollider2D>().enabled = true;
        gun.SetActive(false);
    }

    public void PickUp(Transform parent)
    {
        transform.SetParent(parent, true);
        transform.position = parent.position;
        transform.rotation = parent.rotation;
        transform.localScale = parent.localScale;
        Debug.Log(transform.parent);
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        gun.SetActive(true);
    }
}
