using System.Collections;
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
        _GM_.AddShootingActiveSwitch("UI_Menu");
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
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_Menu")] = false;
            Time.timeScale = 0.0f;

            //GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursory>().enabled = false;
            //GameObject.FindGameObjectWithTag("Cursor").GetComponent<SpriteRenderer>().enabled = false;
            //Cursor.visible = true;
        }
        else
        {
            menuPanel.SetActive(false);
            darken.SetActive(false);
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_Menu")] = true;
            Time.timeScale = 1.0f;

            //GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursory>().enabled = true;
            //GameObject.FindGameObjectWithTag("Cursor").GetComponent<SpriteRenderer>().enabled = true;
            //Cursor.visible = false;
        }
    }
}
