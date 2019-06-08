﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public float pickUpRadius;
    public LayerMask pickUp_layer;

    public float coolDown;
    private float coolDown_Timer;

    public GameObject weaponsFolder;

    void Update()
    {
        if(coolDown_Timer >= coolDown)
        {
            PickUp();
        }
        else
        {
            coolDown_Timer += Time.deltaTime;
        }
        
    }

    void PickUp()
    {
        if (Input.GetKeyDown("e"))
        {
            Collider2D[] checkRadius = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, pickUp_layer);
            if (checkRadius != null)
            {
                GameObject closestItem = null;
                float minDistance = Mathf.Infinity;
                foreach (Collider2D item in checkRadius)
                {
                    float distance = Vector2.Distance(transform.position, item.transform.position);
                    GameObject itemParent = item.transform.parent.gameObject;

                    if (distance < minDistance && (itemParent.transform.parent == null || !itemParent.transform.parent.tag.Contains("Player/Inventory")))
                    {
                        closestItem = item.gameObject;
                        minDistance = distance;
                    }
                }

                if (closestItem != null)
                {
                    switch (closestItem.GetComponent<ItemPickUp>().targetScript)
                    {
                        case "WeaponStats":
                            if(GetComponent<PlayerWeaponManager>().activeWeapon != GetComponent<PlayerWeaponManager>().defaultWeapon)
                                closestItem.GetComponent<ItemPickUp>().PickUp(weaponsFolder.transform);
                            break;
                        default:
                            closestItem.GetComponent<ItemPickUp>().PickUp(transform);
                            break;
                    }
                }

                coolDown_Timer = 0.0f;
            }
        }
    }
}
