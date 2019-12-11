using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_KeyTrigger : MonoBehaviour
{
    private GameObject player;
    public float triggerDistance;

    public GameObject keyR;
    public GameObject keyL;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 diff = new Vector2((player.transform.position.x - transform.position.x), (player.transform.position.y - transform.position.y));
        if(diff.x*diff.x + diff.y*diff.y <= triggerDistance * triggerDistance)
        {
            if(player.transform.position.x > transform.position.x)
            {
                keyR.GetComponent<SpriteRenderer>().enabled = true;
                keyL.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                keyR.GetComponent<SpriteRenderer>().enabled = false;
                keyL.GetComponent<SpriteRenderer>().enabled = true;
            }

            if (Input.GetKeyDown("e"))
            {
                Door door = transform.parent.GetComponent<Door>();
                if (door.opened)
                    door.Close();
                else
                    door.Open();
            }
        }
        else
        {
            keyR.GetComponent<SpriteRenderer>().enabled = false;
            keyL.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
