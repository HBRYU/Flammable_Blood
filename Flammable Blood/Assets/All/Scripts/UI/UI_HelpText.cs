using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_HelpText : MonoBehaviour
{
    public TextMeshProUGUI helpText;
    public float displayTime;
    private float displayTime_timer;

    void Update()
    {
        if (displayTime_timer > 0)
            displayTime_timer -= Time.deltaTime;
        else
        {
            displayTime_timer = 0;
            helpText.text = string.Empty;
        }
        helpText.color = new Color(helpText.color.r, helpText.color.g, helpText.color.b, displayTime_timer / displayTime);
    }

    public void DisplayText(string text)
    {
        helpText.text += "\n" + text;
        displayTime_timer = displayTime;
    }
}
