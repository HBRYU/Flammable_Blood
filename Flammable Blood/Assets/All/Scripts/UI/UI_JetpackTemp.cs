using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_JetpackTemp : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;
    private PlayerMove pm;

    private float heat;
    private float maxHeat;

    public TextMeshProUGUI UI_heat;
    public GameObject overheatedText;
    public Image image;

    public float alertHeat;
    public float criticalHeat;

    public Color defaultColor;
    public Color alertColor;
    public Color criticalColor;
    public Color overheatColor;

    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        pm = player.GetComponent<PlayerMove>();
    }

    void Update()
    {
        heat = pm.jetpack_heat;
        maxHeat = pm.jetpack_overheat;

        if (!pm.jetpack_overheated)
        {
            UI_heat.text = Mathf.RoundToInt(heat).ToString();
            image.fillAmount = heat / maxHeat;
            overheatedText.SetActive(false);
        }
        else
        {
            image.fillAmount = pm.jetpack_cooldown_timer / pm.jetpack_cooldown;
            UI_heat.text = "";
            overheatedText.SetActive(true);
        }

        if (pm.jetpack_overheated)
            image.color = overheatColor;
        else if (heat >= criticalHeat)
            image.color = criticalColor;
        else if (heat >= alertHeat)
            image.color = alertColor;
        else
            image.color = defaultColor;

    }
}
