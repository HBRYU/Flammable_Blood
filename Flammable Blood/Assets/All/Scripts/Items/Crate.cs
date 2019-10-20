using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string type;
    public float count;
    public GameObject obj;
    public Module module;
}


public class Crate : MonoBehaviour
{
    private GameObject player;

    public string ID;
    public List<Item> items;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Access()
    {
        List<Item>  remains = new List<Item>();

        foreach(Item item in items)
        {
            switch (item.type)
            {
                case "Ammo":
                    PlayerWeaponManager wm = player.GetComponent<PlayerWeaponManager>();
                    wm.ammo_count[wm.ammo_type.IndexOf(item.name)] += Mathf.RoundToInt(item.count);
                    break;
                case "Health":
                    PlayerStats ps = player.GetComponent<PlayerStats>();
                    ps.health += item.count;
                    break;
                case "Module":
                    PlayerModuleManager mm = player.GetComponent<PlayerModuleManager>();
                    if(mm.InsertModule(item.module) == false)
                    {
                        remains.Add(item);
                    }
                    break;
            }
        }

        items = remains;
    }
}
