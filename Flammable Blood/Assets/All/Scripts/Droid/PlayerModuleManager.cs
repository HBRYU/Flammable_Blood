using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModuleManager : MonoBehaviour
{
    public List<Module> modules;
    public List<string> modules_IDs;
    public GameObject item_module;
    public UI_ControlPanel2 controlPanel;

    void Start()
    {
        //RESET modules_IDs
        modules_IDs = new List<string>();

        foreach (Module module in modules)
        {
            modules_IDs.Add(module.ID);
        }
        controlPanel = GameObject.FindGameObjectWithTag("UI").GetComponent<UI_ControlPanel2>();
    }

    void Update()
    {
        
    }

    public bool InsertModule(Module module)
    {
        
        switch (module.ID)
        {
            case "JetpackFuelEfficiency":
                if (!M_JetpackFuelEfficiency(module))
                    return (false);
                break;
            case "MovementFuelEfficiency":
                if(!M_MovementFuelEfficiency(module))
                    return (false);
                break;
            case "FuelCapacity":
                if(!M_FuelCapacity(module))
                    return (false);
                break;
            case "Jetpack":
                //Debug.Log("FUCK");
                if (!M_Jetpack(module))
                    return (false);
                break;
            case "JetpackThermalInsulation":
                if (!M_JetpackThermalInsulation(module))
                    return (false);
                break;
            case "JetpackThrust":
                if (!M_JetpackThrust(module))
                    return (false);
                break;
            case "WeaponCapacity":
                if (!M_WeaponCapacity(module))
                    return (false);
                break;
            case "Deployables":
                if (!M_Deployables(module))
                    return (false);
                break;
            case "Shield":
                if (!M_Shield(module))
                    return (false);
                break;
        }
        GM.DisplayText("Module Inserted:" + module.ID, false);
        GM.DisplayText3("Module Inserted:\n [" + module.ID + "] LV."+ module.level, false, 0);
        //GM.DisplayText2(module.description, false);
        controlPanel.AddModule(module);
        return (true);
    }

    public void UpdateModule(Module module, int state)
    {
        Debug.Log("s: 2: " + module.ID);
        switch (module.ID)
        {
            case "JetpackFuelEfficiency":
                M_JetpackFuelEfficiency(module, state);
                break;
            case "MovementFuelEfficiency":
                M_MovementFuelEfficiency(module, state);
                break;
            case "FuelCapacity":
                M_FuelCapacity(module, state);
                break;
            case "Jetpack":
                M_Jetpack(module, state);
                break;
            case "JetpackThermalInsulation":
                M_JetpackThermalInsulation(module, state);
                break;
            case "JetpackThrust":
                M_JetpackThrust(module, state);
                break;
            case "WeaponCapacity":
                M_WeaponCapacity(module, state);
                break;
            case "Deployables":
                M_Deployables(module, state);
                break;
            case "Shield":
                M_Shield(module, state);
                break;
        }
        
    }

    void SpawnModule(Module module)
    {
        GameObject thisItem = item_module;
        thisItem.GetComponent<Item_Module>().module = modules[modules_IDs.IndexOf(module.ID)];
        thisItem.GetComponent<SpriteRenderer>().sprite = modules[modules_IDs.IndexOf(module.ID)].IMG;
        Instantiate(thisItem, transform.position, transform.rotation);
        modules.Remove(modules[modules_IDs.IndexOf(module.ID)]);
        modules_IDs.Remove(modules_IDs[modules_IDs.IndexOf(module.ID)]);
    }

    void SpawnModule(Module module, bool direct)
    {
        GameObject thisItem = item_module;
        thisItem.GetComponent<Item_Module>().module = modules[modules.IndexOf(module)];
        thisItem.GetComponent<SpriteRenderer>().sprite = modules[modules.IndexOf(module)].IMG;
        Instantiate(thisItem, transform.position, transform.rotation);
        modules.Remove(modules[modules.IndexOf(module)]);
        modules_IDs.Remove(modules_IDs[modules.IndexOf(module)]);
    }

    //MODULE INSERTION FUNCTIONS
    //////////////////////////////////////////////

    bool M_JetpackFuelEfficiency(Module module)
    {
        if (!modules_IDs.Contains(module.ID) || modules[modules_IDs.IndexOf(module.ID)].level != module.level)
        {
            //SAME MODULE EXISTS WITH LOWER LEVEL
            if (modules_IDs.IndexOf(module.ID) != -1 && modules[modules_IDs.IndexOf(module.ID)].level != module.level)
            {
                SpawnModule(module);
            }

            //CHANGE STATS
            if (module.level == 1)
                GetComponent<PlayerMove>().jetpack_fuel_efficiency = 0.5f;
            if (module.level == 2)
                GetComponent<PlayerMove>().jetpack_fuel_efficiency = 0.9f;
            if (module.level == 3)
                GetComponent<PlayerMove>().jetpack_fuel_efficiency = 1.2f;

            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            GM.DisplayText3("Module [" + module.ID + "] LV.(" + module.level + " or higher) already exsists", false, 0);
            return (false);
        }
    }

    void M_JetpackFuelEfficiency(Module module, int state)
    {
        if (state == 0)
        {
            SpawnModule(module, true);
            GetComponent<PlayerMove>().jetpack_fuel_efficiency = 0f;
        }
        else if(state == 1)
        {
            GetComponent<PlayerMove>().jetpack_fuel_efficiency = 0f;
        }
        else
        {
            if (module.level == 1)
                GetComponent<PlayerMove>().jetpack_fuel_efficiency = 0.5f;
            if (module.level == 2)
                GetComponent<PlayerMove>().jetpack_fuel_efficiency = 0.9f;
            if (module.level == 3)
                GetComponent<PlayerMove>().jetpack_fuel_efficiency = 1.2f;
        }
    }

    //-----------------------------------------------------------------

    bool M_MovementFuelEfficiency(Module module)
    {
        if (!modules_IDs.Contains(module.ID) || modules[modules_IDs.IndexOf(module.ID)].level != module.level)
        {
            //SAME MODULE EXISTS WITH LOWER LEVEL
            if (modules_IDs.IndexOf(module.ID) != -1 && modules[modules_IDs.IndexOf(module.ID)].level != module.level)
            {
                SpawnModule(module);
            }

            //CHANGE STATS
            if (module.level == 1)
                GetComponent<PlayerMove>().move_fuel_efficiency = 35f;
            if (module.level == 2)
                GetComponent<PlayerMove>().move_fuel_efficiency = 45f;
            if (module.level == 3)
                GetComponent<PlayerMove>().move_fuel_efficiency = 55f;

            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            GM.DisplayText3("Module [" + module.ID + "] LV.(" + module.level + " or higher) already exsists", false, 0);
            return (false);
        }
    }

    void M_MovementFuelEfficiency(Module module, int state)
    {
        if (state == 0)
        {
            SpawnModule(module, true);
            GetComponent<PlayerMove>().move_fuel_efficiency = 0f; ;
        }
        else if (state == 1)
        {
            GetComponent<PlayerMove>().move_fuel_efficiency = 0f;
        }
        else
        {
            if (module.level == 1)
                GetComponent<PlayerMove>().move_fuel_efficiency = 35f;
            if (module.level == 2)
                GetComponent<PlayerMove>().move_fuel_efficiency = 45f;
            if (module.level == 3)
                GetComponent<PlayerMove>().move_fuel_efficiency = 55f;
        }
    }

    //-----------------------------------------------------------------

    bool M_FuelCapacity(Module module)
    {
        if (!modules_IDs.Contains(module.ID) || modules[modules_IDs.IndexOf(module.ID)].level != module.level)
        {
            //SAME MODULE EXISTS WITH LOWER LEVEL
            if (modules_IDs.IndexOf(module.ID) != -1 && modules[modules_IDs.IndexOf(module.ID)].level != module.level)
            {
                SpawnModule(module);
            }

            //CHANGE STATS
            if (module.level == 1)
                GetComponent<PlayerMove>().maxFuel = 1600;
            if (module.level == 2)
                GetComponent<PlayerMove>().maxFuel = 1850;
            if (module.level == 3)
                GetComponent<PlayerMove>().maxFuel = 2200;
            if (module.level == 4)
                GetComponent<PlayerMove>().maxFuel = 2500;
            if (module.level == 5)
                GetComponent<PlayerMove>().maxFuel = 2800;

            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            GM.DisplayText3("Module [" + module.ID + "] LV.(" + module.level + " or higher) already exsists", false, 0);
            return (false);
        }
    }

    void M_FuelCapacity(Module module, int state)
    {
        if (state == 0)
        {
            SpawnModule(module, true);
            GetComponent<PlayerMove>().maxFuel = 1200;
        }
        else if (state == 1)
        {
            GetComponent<PlayerMove>().maxFuel = 1200;
        }
        else
        {
            if (module.level == 1)
                GetComponent<PlayerMove>().maxFuel = 1600;
            if (module.level == 2)
                GetComponent<PlayerMove>().maxFuel = 1850;
            if (module.level == 3)
                GetComponent<PlayerMove>().maxFuel = 2200;
            if (module.level == 4)
                GetComponent<PlayerMove>().maxFuel = 2500;
            if (module.level == 5)
                GetComponent<PlayerMove>().maxFuel = 2800;
        }
    }

    //-----------------------------------------------------------------

    bool M_Jetpack(Module module)
    {
        if (!modules_IDs.Contains(module.ID))
        {
            //CHANGE STATS
            GetComponent<PlayerMove>().jetpack = true;
            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            GM.DisplayText3("Module [" + module.ID + "] LV.(" + module.level + " or higher) already exsists", false, 0);
            return (false);
        }
    }

    void M_Jetpack(Module module, int state)
    {
        if (state == 0)
        {
            SpawnModule(module, true);
            GetComponent<PlayerMove>().jetpack = false;
        }
        else if (state == 1)
        {
            GetComponent<PlayerMove>().jetpack = false;
        }
        else
        {
            GetComponent<PlayerMove>().jetpack = true;
        }
    }

    //-----------------------------------------------------------------

    bool M_JetpackThermalInsulation(Module module)
    {
        if (!modules_IDs.Contains(module.ID) || modules[modules_IDs.IndexOf(module.ID)].level != module.level)
        {
            //SAME MODULE EXISTS WITH LOWER LEVEL
            if (modules_IDs.IndexOf(module.ID) != -1 && modules[modules_IDs.IndexOf(module.ID)].level != module.level)
            {
                SpawnModule(module);
            }

            //CHANGE STATS
            if (module.level == 1)
                GetComponent<PlayerMove>().jetpack_overheat = 160f;
            if (module.level == 2)
                GetComponent<PlayerMove>().jetpack_overheat = 190f;
            if (module.level == 3)
                GetComponent<PlayerMove>().jetpack_overheat = 210f;

            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            GM.DisplayText3("Module [" + module.ID + "] LV.(" + module.level + " or higher) already exsists", false, 0);
            return (false);
        }
    }

    void M_JetpackThermalInsulation(Module module, int state)
    {
        if (state == 0)
        {
            SpawnModule(module, true);
            GetComponent<PlayerMove>().jetpack_overheat = 120f;
        }
        else if (state == 1)
        {
            GetComponent<PlayerMove>().jetpack_overheat = 120f;
        }
        else
        {
            if (module.level == 1)
                GetComponent<PlayerMove>().jetpack_overheat = 160f;
            if (module.level == 2)
                GetComponent<PlayerMove>().jetpack_overheat = 190f;
            if (module.level == 3)
                GetComponent<PlayerMove>().jetpack_overheat = 210f;
        }
    }


    //-----------------------------------------------------------------


    bool M_JetpackThrust(Module module)
    {
        if (!modules_IDs.Contains(module.ID) || modules[modules_IDs.IndexOf(module.ID)].level != module.level)
        {
            //SAME MODULE EXISTS WITH LOWER LEVEL
            if (modules_IDs.IndexOf(module.ID) != -1 && modules[modules_IDs.IndexOf(module.ID)].level != module.level)
            {
                SpawnModule(module);
            }

            //CHANGE STATS
            if (module.level == 1)
                GetComponent<PlayerMove>().jetpack_force = 2f;
            if (module.level == 2)
                GetComponent<PlayerMove>().jetpack_force = 2.5f;
            if (module.level == 3)
                GetComponent<PlayerMove>().jetpack_force = 3f;
            if (module.level == 4)
                GetComponent<PlayerMove>().jetpack_force = 3.5f;
            if (module.level == 5)
                GetComponent<PlayerMove>().jetpack_force = 4f;

            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            GM.DisplayText3("Module [" + module.ID + "] LV.(" + module.level + " or higher) already exsists", false, 0);
            return (false);
        }
    }

    void M_JetpackThrust(Module module, int state)
    {
        if (state == 0)
        {
            SpawnModule(module, true);
            GetComponent<PlayerMove>().jetpack_force = 1.5f;
        }
        else if (state == 1)
        {
            GetComponent<PlayerMove>().jetpack_force = 1.5f;
        }
        else
        {
            if (module.level == 1)
                GetComponent<PlayerMove>().jetpack_force = 2f;
            if (module.level == 2)
                GetComponent<PlayerMove>().jetpack_force = 2.5f;
            if (module.level == 3)
                GetComponent<PlayerMove>().jetpack_force = 3f;
            if (module.level == 4)
                GetComponent<PlayerMove>().jetpack_force = 3.5f;
            if (module.level == 5)
                GetComponent<PlayerMove>().jetpack_force = 4f;
        }
    }

    //-----------------------------------------------------------------

    bool M_WeaponCapacity(Module module)
    {
        if (!modules_IDs.Contains(module.ID) || modules[modules_IDs.IndexOf(module.ID)].level != module.level)
        {
            //SAME MODULE EXISTS WITH LOWER LEVEL
            if (modules_IDs.IndexOf(module.ID) != -1 && modules[modules_IDs.IndexOf(module.ID)].level != module.level)
            {
                SpawnModule(module);
            }

            //CHANGE STATS
            if (module.level == 1)
                GetComponent<PlayerWeaponManager>().maxWeaponCount = 3;
            if (module.level == 2)
                GetComponent<PlayerWeaponManager>().maxWeaponCount = 4;

            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            GM.DisplayText3("Module [" + module.ID + "] LV.(" + module.level + " or higher) already exsists", false, 0);
            return (false);
        }
    }

    void M_WeaponCapacity(Module module, int state)
    {
        if (state == 0)
        {
            SpawnModule(module, true);
            GetComponent<PlayerWeaponManager>().maxWeaponCount = 2;
        }
        else if (state == 1)
        {
            GetComponent<PlayerWeaponManager>().maxWeaponCount = 2;
        }
        else
        {
            if (module.level == 1)
                GetComponent<PlayerWeaponManager>().maxWeaponCount = 3;
            if (module.level == 2)
                GetComponent<PlayerWeaponManager>().maxWeaponCount = 4;
        }
    }

    //-----------------------------------------------------------------


    bool M_Deployables(Module module)
    {
        if (!modules_IDs.Contains(module.ID))
        {
            //CHANGE STATS
            GetComponent<DeployablesManager>().ACTIVE = true;

            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            GM.DisplayText3("Module [" + module.ID + "] LV.(" + module.level + " or higher) already exsists", false, 0);
            return (false);
        }
    }

    void M_Deployables(Module module, int state)
    {
        if (state == 0)
        {
            SpawnModule(module, true);
            GetComponent<DeployablesManager>().ACTIVE = false;
        }
        else if (state == 1)
        {
            GetComponent<DeployablesManager>().ACTIVE = false;
        }
        else
        {
            GetComponent<DeployablesManager>().ACTIVE = true;
        }
    }

    //-----------------------------------------------------------------

    bool M_Shield(Module module)
    {
        if (!modules_IDs.Contains(module.ID))
        {
            //CHANGE STATS
            GetComponent<PlayerShield>().enabled = true;
            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            GM.DisplayText3("Module [" + module.ID + "] LV.(" + module.level + " or higher) already exsists", false, 0);
            return (false);
        }
    }

    void M_Shield(Module module, int state)
    {
        if (state == 0)
        {
            SpawnModule(module, true);
            GetComponent<PlayerShield>().enabled = false;
        }
        else if (state == 1)
        {
            GetComponent<PlayerShield>().enabled = false;
        }
        else
        {
            GetComponent<PlayerShield>().enabled = true;
        }
    }
}
