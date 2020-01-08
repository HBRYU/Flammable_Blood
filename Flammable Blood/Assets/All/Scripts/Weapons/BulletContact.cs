using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContact : MonoBehaviour
{
    public List<AudioClip> SFXs;
    public List<string> SFXs_names;

    public AudioClip SFX;

    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(SFX);
    }
}
