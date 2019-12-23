using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimpleItem
{
    public string type;
    public string name;
    [Header("Alter module levels with 'count'")]
    public float count;
}


public class Crate_AutoAdd : MonoBehaviour
{
    public SimpleItem[] items;

    public List<string> itemTypes;
    public List<string> itemNames;
    public List<float> itemCounts;

    private Crate crate;

    void Start()
    {
        crate = GetComponent<Crate>();

        foreach(SimpleItem i in items)
        {
            Debug.Log(i.type);
            itemTypes.Add(i.type);
            itemNames.Add(i.name);
            itemCounts.Add(i.count);
        }

        crate.items = new List<Item>();
        GM _GM_ = GM.GetGM();

        int m = 0; //Modules index

        for(int i = 0; i < itemNames.Count; i++)
        {
            Item item = new Item();
            item.name = itemNames[i];
            item.count = itemCounts[i];
            item.type = itemTypes[i];
            switch (itemTypes[i])
            {
                case "Ammo":
                    crate.items.Add(item);
                    break;
                case "Health":
                    crate.items.Add(item);
                    break;
                case "Fuel":
                    crate.items.Add(item);
                    break;
                case "Weapon":
                    foreach(Item _GM_I in _GM_.itemsList)
                    {
                        if(_GM_I.name == itemNames[i])
                        {
                            crate.items.Add(_GM_I);
                        }
                    }
                    break;
                case "Deployable":
                    foreach (Item _GM_I in _GM_.itemsList)
                    {
                        if (_GM_I.name == itemNames[i])
                        {
                            Item a = _GM_I;
                            a.count = Mathf.RoundToInt(itemCounts[i]);
                            crate.items.Add(_GM_I);
                        }
                    }
                    break;
                case "Module":
                    foreach (Module _GM_M in _GM_.modulesList)
                    {
                        if (_GM_M.ID == itemNames[i] && _GM_M.level == itemCounts[i])
                        {
                            item.module = _GM_M;
                            item.count = 0;
                            crate.items.Add(item);
                        }
                    }
                    break;
                default:
                    Debug.Log("ERR: Unknown module type. Fuck?");
                    break;

            }
        }
        GetComponent<Crate_AutoAdd>().enabled = false;
    }
}
