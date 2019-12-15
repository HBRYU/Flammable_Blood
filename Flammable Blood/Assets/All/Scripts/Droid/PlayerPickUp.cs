using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public float pickUpRadius;
    public LayerMask pickUp_layer;

    public float coolDown;
    private float coolDown_Timer;

    public GameObject weaponsFolder;

    [HideInInspector]
    public Crate activeCrate;

    [HideInInspector]
    public GameObject selectedItem;

    void Update()
    {
        Collider2D[] checkRadius = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, pickUp_layer);
        GameObject closestItem = null;
        float minDistance = Mathf.Infinity;

        if (checkRadius != null)
        {
            
            foreach (Collider2D item in checkRadius)
            {
                float distance;
                if (Input.GetKey("f"))
                {
                    distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), item.transform.position);
                }

                else
                {
                    distance = Vector2.Distance(transform.position, item.transform.position);
                }
                GameObject itemParent = item.transform.parent.gameObject;

                if (distance < minDistance && (itemParent.transform.parent == null || !itemParent.transform.parent.tag.Contains("Player/Inventory")))
                {
                    closestItem = item.gameObject;
                    minDistance = distance;
                }
            }

            if (closestItem != null)
                selectedItem = closestItem.gameObject;
            else
                selectedItem = null;
            //Debug.Log(selectedItem);

            if (coolDown_Timer >= coolDown)
            {
                PickUp(closestItem);
            }
            else
            {
                coolDown_Timer += Time.deltaTime;
            }
        }
        

    }

    void PickUp(GameObject item)
    {
        if (Input.GetKeyDown("e") || (Input.GetMouseButtonDown(0) && Input.GetKey("f")))
        {
            if (item != null)
            {
                switch (item.GetComponent<ItemPickUp>().targetScript)
                {
                    case "WeaponStats":
                        item.GetComponent<ItemPickUp>().PickUp(weaponsFolder.transform);
                        break;
                    case "Crate":
                        item.GetComponent<ItemPickUp>().PickUp(null);
                        activeCrate = item.transform.parent.GetComponent<Crate>();
                        break;
                    default:
                        item.GetComponent<ItemPickUp>().PickUp(transform);
                        break;
                }
            }

            coolDown_Timer = 0.0f;
        }
    }
}
