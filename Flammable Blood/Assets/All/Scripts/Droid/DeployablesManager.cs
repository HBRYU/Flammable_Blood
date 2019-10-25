using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeployablesManager : MonoBehaviour
{
    public bool ACTIVE;

    public Transform spawnPos;

    public List<GameObject> deployables;
    public List<int> dplybles_count;
    public List<string> dplybles_name;
    public List<Sprite> dplybles_IMG;
    public GameObject activeDPLYBL;
    public int activeDPLYBL_count;
    public string activeDPLYBL_name;
    public Sprite activeDPLYBL_IMG;

    // Start is called before the first frame update
    void Start()
    {
        activeDPLYBL = deployables[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (ACTIVE)
            Work();

    }

    void Work()
    {
        activeDPLYBL_count = dplybles_count[deployables.IndexOf(activeDPLYBL)];
        activeDPLYBL_name = dplybles_name[deployables.IndexOf(activeDPLYBL)];
        activeDPLYBL_IMG = dplybles_IMG[deployables.IndexOf(activeDPLYBL)];

        if (Input.GetKeyDown("t"))      //SWITCH DEPLOYABLES WITH 'T'
        {
            if (activeDPLYBL != deployables[deployables.Count - 1])
            {
                int a = 1;  //FAIL SWITCH
                while (true)
                {
                    if ((deployables.IndexOf(activeDPLYBL) + 1) > deployables.Count - 1)
                        break;
                    a += 1;
                    int count = 0;
                    foreach (int i in dplybles_count)
                        count += i;
                    if (count == 0)
                        break;
                    //Debug.Log("IN LOOP: " + (deployables.IndexOf(activeDPLYBL) + 1));
                    //Debug.Log("IN: " + a);
                    activeDPLYBL = deployables[deployables.IndexOf(activeDPLYBL) + 1];
                    if (dplybles_count[deployables.IndexOf(activeDPLYBL)] > 0)
                        break;

                }

                if (a > 100)
                    Debug.Log("Grenades count error: Check script and fix the retarded algorythm");

            }
            else
            {
                activeDPLYBL = deployables[0];
                int a = 0;  //FAIL SWITCH
                while (a < deployables.Count)
                {
                    a += 1;
                    int count = 0;
                    foreach (int i in dplybles_count)
                        count += i;
                    if (count == 0)
                        break;

                    if (dplybles_count[deployables.IndexOf(activeDPLYBL)] > 0)
                        break;
                    else
                    {
                        //Debug.Log("OUT OF LOOP: " + (deployables.IndexOf(activeDPLYBL) + 1));
                        //Debug.Log("OUT: " + a);
                        activeDPLYBL = deployables[deployables.IndexOf(activeDPLYBL) + 1];
                    }
                }
                if (a > 100)
                    Debug.Log("Grenades count error: Check script and fix the retarded algorythm");

            }
        }
        ///////////////////

        if (Input.GetMouseButtonDown(2))
        {
            if (dplybles_count[deployables.IndexOf(activeDPLYBL)] > 0)
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
                dplybles_count[deployables.IndexOf(activeDPLYBL)] -= 1;
            }
            else
            {
                Debug.Log("Grenades slot empty");
            }
        }
    }
}
