using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [Header("WeaponStats, etc")]
    public string targetScript;

    public void PickUp(Transform parent)
    {
        switch (targetScript)
        {
            case "WeaponStats":
                transform.parent.gameObject.GetComponent<WeaponStats>().PickUp(parent);
                break;
            case "Crate":
                transform.parent.GetComponent<Crate>().Access();
                break;
            default:
                Debug.Log("ERR: Unknown item target script (" + targetScript +")  [From " + gameObject.name + "]");
                break;
        }
    }
}
