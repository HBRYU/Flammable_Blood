using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_HelpText_3 : MonoBehaviour
{
    public TextMeshProUGUI helpText;
    public float color_alpha;
    public Color[] colors;
    public float default_alpha;


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
                //helpText.text = string.Empty;
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
        Debug.Log("DA: " + default_alpha / 255);
        Debug.Log("OA: " + (fadeTime_timer / fadeTime * (color_alpha / 255)));
        Debug.Log("Alpha: " + ((fadeTime_timer / fadeTime) * (color_alpha / 255)) + (default_alpha / 255));
        helpText.color = new Color(helpText.color.r, helpText.color.g, helpText.color.b, ((fadeTime_timer / fadeTime) * (color_alpha/255)) + (default_alpha/255));
    }

    public void DisplayText(string text, int col)
    {
        //Debug.Log(text);
        helpText.text += "> " + text + "\n";
        helpText.color = colors[col];
        line++;
        displayTime_timer = displayTime;
        fadeTime_timer = fadeTime;
    }
}
