using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Dead : MonoBehaviour
{
    public GameObject deathMenu;
    public GameObject darken;

    public GameObject[] buttons;
    public TMP_InputField PW;
    public TMP_InputField INFO;

    private void Awake()
    {
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
    }

    public void Dead()
    {
        darken.SetActive(true);
        deathMenu.SetActive(true);
    }

    public void PWEntered()
    {
        Debug.Log(PW.text + "/" + GM.GetGM().ConfirmPW);
        if((PW.text == GM.GetGM().ConfirmPW))
        {
            //Debug.Log("FUCK");
            GM.DisplayText("Saving scores. . .", true);
            GM.GetGM().data.WriteData("[" + GM.GetGM().gameObject.GetComponent<LevelManager>().level + "/"+ INFO.text + "/" + GM.GetGM().clock.clockText_score.text + "/" + GM.GetGM().clock.killsText_score.text + "]");
            GM.GetUI().GetComponent<UI_Scores>().Refresh();
            buttons[0].SetActive(true);
            buttons[1].SetActive(true);
        }
    }
}
