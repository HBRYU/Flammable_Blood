﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [Header("WeaponStats, etc")]
    public string targetScript;
    public GameObject pressKey;

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickUp>().selectedItem == gameObject)
        {
            if (!Input.GetKey("e"))
            {
                pressKey.GetComponent<SpriteRenderer>().enabled = true;
                if(targetScript == "PickableItem" && GetComponentInParent<PickableItem>().useUI)
                {
                    GetComponentInParent<PickableItem>().UI.SetActive(true);
                }
            }
        }
        else
        {
            pressKey.GetComponent<SpriteRenderer>().enabled = false;
            if (targetScript == "PickableItem" && GetComponentInParent<PickableItem>().useUI)
            {
                GetComponentInParent<PickableItem>().UI.SetActive(false);
            }
        }
    }

    public void PickUp(Transform parent)
    {
        switch (targetScript)
        {
            case "WeaponStats":
                transform.parent.gameObject.GetComponent<WeaponStats>().PickUp(parent);
                break;
            case "Crate":
                transform.parent.GetComponent<Crate>().Access(true);
                break;
            case "Item_Module":
                transform.parent.GetComponent<Item_Module>().Access();
                break;
            case "PickableItem":
                GetComponentInParent<PickableItem>().Access();
                break;
            default:
                Debug.Log("ERR: Unknown item target script (" + targetScript +")  [From " + gameObject.name + "]");
                break;
        }
    }
}
