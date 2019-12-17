﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GM : MonoBehaviour
{
    public GameObject player;
    public CameraShakeManager camShakeManager;

    public bool playerAlive;
    public bool shooting_active;
    public List<bool> shooting_active_switches;
    public List<string> shooting_active_keys;

    public List<string> chunkParticles_names;
    public List<GameObject> chunkParticles;

    // Start is called before the first frame update
    void Awake()
    {
        camShakeManager = GetComponent<CameraShakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool shooting_active_flag = true;
        for(int i = 0; i < shooting_active_keys.Count; i++)
        {
            if (shooting_active_switches[i] == false)
                shooting_active_flag = false;
        }
        if (!shooting_active_flag)
            shooting_active = false;
        else
            shooting_active = true;
    }

    public static GM GetGM()
    {
        GM _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        return (_GM_);
    }

    public static GameObject GetUI()
    {
        return (GameObject.FindGameObjectWithTag("UI"));
    }

    public void AddShootingActiveSwitch(string key)
    {
        shooting_active_keys.Add(key);
        shooting_active_switches.Add(true);
    }

    public static float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }

    public static int CompareDistance(Vector2 origin, Vector2 target, float sampleDistance)
    {
        Vector2 diff = new Vector2((target.x - origin.x), (target.y - origin.y));
        if (diff.x * diff.x + diff.y * diff.y > sampleDistance * sampleDistance)
        {
            return (1);
        }
        else if (diff.x * diff.x + diff.y * diff.y == sampleDistance * sampleDistance)
        {
            return (0);
        }
        else
        {
            return (-1);
        }
    }

    public static void DisplayText(string text, bool clear)
    {
        TextMeshProUGUI displayText = GM.GetUI().GetComponent<UI_HelpText>().helpText;

        if (clear)
            displayText.text = string.Empty;

        GM.GetUI().GetComponent<UI_HelpText>().DisplayText(text);
    }
}
