using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Module : MonoBehaviour
{
    public Module module;

    public void Access()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModuleManager>().InsertModule(module))
            Destroy(gameObject);
    }
}
