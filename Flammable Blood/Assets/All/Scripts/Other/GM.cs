using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public GameObject player;
    public CameraShakeManager camShakeManager;

    public bool playerAlive;
    public bool shooting_active;

    public List<string> chunkParticles_names;
    public List<GameObject> chunkParticles;

    // Start is called before the first frame update
    void Awake()
    {
        camShakeManager = GetComponent<CameraShakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }
}
