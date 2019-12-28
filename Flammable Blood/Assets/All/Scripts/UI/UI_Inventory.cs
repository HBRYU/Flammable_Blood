using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private GM _GM_;
    [HideInInspector]
    public GameObject player;

    public GameObject inventoryPanel;

    [HideInInspector]
    public PlayerWeaponManager pw;
    [HideInInspector]
    public DeployablesManager dm;
    public List<TextMeshProUGUI> weaponSlots;
    public List<TextMeshProUGUI> ammoSlots;
    public List<TextMeshProUGUI> deployableSlots;

    public GameObject selectPanel;

    [HideInInspector]
    public int selectedWeaponIndex = -1, selectedAmmoIndex = -1, selectedDeployableIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        _GM_.AddShootingActiveSwitch("UI_Inventory");
        player = _GM_.player;
        pw = player.GetComponent<PlayerWeaponManager>();
        dm = player.GetComponent<DeployablesManager>();
        selectPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RefreshWeaponSlots();
        RefreshAmmoSlots();
        RefreshDeployableSlots();

        if (Input.GetKeyDown("y") && _GM_.playerAlive && !_GM_.paused)
        {
            OpenClose();
        }

        if (!_GM_.playerAlive)
        {
            inventoryPanel.SetActive(false);
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_Inventory")] = false;
        }
    }

    public void OpenClose()
    {
        if (inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_Inventory")] = true;
        }
        else
        {
            inventoryPanel.SetActive(true);
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_Inventory")] = false;
        }
    }

    void RefreshWeaponSlots()
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            try
            {
                weaponSlots[i].transform.parent.GetComponent<Button>().enabled = true;
                weaponSlots[i].text = " " + pw.weapons[i].GetComponent<WeaponStats>().name + " [" + pw.weapons[i].GetComponent<WeaponStats>().ammoType + "]";
            }
            catch
            {
                weaponSlots[i].text = " -";
                weaponSlots[i].transform.parent.GetComponent<Button>().enabled = false;
            }
            
        }
    }

    void RefreshAmmoSlots()
    {
        for (int i = 0; i < ammoSlots.Count; i++)
        {
            try
            {
                if(pw.ammo_count[i] > 0)
                {
                    ammoSlots[i].transform.parent.GetComponent<Button>().enabled = true;
                    ammoSlots[i].text = " " + pw.ammo_type[i] + " x (" + pw.ammo_count[i] + ")";
                }
                else
                {
                    ammoSlots[i].text = " -";
                    ammoSlots[i].transform.parent.GetComponent<Button>().enabled = false;
                }
            }
            catch
            {
                ammoSlots[i].text = " -";
                ammoSlots[i].transform.parent.GetComponent<Button>().enabled = false;
            }
        }
    }

    void RefreshDeployableSlots()
    {
        for (int i = 0; i < deployableSlots.Count; i++)
        {
            try
            {
                deployableSlots[i].text = " " + dm.dplybles_name[i] + " x (" + dm.dplybles_count[i] + ")";
                deployableSlots[i].transform.parent.GetComponent<Button>().enabled = true;
            }
            catch
            {
                deployableSlots[i].text = " -";
                deployableSlots[i].transform.parent.GetComponent<Button>().enabled = false;
            }

        }
    }

    public void SelectSlotWeapon(int index)
    {
        if (selectedWeaponIndex != index)
        {
            selectedWeaponIndex = index;
            selectPanel.SetActive(true);
            selectPanel.transform.position = Input.mousePosition;
            selectPanel.GetComponent<UI_Inventory_SelectPanel>().index = index;
            selectPanel.GetComponent<UI_Inventory_SelectPanel>().type = "Weapon";
        }
        else
        {
            selectPanel.SetActive(false);
            selectedWeaponIndex = -1;
        }
        
    }
    public void SelectSlotAmmo(int index)
    {
        if (selectedAmmoIndex != index)
        {
            selectedAmmoIndex = index;
            selectPanel.SetActive(true);
            selectPanel.transform.position = Input.mousePosition;
            selectPanel.GetComponent<UI_Inventory_SelectPanel>().index = index;
            selectPanel.GetComponent<UI_Inventory_SelectPanel>().type = "Ammo";
        }
        else
        {
            selectPanel.SetActive(false);
            selectedAmmoIndex = -1;
        }
    }
    public void SelectSlotDeployable(int index)
    {
        if (selectedDeployableIndex != index)
        {
            selectedDeployableIndex = index;
            selectPanel.SetActive(true);
            selectPanel.transform.position = Input.mousePosition;
            selectPanel.GetComponent<UI_Inventory_SelectPanel>().index = index;
            selectPanel.GetComponent<UI_Inventory_SelectPanel>().type = "Deployable";
        }
        else
        {
            selectPanel.SetActive(false);
            selectedDeployableIndex = -1;
        }
    }

}
