using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public Transform camPos;

    public float shake_intensity;
    public float shake_duration;
    private float shake_duration_timer;

    Vector2 initPos;

    void Start()
    {
        initPos = camPos.localPosition;
    }

    void FixedUpdate()
    {
        //Debug.Log("SHaKinG Rn");

        Vector2 movePos = camPos.localPosition;

        if(shake_duration_timer > 0)
        {  
            float shakeForce = shake_duration_timer / shake_duration;
            movePos.x = initPos.x + Random.Range(-shake_intensity * shakeForce, shake_intensity * shakeForce);
            movePos.y = initPos.y + Random.Range(-shake_intensity * shakeForce, shake_intensity * shakeForce);

            camPos.localPosition = movePos;

            shake_duration_timer -= 1;
        }
        else
        {
            shake_duration = 0;
            shake_duration_timer = 0;
            shake_intensity = 0;
            camPos.localPosition = initPos;
        }

        
        
    }

    public void CameraShake(float intensity, float duration, bool multiply)
    {
        if(multiply)
            shake_intensity += intensity;
        else
        {
            if(shake_intensity < intensity)
            {
                shake_intensity = intensity;
            }
        }
            

        if(duration > shake_duration_timer)
        {
            shake_duration = duration;
            shake_duration_timer = duration;
        }
    }
}
