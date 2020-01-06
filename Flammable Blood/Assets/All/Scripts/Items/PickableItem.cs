using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickableItem : MonoBehaviour
{
    public string name;
    public string type;
    public float count;

    public bool useUI;
    public GameObject UI;
    public TextMeshProUGUI UI_Text;

    void Start()
    {
        if (useUI)
        {
            UI_Text.text = "[" + type + "] \n> " + name + "\nx " + count.ToString();
        }
    }

    public void Access()
    {
        bool remain = false;
        switch (type)
        {
            case "Ammo":
                PlayerWeaponManager wm = GM.GetPlayer().GetComponent<PlayerWeaponManager>();
                wm.ammo_count[wm.ammo_type.IndexOf(name)] += Mathf.RoundToInt(count);
                GM.DisplayText("Picked up: " + name + " x (" + count + ")", false);
                break;
            case "Deployable":
                foreach (Item _GM_I in GM.GetGM().itemsList)
                {
                    if (_GM_I.name == name)
                    {
                        Item a = _GM_I;
                        a.count = Mathf.RoundToInt(count);
                        DeployablesManager dm = GM.GetPlayer().GetComponent<DeployablesManager>();
                        dm.dplybles_count[dm.deployables.IndexOf(a.obj)] += (int)a.count;
                        GM.DisplayText("Picked up: " + name + " GRND x (" + count + ")", false);
                        break;
                    }
                }
                Debug.Log("ERR: Unknown deployable item name");
                break;
            case "Health":
                PlayerStats ps = GM.GetPlayer().GetComponent<PlayerStats>();
                ps.health += count;
                GM.DisplayText("Health += " + count, false);
                break;
            case "Fuel":
                PlayerMove pm = GM.GetPlayer().GetComponent<PlayerMove>();
                float leftover = pm.AddFuel(count);
                if (leftover != 0)
                {
                    if (leftover == count)
                        GM.DisplayText("Maximum fuel capacity reached", false);
                    else
                    {
                        GM.DisplayText("Fuel += " + (count - leftover) + "mL", false);
                        count = leftover;
                        remain = true;
                    }
                }
                else
                    GM.DisplayText("Fuel += " + count + "mL", false);
                break;
        }

        if (!remain)
            Destroy(gameObject);
    }
}
