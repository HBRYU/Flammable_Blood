using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Weapon : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;

    public Image weapon_img;
    public TextMeshProUGUI weapon_name;
    public TextMeshProUGUI ammo_text;
    public Image BG;
    public Sprite defaultIMG;

    private PlayerWeaponManager WM;

    List<int> ammoCount;
    List<string> ammoType;
    private int availableAmmo;
    private int currentAmmo;
    private int currentAmmo_update;

    private GameObject activeWeapon;
    private GameObject lastActiveWeapon;

    private int alertValue;

    public Color defaultColor;
    public Color alertColor;
    public Color emptyColor;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        WM = player.GetComponent<PlayerWeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        activeWeapon = WM.activeWeapon;
        ammoCount = player.GetComponent<PlayerWeaponManager>().ammo_count;
        ammoType = player.GetComponent<PlayerWeaponManager>().ammo_type;

        if(activeWeapon != null)
        {
            weapon_img.enabled = true;
            BG.enabled = true;

            //////////////      AMMO
            if (ammoCount[ammoType.IndexOf(WM.AW_ammoType)] < WM.AW_WS.ammoCount)
            {
                currentAmmo = ammoCount[ammoType.IndexOf(WM.AW_ammoType)];
            }
            else
            {
                currentAmmo = WM.AW_WS.ammoCount;
            }

            //////////////      WEAPON
            availableAmmo = ammoCount[ammoType.IndexOf(WM.AW_ammoType)] - WM.AW_WS.magSize;
            if (activeWeapon != lastActiveWeapon || WM.AW_WS.is_reloading == true || ammoCount[ammoType.IndexOf(WM.AW_ammoType)] == 0)
            {
                if (availableAmmo < 0)
                    availableAmmo = 0;
            }
            lastActiveWeapon = activeWeapon;

            

            ///////////////     COLOR
            alertValue = WM.AW_WS.alertAmmoCount;

            if (ammoCount[ammoType.IndexOf(WM.AW_ammoType)] <= 0)
            {
                ammo_text.color = emptyColor;
            }
            else if(ammoCount[ammoType.IndexOf(WM.AW_ammoType)] <= alertValue)
            {
                ammo_text.color = alertColor;
            }
            else
            {
                ammo_text.color = defaultColor;
            }

            //////////////      TEXT
            string displayText;

            if (availableAmmo < 0)
                availableAmmo = 0;

            if(WM.AW_WS.is_reloading == false)
            {
                displayText = currentAmmo + " / " + availableAmmo;
            }
            else
            {
                displayText =  "- / " + availableAmmo;
            }
            ammo_text.text = displayText;
            weapon_name.text = WM.AW_WS.name;

            //////////////      IMAGE
            if (WM.AW_WS.IMG != null)
                weapon_img.sprite = WM.AW_WS.IMG;
            else
                weapon_img.sprite = defaultIMG;
        }
        else
        {
            weapon_name.text = string.Empty;
            ammo_text.text = string.Empty;
            weapon_img.enabled = false;
            BG.enabled = false;
        }
        
    }
}
