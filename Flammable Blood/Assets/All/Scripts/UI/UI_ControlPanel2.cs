using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UI_Module
{
    public string ID;
    public GameObject obj;
    public int page;
}

public class UI_ControlPanel2 : MonoBehaviour
{
    private GM _GM_;

    public GameObject panel;
    //public GameObject darken;

    public bool opened;

    public List<Module> modules;
    public List<UI_Module> UI_modules;

    void Start()
    {
        panel.SetActive(false);
        //darken.SetActive(false);
        opened = false;
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        _GM_.AddShootingActiveSwitch("UI_ControlPanel2");
        _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_ControlPanel2")] = true;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1) && _GM_.playerAlive && !_GM_.paused)
            OpenClose();

        if (!_GM_.playerAlive)
        {
            panel.SetActive(false);
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_ControlPanel2")] = false;
        }
    }

    public void AddModule(Module module)
    {
        foreach(UI_Module ui_module in UI_modules)
        {
            if(ui_module.ID == module.accessPanel_ID)
            {
                ui_module.obj.SetActive(true);
            }
        }
    }

    public void RemoveModule(Module module)
    {
        foreach (UI_Module ui_module in UI_modules)
        {
            if (ui_module.ID == module.accessPanel_ID)
            {
                ui_module.obj.SetActive(false);
            }
        }
    }

    public void OpenClose()
    {
        if (panel.activeInHierarchy == false)
        {
            panel.SetActive(true);
            //darken.SetActive(true);
            opened = true;
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_ControlPanel2")] = false;

            //GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursory>().enabled = false;
            //GameObject.FindGameObjectWithTag("Cursor").GetComponent<SpriteRenderer>().enabled = false;
            //Cursor.visible = true;
        }
        else
        {
            panel.SetActive(false);
            //darken.SetActive(false);
            opened = false;
            _GM_.shooting_active_switches[_GM_.shooting_active_keys.IndexOf("UI_ControlPanel2")] = true;

            //GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursory>().enabled = true;
            //GameObject.FindGameObjectWithTag("Cursor").GetComponent<SpriteRenderer>().enabled = true;
            //Cursor.visible = false;
        }
    }
}
