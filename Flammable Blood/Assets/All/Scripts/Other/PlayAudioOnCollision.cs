using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{
    public AudioClip[] sfxs;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            GetComponent<AudioSource>().PlayOneShot(sfxs[Random.Range(0, sfxs.Length - 1)]);
    }
}
