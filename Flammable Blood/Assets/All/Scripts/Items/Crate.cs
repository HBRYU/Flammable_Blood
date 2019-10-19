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
                    Debug.Log("Module");
                    break;
            }
        }

        items = new List<Item>();
    }
}
