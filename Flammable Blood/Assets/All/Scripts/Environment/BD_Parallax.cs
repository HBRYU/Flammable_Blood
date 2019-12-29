using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BD_Parallax : MonoBehaviour
{
    public Transform[] backgrounds;
    public float[] parallaxScales;
    public bool enableColorVariation;
    public float colorVariationDelta;
    public float smoothing = 1.0f;

    private Transform cam;
    private Vector3 prev_camPos;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        prev_camPos = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
            if (enableColorVariation)
            {
                Color col = backgrounds[i].gameObject.GetComponent<SpriteRenderer>().color;
                //Debug.Log(col.r * colorVariationDelta * backgrounds[i].position.z);
                col.r *= colorVariationDelta * backgrounds[i].position.z;
                col.g *= colorVariationDelta * backgrounds[i].position.z;
                col.b *= colorVariationDelta * backgrounds[i].position.z;
                backgrounds[i].gameObject.GetComponent<SpriteRenderer>().color = col;
            }
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (prev_camPos.x - cam.position.x) * parallaxScales[i];

            float targetPosX = backgrounds[i].position.x + parallax;

            Vector3 targetPos = new Vector3(targetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPos, smoothing);
        }

        prev_camPos = cam.position;
    }
}
