using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ControlPanel : MonoBehaviour
{
    /*
    public GameObject panel;
    public GameObject darken;

    public bool opened;

    public List<Module> modules;
    public List<GameObject> UI_modules;
    public List<GameObject> UI_modules_all; //INSTANTIATED VERSIONS
    public List<GameObject> slots_1;
    public List<GameObject> slots_2;
    public List<GameObject> slots_3;
    public List<GameObject> slots_4;
    public List<GameObject> slots_5;

    public int slotPerPage;
    public int totalPageCount;
    public int pagesFilled;
    public List<GameObject> pages;
    public List<List<GameObject>> book;

    void Start()
    {
        RefreshModulesList();
        book.Add(slots_1);
        book.Add(slots_2);
        book.Add(slots_3);
        book.Add(slots_4);
        book.Add(slots_5);
    }

    void Update()
    {
        if (opened)
        {
            Debug.Log("OPENDED");
        }
    }

    public void RefreshModulesList()
    {
        modules = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModuleManager>().modules;
        List<GameObject> a = new List<GameObject>();
        foreach(Module module in modules)
        {
            foreach(GameObject g in UI_modules_all)
            {
                //if(module.UI_GameObject.GetComponent<SpriteRenderer>().sprite == )

            }
            a.Add(module.UI_GameObject);
        }
        UI_modules = a;

        int b = 0;
        pagesFilled = 0;

        for (int i = 0; i < UI_modules.Count; i++)
        {
            b++;
            if (b >= slotPerPage)
            {
                pagesFilled++;
                b = 0;
            }
            foreach(Module module in modules)
            {

            }
            UI_modules_all.Add(Instantiate(UI_modules[i], book[pagesFilled][b].transform.position, Quaternion.identity));
        }
    }

    public void AddModule(Module module)
    {
        UI_modules.Add(module.UI_GameObject);
        

        int a = 0;
        pagesFilled = 0;

        for (int i = 0; i < UI_modules.Count; i++)
        {
            a++;
            if (a >= slotPerPage)
            {
                pagesFilled++;
                a = 0;
            }
        }

        int b = UI_modules.Count % slotPerPage - 1;

        UI_modules_all.Add(Instantiate(module.UI_GameObject, book[pagesFilled][b].transform.position, Quaternion.identity));
    }

    void OpenClose()
    {
        if(panel.activeInHierarchy == false)
        {
            panel.SetActive(true);
            darken.SetActive(true);
            opened = true;
        }
        else
        { 
            panel.SetActive(false);
            darken.SetActive(false);
            opened = false;
        }
    }
    */
}
