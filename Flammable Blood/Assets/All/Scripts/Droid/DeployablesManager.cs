using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployablesManager : MonoBehaviour
{
    public Transform spawnPos;

    public List<GameObject> deployables;
    public GameObject activeDPLYBL;

    // Start is called before the first frame update
    void Start()
    {
        activeDPLYBL = deployables[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))      //SWITCH DEPLOYABLES WITH 'T'
        {
            if(activeDPLYBL != deployables[deployables.Count - 1])
            {
                activeDPLYBL = deployables[deployables.IndexOf(activeDPLYBL) + 1];
            }
            else
            {
                activeDPLYBL = deployables[0];
            }
        }
        ///////////////////

        if (Input.GetMouseButtonDown(2))
        {
            Quaternion spawnRot = Quaternion.identity;

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(spawnPos.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            spawnRot = Quaternion.Euler(new Vector3(0, 0, angle));

            Instantiate(activeDPLYBL, spawnPos.position, spawnRot);
        }

    }
}
