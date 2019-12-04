using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dead : MonoBehaviour
{
    public GameObject deathMenu;
    public GameObject darken;

    public void Dead()
    {
        darken.SetActive(true);
        deathMenu.SetActive(true);
    }
}
