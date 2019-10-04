using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public GameObject player;
    public CameraShakeManager camShakeManager;

    public bool playerAlive;

    public List<string> chunkParticles_names;
    public List<GameObject> chunkParticles;

    // Start is called before the first frame update
    void Start()
    {
        camShakeManager = GetComponent<CameraShakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
