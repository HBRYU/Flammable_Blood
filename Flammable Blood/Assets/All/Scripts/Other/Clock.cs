﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI clockText_score;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI killsText_score;
    public GameObject killsPanel;
    public int initTime;
    public float remainingTime;

    public string endDisplayText;

    public bool started;

    public bool paused;

    void Start()
    {
        remainingTime = initTime;
        killsPanel.SetActive(false);
    }

    void Update()
    {
        
        killsText_score.text = GM.GetGM().killCount.ToString();
        if (started)
        {
            if (GM.GetGM().playerAlive)
                clockText_score.text = (initTime - Mathf.RoundToInt(remainingTime)).ToString();
            if(!paused)
                remainingTime -= Time.deltaTime;
            clockText.text = Mathf.RoundToInt(remainingTime).ToString();
            clockText.color = new Color(1, 1, 1, 1);
            killsPanel.SetActive(true);
            killsText.text = GM.GetGM().killCount.ToString();
            if(remainingTime <= 0)
            {
                clockText.text = endDisplayText;
                GM.DisplayText("TIME UP", true);
                GM.GetGM().player.GetComponent<PlayerStats>().TakeDamage(999999999);
                started = false;
            }
        }
    }

    public void Begin()
    {
        started = true;
    }
}
