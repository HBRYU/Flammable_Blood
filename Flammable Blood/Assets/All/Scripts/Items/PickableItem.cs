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
                GM.DisplayText3("Picked up: " + name + " x (" + count + ")", false, 0);
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
                        GM.DisplayText3("Picked up: " + name + " GRND x (" + count + ")", false, 0);
                        break;
                    }
                }
                Debug.Log("ERR: Unknown deployable item name");
                break;
            case "Health":
                PlayerStats ps = GM.GetPlayer().GetComponent<PlayerStats>();
                ps.health += count;
                GM.DisplayText3("Health += " + count, false, 0);
                break;
            case "Fuel":
                PlayerMove pm = GM.GetPlayer().GetComponent<PlayerMove>();
                float leftover = pm.AddFuel(count);
                if (leftover != 0)
                {
                    if (leftover == count)
                        GM.DisplayText3("Maximum fuel capacity reached", false, 0);
                    else
                    {
                        GM.DisplayText3("Fuel += " + (count - leftover) + "mL", false, 0);
                        count = leftover;
                        remain = true;
                    }
                }
                else
                    GM.DisplayText3("Fuel += " + count + "mL", false, 0);
                break;
        }

        if (!remain)
            Destroy(gameObject);
    }
}
