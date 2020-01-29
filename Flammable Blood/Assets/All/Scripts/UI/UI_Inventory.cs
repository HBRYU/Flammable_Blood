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

    public GameObject statsPanel;

    public TextMeshProUGUI stats_itemName;
    public TextMeshProUGUI stats_description;
    public Image stats_IMG;

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
        resetStatsPanel();
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
        resetStatsPanel();
        if (inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
            selectPanel.SetActive(false);
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_Inventory")] = true;
            player.GetComponent<PlayerMove>().faceMouseMovement = true;
            player.GetComponent<PlayerMove>().move = true;
            player.GetComponent<Gun_Rotation>().disableGunRot = false;
            player.GetComponent<PlayerAnimControl>().disableAnimation = false;
        }
        else
        {
            inventoryPanel.SetActive(true);
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_Inventory")] = false;
            player.GetComponent<PlayerMove>().faceMouseMovement = false;
            player.GetComponent<PlayerMove>().move = false;
            player.GetComponent<Gun_Rotation>().disableGunRot = true;
            player.GetComponent<PlayerAnimControl>().disableAnimation = true;
        }
    }

    void RefreshWeaponSlots()
    {
        //resetStatsPanel();
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
        //resetStatsPanel();
        for (int i = 0; i < deployableSlots.Count; i++)
        {
            try
            {
                if(dm.dplybles_count[i] > 0)
                {
                    deployableSlots[i].text = " " + dm.dplybles_name[i] + " x (" + dm.dplybles_count[i] + ")";
                    deployableSlots[i].transform.parent.GetComponent<Button>().enabled = true;
                }
                else
                {
                    deployableSlots[i].text = " -";
                    deployableSlots[i].transform.parent.GetComponent<Button>().enabled = false;
                }
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

            //statsPanel.SetActive(true);
            stats_itemName.text = pw.weapons[index].GetComponent<WeaponStats>().name;
            stats_IMG.sprite = pw.weapons[index].GetComponent<WeaponStats>().UI_IMG;
            stats_IMG.color = pw.weapons[index].GetComponent<WeaponStats>().UI_IMG_color;
            //Debug.Log(pw.weapons[index].GetComponent<WeaponStats>().UI_IMG_color + "/" + stats_IMG.color);
            stats_description.text = pw.weapons[index].GetComponent<WeaponStats>().description;
            stats_description.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            stats_description.GetComponent<RectTransform>().offsetMin = new Vector2(0, pw.weapons[index].GetComponent<WeaponStats>().description_scroll_bottom);
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

            statsPanel.SetActive(true);
            stats_itemName.text = dm.deployables[index].GetComponent<Deployable>().ID;
            stats_IMG.sprite = dm.deployables[index].GetComponent<Deployable>().UI_IMG;
            //stats_IMG.color = dm.deployables[index].GetComponent<Deployable>().UI_IMG_color;
            stats_IMG.color = new Color(1, 1, 1, 1);
            //Debug.Log(dm.deployables[index].GetComponent<Deployable>().UI_IMG_color + "/" + stats_IMG.color);
            stats_description.text = dm.deployables[index].GetComponent<Deployable>().description;
            stats_description.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            stats_description.GetComponent<RectTransform>().offsetMin = new Vector2(0, dm.deployables[index].GetComponent<Deployable>().description_scroll_bottom);
        }
        else
        {
            selectPanel.SetActive(false);
            selectedDeployableIndex = -1;
        }
    }

    public void resetStatsPanel()
    {
        stats_itemName.text = "-";
        stats_description.text = "-";
        stats_description.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        stats_description.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        stats_IMG.sprite = null;
        stats_IMG.color = new Color(1, 1, 1, 0);
    }

}
