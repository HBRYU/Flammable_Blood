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

    public void Reset(Item item)
    {
        item.name = string.Empty;
        item.type = string.Empty;
        item.count = 0;
        item.obj = null;
        item.module.Reset(module);
    }
}


public class Crate : MonoBehaviour
{
    public bool opened;
    private GameObject player;
    public float accessDistance;
    public GameObject AccessUI;
    public string ID;
    public List<ItemSlot> slots;
    public List<Item> items;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RefreshSlots();
    }

    private void Update()
    {
        //Debug.Log(GM.CompareDistance(transform.position, player.transform.position, accessDistance));
        if(GM.CompareDistance(transform.position, player.transform.position, accessDistance) <= 0 && Input.GetKeyDown("e") && !opened)
        {
            Access(true);
        }
        if((GM.CompareDistance(transform.position, player.transform.position, accessDistance) == 1 && opened))
        {
            Access(false);
        }

        if (opened)
        {
            if (Input.GetKeyDown("1"))
                PickUp(0);
            if (Input.GetKeyDown("2"))
                PickUp(1);
            if (Input.GetKeyDown("3"))
                PickUp(2);
            if (Input.GetKeyDown("4"))
                PickUp(3);
            if (Input.GetKeyDown("5"))
                PickUp(4);
            if (Input.GetKeyDown("6"))
                PickUp(5);
            if (Input.GetKeyDown("7"))
                PickUp(6);
            if (Input.GetKeyDown("8"))
                PickUp(7);
            if (Input.GetKeyDown("g"))
            {
                foreach(Item item in items)
                {
                    PickUp(items.IndexOf(item));
                }
            }
        }
    }

    public void Access(bool open)
    {
        if (open)
        {
            AccessUI.SetActive(true);
            opened = true;
        }
        else
        {
            AccessUI.SetActive(false);
            opened = false;
        }
    }

    void RefreshSlots()
    {
        foreach (ItemSlot slot in slots)
        {
            slot.TYPE_ID.text = string.Empty;
            slot.COUNT.text = string.Empty;
        }

        for (int i = 0; i < items.Count; i++)
        {
            slots[i].TYPE_ID.text = " > " + items[i].type + "\n " + items[i].name;
            if (items[i].count != 0)
                slots[i].COUNT.text = "x (" + items[i].count + ")";
        }
    }

    public void PickUp(int index)
    {
        Debug.Log("Picking Up: " + index);
        if (index >= items.Count)
            return;
        Item item = items[index];
        bool remain = false;
        switch (item.type)
        {
            case "Ammo":
                PlayerWeaponManager wm = player.GetComponent<PlayerWeaponManager>();
                wm.ammo_count[wm.ammo_type.IndexOf(item.name)] += Mathf.RoundToInt(item.count);
                items.Remove(item);
                break;
            case "Health":
                PlayerStats ps = player.GetComponent<PlayerStats>();
                ps.health += item.count;
                items.Remove(item);
                break;
            case "Fuel":
                PlayerMove pm = player.GetComponent<PlayerMove>();
                float leftover = pm.AddFuel(item.count);
                if (leftover != 0)
                {
                    item.count = leftover;
                    remain = true;
                }
                items.Remove(item);
                break;
            case "Module":
                PlayerModuleManager mm = player.GetComponent<PlayerModuleManager>();
                if (mm.InsertModule(item.module) == false)
                {
                    remain = true;
                }
                items.Remove(item);
                break;
            case "Weapon":
                GameObject wp = Instantiate((item.obj), transform.position, Quaternion.identity);
                wp.GetComponent<WeaponStats>()._GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
                wp.GetComponent<WeaponStats>().PickUp(player.GetComponent<PlayerPickUp>().weaponsFolder.transform);
                items.Remove(item);
                break;
        }
        if (remain)
            items.Add(item);

        RefreshSlots();
    }

    public void PickUp(Item item)
    {
        bool remain = false;
        switch (item.type)
        {
            case "Ammo":
                PlayerWeaponManager wm = player.GetComponent<PlayerWeaponManager>();
                wm.ammo_count[wm.ammo_type.IndexOf(item.name)] += Mathf.RoundToInt(item.count);
                items.Remove(item);
                break;
            case "Health":
                PlayerStats ps = player.GetComponent<PlayerStats>();
                ps.health += item.count;
                items.Remove(item);
                break;
            case "Fuel":
                PlayerMove pm = player.GetComponent<PlayerMove>();
                float leftover = pm.AddFuel(item.count);
                if (leftover != 0)
                {
                    item.count = leftover;
                    remain = true;
                }
                items.Remove(item);
                break;
            case "Module":
                PlayerModuleManager mm = player.GetComponent<PlayerModuleManager>();
                if (mm.InsertModule(item.module) == false)
                {
                    remain = true;
                }
                items.Remove(item);
                break;
            case "Weapon":
                GameObject wp = Instantiate((item.obj), transform.position, Quaternion.identity);
                wp.GetComponent<WeaponStats>()._GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
                wp.GetComponent<WeaponStats>().PickUp(player.GetComponent<PlayerPickUp>().weaponsFolder.transform);
                items.Remove(item);
                break;
        }
        if (remain)
            items.Add(item);

        RefreshSlots();
    }
}
