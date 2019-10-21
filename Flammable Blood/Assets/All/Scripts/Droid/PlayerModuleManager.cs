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
        if (module.ID.Contains("FuelEfficiency"))
        {
            if (module.ID.Contains("Jetpack"))
            {
                if (M_JetpackFuelEfficiency(module) == false)
                    return (false);
            }
            if (module.ID.Contains("Movement"))
            {
                if (M_MovementFuelEfficiency(module) == false)
                    return (false);
            }
        }
        if (module.ID.Contains("FuelCapacity"))
        {
            if (M_FuelCapacity(module) == false)
                return (false);
        }

        return (true);
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
                Debug.Log("FUCK");
                GameObject thisItem = item_module;
                thisItem.GetComponent<Item_Module>().module = modules[modules_IDs.IndexOf(module.ID)];
                thisItem.GetComponent<SpriteRenderer>().sprite = modules[modules_IDs.IndexOf(module.ID)].IMG;
                Instantiate(thisItem, transform.position, transform.rotation);
                modules.Remove(modules[modules_IDs.IndexOf(module.ID)]);
                modules_IDs.Remove(modules_IDs[modules_IDs.IndexOf(module.ID)]);
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

    bool M_MovementFuelEfficiency(Module module)
    {
        if (!modules_IDs.Contains(module.ID) || modules[modules_IDs.IndexOf(module.ID)].level != module.level)
        {
            //SAME MODULE EXISTS WITH LOWER LEVEL
            if (modules_IDs.IndexOf(module.ID) != -1 && modules[modules_IDs.IndexOf(module.ID)].level != module.level)
            {
                Debug.Log("FUCK");
                GameObject thisItem = item_module;
                thisItem.GetComponent<Item_Module>().module = modules[modules_IDs.IndexOf(module.ID)];
                thisItem.GetComponent<SpriteRenderer>().sprite = modules[modules_IDs.IndexOf(module.ID)].IMG;
                Instantiate(thisItem, transform.position, transform.rotation);
                modules.Remove(modules[modules_IDs.IndexOf(module.ID)]);
                modules_IDs.Remove(modules_IDs[modules_IDs.IndexOf(module.ID)]);
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

    bool M_FuelCapacity(Module module)
    {
        if (!modules_IDs.Contains(module.ID) || modules[modules_IDs.IndexOf(module.ID)].level != module.level)
        {
            //SAME MODULE EXISTS WITH LOWER LEVEL
            if (modules_IDs.IndexOf(module.ID) != -1 && modules[modules_IDs.IndexOf(module.ID)].level != module.level)
            {
                Debug.Log("FUCK");
                GameObject thisItem = item_module;
                thisItem.GetComponent<Item_Module>().module = modules[modules_IDs.IndexOf(module.ID)];
                thisItem.GetComponent<SpriteRenderer>().sprite = modules[modules_IDs.IndexOf(module.ID)].IMG;
                Instantiate(thisItem, transform.position, transform.rotation);
                modules.Remove(modules[modules_IDs.IndexOf(module.ID)]);
                modules_IDs.Remove(modules_IDs[modules_IDs.IndexOf(module.ID)]);
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
}
