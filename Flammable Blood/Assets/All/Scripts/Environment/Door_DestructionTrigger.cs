using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_DestructionTrigger : MonoBehaviour
{
    public GameObject[] objs;
    public GameObject[] intact_objs;

    void Start()
    {
        
    }

    void Update()
    {
        intact_objs = objs;
        bool closed = false;
        foreach(GameObject obj in intact_objs)
        {
            try
            {
                if (obj.GetComponent<Transform>() != null)
                    closed = true;
            }
            catch
            {
            }
        }
        if (!closed)
        {
            GetComponentInParent<Door>().Open();
        }
    }
}
