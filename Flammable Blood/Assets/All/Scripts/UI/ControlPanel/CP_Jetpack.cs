using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CP_Jetpack : MonoBehaviour
{
    private GM _GM_;
    private GameObject player;
    public Slider slider;
    public TMP_InputField inputField;
    public Toggle toggle;
    private PlayerMove pm;

    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        player = _GM_.player;
        pm = player.GetComponent<PlayerMove>();
    }

    public void UpdateToggle()
    {
        bool enable = toggle.isOn;
        if (enable)
            player.GetComponent<PlayerModuleManager>().UpdateModule(GetComponent<Stats_Module>().module, 2);
        else
        {
            player.GetComponent<PlayerModuleManager>().UpdateModule(GetComponent<Stats_Module>().module, 1);
        }
    }

    public void UpdateThrustSlider()
    {
        float val = slider.value;
        pm.jetpack_force = val;
        inputField.text = val.ToString();
    }

    public void UpdateThrustInput()
    {
        string val = inputField.text;
        float val2 = GM.GetFloat(val, 0);
        Debug.Log(val +"/"+ val2);
        pm.jetpack_force = val2;
        slider.value = val2;
    }


}
