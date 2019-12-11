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

    public static int CompareDistance(Vector2 origin, Vector2 target, float sampleDistance)
    {
        Vector2 diff = new Vector2((target.x - origin.x), (target.y - origin.y));
        if (diff.x * diff.x + diff.y * diff.y > sampleDistance * sampleDistance)
        {
            return (1);
        }
        else if (diff.x * diff.x + diff.y * diff.y == sampleDistance * sampleDistance)
        {
            return (0);
        }
        else
        {
            return (-1);
        }
    }
}
