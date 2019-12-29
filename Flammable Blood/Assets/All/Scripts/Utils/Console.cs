using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    public TMP_InputField inputField;

    public void commandEntered()
    {
        if(inputField.text != string.Empty)
            EnterCommand(inputField.text);
    }


    public void EnterCommand(string com)
    {
        string[] coms = com.Split('/');
        /*
        for (int i = 0; i < coms.Length; i++)
        {
            coms[i] = coms[i].ToLower();
            Debug.Log(coms[i]);
        }
        */

        try
        {
            switch (coms[0])
            {
                case "player":
                    switch (coms[1])
                    {
                        case "health":
                            if (coms[2] == "max")
                                GM.GetGM().player.GetComponent<PlayerStats>().health = GM.GetGM().player.GetComponent<PlayerStats>().maxHealth;
                            else
                                GM.GetGM().player.GetComponent<PlayerStats>().health = GM.GetFloat(coms[2], GM.GetGM().player.GetComponent<PlayerStats>().health);
                            GM.DisplayText("Command entered.", true);
                            break;
                        case "maxhealth":
                            GM.GetGM().player.GetComponent<PlayerStats>().maxHealth = GM.GetFloat(coms[2], GM.GetGM().player.GetComponent<PlayerStats>().maxHealth);
                            GM.DisplayText("Command entered.", true);
                            break;
                        case "fuel":
                            GM.GetGM().player.GetComponent<PlayerMove>().fuel = GM.GetFloat(coms[2], GM.GetGM().player.GetComponent<PlayerMove>().fuel);
                            GM.DisplayText("Command entered.", true);
                            break;
                        case "maxfuel":
                            GM.GetGM().player.GetComponent<PlayerMove>().maxFuel = GM.GetFloat(coms[2], GM.GetGM().player.GetComponent<PlayerMove>().maxFuel);
                            GM.DisplayText("Command entered.", true);
                            break;
                        default:
                            GM.DisplayText("Unknown command", true);
                            break;
                    }
                    break;
                case "getitem":
                    switch (coms[1])
                    {
                        case "module":
                            foreach (Module _GM_M in GM.GetGM().modulesList)
                            {
                                if (_GM_M.ID == coms[2] && _GM_M.level == GM.GetFloat(coms[3], 1))
                                {
                                    PlayerModuleManager mm = GM.GetPlayer().GetComponent<PlayerModuleManager>();
                                    if (mm.InsertModule(_GM_M) == false)
                                    {
                                        GM.DisplayText("Unable to add module", true);
                                    }
                                }
                            }
                            break;
                        case "weapon":
                            foreach (Item _GM_I in GM.GetGM().itemsList)
                            {
                                if (_GM_I.name == coms[2])
                                {
                                    GameObject wp = Instantiate((_GM_I.obj), transform.position, Quaternion.identity);
                                    wp.GetComponent<WeaponStats>()._GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
                                    wp.GetComponent<WeaponStats>().PickUp(GM.GetPlayer().GetComponent<PlayerPickUp>().weaponsFolder.transform);
                                    GM.DisplayText("Picked up: " + _GM_I.name, false);
                                    GM.DisplayText("Command entered.", true);
                                }
                            }
                            break;

                        case "deployable":
                            foreach (Item _GM_I in GM.GetGM().itemsList)
                            {
                                if (_GM_I.name == coms[2])
                                {
                                    Item a = _GM_I;
                                    a.count = Mathf.RoundToInt(GM.GetFloat(coms[3], GM.GetGM().player.GetComponent<PlayerMove>().maxFuel));
                                    DeployablesManager dm = GM.GetPlayer().GetComponent<DeployablesManager>();
                                    dm.dplybles_count[dm.deployables.IndexOf(a.obj)] += (int)a.count;
                                    GM.DisplayText("Picked up: " + a.name + " GRND x (" + a.count + ")", false);
                                    GM.DisplayText("Command entered.", true);
                                }
                            }
                            break;
                        case "ammo":
                            PlayerWeaponManager wm = GM.GetPlayer().GetComponent<PlayerWeaponManager>();
                            wm.ammo_count[wm.ammo_type.IndexOf(coms[2])] += Mathf.RoundToInt(GM.GetFloat(coms[3], 0));
                            GM.DisplayText("Picked up: " + coms[2] + " x (" + GM.GetFloat(coms[3], 0) + ")", false);
                            break;
                        default:
                            GM.DisplayText("Unknown command", true);
                            break;
                    }
                    break;
                case "clock":
                    if(coms[1] == "pause")
                    {
                        GM.GetGM().gameObject.GetComponent<Clock>().paused = true;
                    }
                    else if (coms[1] == "resume")
                    {
                        GM.GetGM().gameObject.GetComponent<Clock>().paused = false;
                    }
                    else if(coms[1] == "time")
                    {
                        GM.GetGM().gameObject.GetComponent<Clock>().remainingTime = GM.GetFloat(coms[2], GM.GetGM().gameObject.GetComponent<Clock>().remainingTime);
                    }
                    else
                    {
                        GM.DisplayText("Unknown command", true);
                    }
                    break;
                case "scores":
                    if(coms[1] == "show")
                    {
                        GM.GetUI().GetComponent<UI_Scores>().Show();
                    }
                    if(coms[1] == "hide")
                    {
                        GM.GetUI().GetComponent<UI_Scores>().Hide();
                    }
                    if (coms[1] == "save")
                    {
                        GM.GetGM().data.SaveData();
                    }
                    if(coms[1] == "clear")
                    {
                        GM.GetGM().data.ResetData();
                    }
                    if(coms[1] == "pw")
                    {
                        GM.DisplayText("QWERTY", true);
                    }
                    break;
                default:
                    GM.DisplayText("Unknown command", true);
                    break;
            }
        }
        catch
        {
            GM.DisplayText("Invalid command.", true);
        }
    }
}
