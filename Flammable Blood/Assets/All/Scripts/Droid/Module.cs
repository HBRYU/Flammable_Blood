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

    public void Reset(Module module)
    {
        module.ID = string.Empty;
        module.level = 0;
        module.IMG = null;
        module.accessPanel_ID = string.Empty;
        module.description = string.Empty;
    }
}
