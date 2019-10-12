using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private GM _GM_;
    public float duration;
    public float lifeSpan;

    public float cameraShake_force;
    public float cameraShake_duration;

    public AudioClip[] SFXs;

    private void Start()
    {
        _GM_ = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        _GM_.camShakeManager.CameraShake(cameraShake_force, cameraShake_duration, true);

        GetComponent<AudioSource>().PlayOneShot(SFXs[Random.Range(0, SFXs.Length)]);
    }

    void FixedUpdate()
    {
        duration -= 1;
        lifeSpan -= 1;

        if(lifeSpan <= 0)
        {
            Destroy(gameObject);
        }

        if(duration <= 0)
        {
            GetComponent<PointEffector2D>().enabled = false;
        }
    }
}
