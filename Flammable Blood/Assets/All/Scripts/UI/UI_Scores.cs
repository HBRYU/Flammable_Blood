using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Scores : MonoBehaviour
{
    public GameObject scorePanel;
    public TextMeshProUGUI scoresTxt;

    // Start is called before the first frame update
    void Start()
    {
        scorePanel.SetActive(false);
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        Refresh();
        scorePanel.SetActive(true);
    }
    public void Hide()
    {
        scorePanel.SetActive(false);
    }
    public void Refresh()
    {
        scoresTxt.text = GM.GetGM().data.ReadData();
    }
}
