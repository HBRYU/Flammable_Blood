using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAlternator : MonoBehaviour
{
    public List<string> texts;
    public float interval;
    private float interval_timer;

    void Update()
    {
        interval_timer += Time.deltaTime;
        if(interval_timer >= interval)
        {
            if (texts.IndexOf(GetComponent<TextMeshProUGUI>().text) + 1 < texts.Count)
                GetComponent<TextMeshProUGUI>().text = texts[texts.IndexOf(GetComponent<TextMeshProUGUI>().text) + 1];
            else
                GetComponent<TextMeshProUGUI>().text = texts[0];
            interval_timer = 0;
        }
    }
}
