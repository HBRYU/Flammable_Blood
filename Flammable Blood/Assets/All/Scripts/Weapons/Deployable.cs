using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deployable : MonoBehaviour
{
    public string ID;
    public Sprite UI_IMG;
    public Color UI_IMG_color = new Color(1, 1, 1, 1);
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
