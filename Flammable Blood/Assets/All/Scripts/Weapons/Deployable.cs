using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deployable : MonoBehaviour
{
    public string ID;
    public Sprite UI_IMG;
    [TextArea(5,10)]
    public string description;
    public int description_scroll_bottom;
    /*
    public GameObject deployable;

    public void trigger()
    {
        Instantiate(deployable, transform.position, Quaternion.identity);
        Destroy(gameObject)
    }
    */
}
