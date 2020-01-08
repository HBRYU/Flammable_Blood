using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Fuel : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;

    private float fuel;
    private float maxFuel;

    public TextMeshProUGUI UI_fuel;
    public GameObject noFuelText;
    public Image image;
    public Image bg;

    public float alertFuel;
    public float criticalFuel;

    public Color defaultColor;
    public Color alertColor;
    public Color criticalColor;
    public Color noneColor;
    public Color BG_defaultColor;
    public Color BG_usingColor;

    float lastFuel;

    // Start is called before the first frame update
    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;

        fuel = player.GetComponent<PlayerMove>().fuel;
        maxFuel = player.GetComponent<PlayerMove>().maxFuel;

        lastFuel = fuel;
    }

    // Update is called once per frame
    void Update()
    {
        fuel = player.GetComponent<PlayerMove>().fuel;
        maxFuel = player.GetComponent<PlayerMove>().maxFuel;

        /*
        if (fuel < lastFuel)
        {
            bg.color = BG_usingColor;
        }
        else
        {
            bg.color = BG_defaultColor;
        }
        lastFuel = fuel;
        */


        if (fuel < 0)
            fuel = 0;

        string displayText = Mathf.Ceil(fuel) + "_ml";

        if (fuel > 0)
        {
            UI_fuel.text = displayText;
            noFuelText.SetActive(false);
        }
        else
        {
            if(!noFuelText.activeInHierarchy)
                GM.DisplayText("Fuel Emptied", false);
            UI_fuel.text = "_EMPTY_";
            noFuelText.SetActive(true);
        }

        image.fillAmount = fuel / maxFuel;

        if (fuel <= 0)
        {
            if (image.color != criticalColor)
                GM.DisplayText3("WARNING: Fuel emptied", true, 1);
            image.color = noneColor;
        }
        else if (fuel <= criticalFuel)
        {
            if (image.color != criticalColor)
                GM.DisplayText3("WARNING: Fuel critically low", true, 1);
            image.color = criticalColor;
        }
        else if (fuel <= alertFuel)
        {
            if (image.color != alertColor)
                GM.DisplayText3("WARNING: Low fuel", false, 0);
            image.color = alertColor;
        }
        else
        {
            image.color = defaultColor;
        }

    }
}
