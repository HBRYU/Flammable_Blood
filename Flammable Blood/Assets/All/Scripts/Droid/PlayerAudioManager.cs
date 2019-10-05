using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioManager : MonoBehaviour
{
    private AudioSource player_audio;

    [Header("Walking and running")]
    public AudioClip[] footsteps;

    [Header("Jumping and flying")]
    public AudioClip jump;
    public AudioClip land;
    public AudioClip jetpack_end;
    public AudioSource player_audio_jetpack;

    // Start is called before the first frame update
    void Start()
    {
        player_audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Step_SFX()
    {
        player_audio.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);
    }

    public void Jump_SFX()
    {
        player_audio.PlayOneShot(jump);
    }

    public void Land_SFX()
    {
        player_audio.PlayOneShot(land);
    }

    public void Jetpack_SFX(bool play)
    {
        if (play)
        {
            Debug.Log("PlayingJP SFX");
            player_audio_jetpack.Play();
        }
        else
        {
            player_audio.PlayOneShot(jetpack_end);
            player_audio_jetpack.Stop();
        } 
    }
}
