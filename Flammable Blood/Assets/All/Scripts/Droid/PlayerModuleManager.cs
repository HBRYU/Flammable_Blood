using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModuleManager : MonoBehaviour
{
    public List<Module> modules;
    public List<string> modules_IDs;
    public GameObject item_module;

    void Awake()
    {
        //RESET modules_IDs
        modules_IDs = new List<string>();

        foreach (Module module in modules)
        {
            modules_IDs.Add(module.ID);
        }
    }

    void Update()
    {
        
    }

    public bool InsertModule(Module module)
    {
        Debug.Log("Inserting module");
        switch (module.ID)
        {
            case "JetpackFuelEfficiency":
                if (!M_JetpackFuelEfficiency(module))
                    return (false);
                break;
            case "MoveFuelEfficiency":
                if(!M_JetpackFuelEfficiency(module))
                    return (false);
                break;
            case "FuelCapacity":
                if(!M_JetpackFuelEfficiency(module))
                    return (false);
                break;
            case "Jetpack":
                if (!M_JetpackFuelEfficiency(module))
                    return (false);
                break;
            case "JetpackThermalInsulation":
                if (!M_JetpackFuelEfficiency(module))
                    return (false);
                break;
            case "JetpackThrust":
                if (!M_JetpackFuelEfficiency(module))
                    return (false);
                break;
            case "WeaponCapacity":
                if (!M_JetpackFuelEfficiency(module))
                    return (false);
                break;
            case "Deployables":
                if (!M_JetpackFuelEfficiency(module))
                    return (false);
                break;
        }
        return (true);
    }

    public void RemoveModule(Module module)
    {
        switch (module.ID)
        {
            case "JetpackFuelEfficiency":
                M_JetpackFuelEfficiency(module, true);
                break;
            case "MoveFuelEfficiency":
                M_JetpackFuelEfficiency(module, true);
                break;
            case "FuelCapacity":
                M_JetpackFuelEfficiency(module, true);
                break;
            case "Jetpack":
                M_JetpackFuelEfficiency(module, true);
                break;
            case "JetpackThermalInsulation":
                M_JetpackFuelEfficiency(module, true);
                break;
            case "JetpackThrust":
                M_JetpackFuelEfficiency(module, true);
                break;
            case "WeaponCapacity":
                M_JetpackFuelEfficiency(module, true);
                break;
            case "Deployables":
                M_JetpackFuelEfficiency(module, true);
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
            Debug.Log("Module (" + module.ID + "_Level " + module.level + ") already exists");
            return (false);
        }
    }

    void M_JetpackFuelEfficiency(Module module, bool remove)
    {
        SpawnModule(module, true);
        GetComponent<PlayerMove>().jetpack_fuel_efficiency = 0f;
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
            Debug.Log("Module (" + module.ID + "_Level " + module.level + ") already exists");
            return (false);
        }
    }

    void M_MovementFuelEfficiency(Module module, bool remove)
    {
        SpawnModule(module, true);
        GetComponent<PlayerMove>().move_fuel_efficiency = 0f;
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
            Debug.Log("Module (" + module.ID + "_Level " + module.level + ") already exists");
            return (false);
        }
    }

    void M_FuelCapacity(Module module, bool remove)
    {
        SpawnModule(module, true);
        GetComponent<PlayerMove>().maxFuel = 1200;
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
            Debug.Log("Module (" + module.ID + "_Level " + module.level + ") already exists");
            return (false);
        }
    }

    void M_Jetpack(Module module, bool remove)
    {
        SpawnModule(module, true);
        GetComponent<PlayerMove>().jetpack = true;
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
            Debug.Log("Module (" + module.ID + "_Level " + module.level + ") already exists");
            return (false);
        }
    }

    void M_JetpackThermalInsulation(Module module, bool remove)
    {
        SpawnModule(module, true);
        GetComponent<PlayerMove>().jetpack_overheat = 120f;
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
                GetComponent<PlayerMove>().jetpack_force = 1.75f;
            if (module.level == 2)
                GetComponent<PlayerMove>().jetpack_force = 2f;
            if (module.level == 3)
                GetComponent<PlayerMove>().jetpack_force = 2.25f;
            if (module.level == 4)
                GetComponent<PlayerMove>().jetpack_force = 2.5f;
            if (module.level == 5)
                GetComponent<PlayerMove>().jetpack_force = 3f;

            modules.Add(module);
            modules_IDs.Add(module.ID);
            return (true);
        }
        else
        {
            Debug.Log("Module (" + module.ID + "_Level " + module.level + ") already exists");
            return (false);
        }
    }

    void M_JetpackThrust(Module module, bool remove)
    {
        SpawnModule(module, true);
        GetComponent<PlayerMove>().jetpack_force = 1.5f;
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
            Debug.Log("Module (" + module.ID + "_Level " + module.level + ") already exists");
            return (false);
        }
    }

    void M_WeaponCapacity(Module module, bool remove)
    {
        SpawnModule(module, true);
        GetComponent<PlayerWeaponManager>().maxWeaponCount = 2;
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
            Debug.Log("Module (" + module.ID + "_Level " + module.level + ") already exists");
            return (false);
        }
    }

    void M_Deployables(Module module, bool remove)
    {
        SpawnModule(module, true);
        GetComponent<DeployablesManager>().ACTIVE = false;
    }

    //-----------------------------------------------------------------
}
