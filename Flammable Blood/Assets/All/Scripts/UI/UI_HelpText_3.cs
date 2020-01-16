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

    public AudioClip SFX;
    public AudioClip typeSFX;

    public AudioSource SFX_audio;
    public AudioSource typeSFX_audio;

    public string displayText;

    public float displayTime;
    private float displayTime_timer;
    public float fadeTime;
    public float fadeTime_timer;

    public float typeDelay;
    private float typeDelay_timer;

    //public int lineLimit;
    [HideInInspector]
    public int line;

    void Update()
    {
        if (displayText.Length > helpText.text.Length)
        {
            typeDelay_timer += Time.deltaTime;
            if (typeDelay_timer > +typeDelay)
            {
                helpText.text += displayText[helpText.text.Length];
                typeSFX_audio.PlayOneShot(typeSFX);
                typeDelay_timer = 0;
            }
        }
        else if (displayTime_timer > 0)
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
        
        

        helpText.color = new Color(helpText.color.r, helpText.color.g, helpText.color.b, ((fadeTime_timer / fadeTime) * (color_alpha/255)) + (default_alpha/255));
    }

    public void DisplayText(string text, int col)
    {
        //Debug.Log(text);
        displayText += "> " + text + "\n";
        helpText.color = colors[col];
        line++;
        displayTime_timer = displayTime;
        fadeTime_timer = fadeTime;
        SFX_audio.PlayOneShot(SFX);
    }
}
