﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    public GM _GM_;
    public GameObject menuPanel;
    public GameObject darken;

    void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            OnMenuButtonPress();
    }


    public void OnMenuButtonPress()
    {
        if (!menuPanel.activeInHierarchy)
        {
            menuPanel.SetActive(true);
            darken.SetActive(true);
            _GM_.shooting_active = false;
            Time.timeScale = 0.0f;
        }
        else
        {
            menuPanel.SetActive(false);
            darken.SetActive(false);
            _GM_.shooting_active = true;
            Time.timeScale = 1.0f;
        }
    }
}