using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public float delay;
    public float intervalDelay;
    public int typeLetterCount;
    [TextArea(3, 5)]
    public string text;
    public int state;
    public bool pressKey;
    public bool clear;
    public bool lockPlayerPosition;
    public bool unlockPlayerPosition;
    public bool facePlayer;
    public bool faceRight;
    public bool faceLeft;
    public List<GameObject> triggerNPCs;
    public List<GameObject> unpauseNPCs;
    public bool pause;
}


public class NPC : MonoBehaviour
{
    public string name;
    public List<string> states;
    public AudioClip dialogueSFX;
    public Color dialugueColor;
}
