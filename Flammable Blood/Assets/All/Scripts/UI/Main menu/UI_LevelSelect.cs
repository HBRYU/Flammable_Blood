using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelSelect : MonoBehaviour
{
    public bool is_open;

    public void OnStartButton()
    {
        if (!is_open)
        {
            is_open = true;
            GetComponent<Animator>().SetBool("IsOpen", true);
        }
        else
        {
            is_open = false;
            GetComponent<Animator>().SetBool("IsOpen", false);
        }
    }
}
