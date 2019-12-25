using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_HelpText_2 : MonoBehaviour
{
    public TextMeshProUGUI helpText;
    public Image BG;
    public Color BG_InitColor;
    public float displayTime;
    private float displayTime_timer;
    public float fadeTime;
    public float fadeTime_timer;
    public int lineLimit;
    [HideInInspector]
    public int line;

    void Update()
    {
        if (displayTime_timer > 0)
            displayTime_timer -= Time.deltaTime;
        else
        {
            if (fadeTime_timer > 0)
                fadeTime_timer -= Time.deltaTime;
            else
            {
                helpText.text = string.Empty;
                fadeTime_timer = 0;
                line = 0;
            }
        }
        if (line > lineLimit)
        {
            //Debug.Log("OVER");
            //Debug.Log("INDEX:" + helpText.text.IndexOf("\n"));
            string txt = helpText.text.Remove(0, helpText.text.IndexOf("\n") + 1);
            helpText.text = txt;
            line--;
        }
        //Debug.Log("LINE:" + line);
        helpText.color = new Color(helpText.color.r, helpText.color.g, helpText.color.b, fadeTime_timer / fadeTime);
        BG.color = new Color(BG.color.r, BG.color.g, BG.color.b, (fadeTime_timer / fadeTime) * BG_InitColor.a);
    }

    public void DisplayText(string text)
    {
        //Debug.Log(text);
        helpText.text += text + "\n";
        line++;
        displayTime_timer = displayTime;
    }
}
