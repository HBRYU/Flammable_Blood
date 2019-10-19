using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Module
{
    public string ID;
    public int level;
    public Sprite IMG;
    [TextArea]
    public string description;
}
