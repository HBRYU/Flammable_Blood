using System.Collections;
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
    public float timeElapsed;

    public string endDisplayText;

    public bool useClock;
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
                clockText_score.text = (timeElapsed).ToString();
            if (!paused)
            {
                if(useClock)
                    remainingTime -= Time.deltaTime;
                timeElapsed += Time.deltaTime;
            }
            if(useClock)
                clockText.text = Mathf.RoundToInt(remainingTime).ToString();
            else
                clockText.text = Mathf.RoundToInt(timeElapsed).ToString();
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
