using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Module
{
    public string ID;
    public int level;
    public Sprite IMG;
    public string accessPanel_ID;
    [TextArea]
    public string description;
}
