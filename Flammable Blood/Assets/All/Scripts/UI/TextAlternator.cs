using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAlternator : MonoBehaviour
{
    public List<string> texts;
    public List<string> texts_variants;
    public float interval;
    private float interval_timer;
    private bool flag;
    public int index;

    private void Start()
    {
        index = Random.Range(0, texts.Count - 1);
    }

    void Update()
    {
        interval_timer += Time.deltaTime;
        if(interval_timer >= interval)
        {
            if (flag)
                GetComponent<TextMeshProUGUI>().text = texts[index];
            else
                GetComponent<TextMeshProUGUI>().text = texts_variants[index];
            interval_timer = 0;
            flag = !flag;
        }
    }
}
