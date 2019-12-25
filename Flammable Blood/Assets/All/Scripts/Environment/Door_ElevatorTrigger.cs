using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_ElevatorTrigger : MonoBehaviour
{
    public Elevator elevator;
    public int point;

    void Update()
    {
        if(point == elevator.targetPosition)
        {
            if (!transform.parent.GetComponent<Door>().opened)
                transform.parent.GetComponent<Door>().Open();
        }
        else
        {
            if(transform.parent.GetComponent<Door>().opened)
                transform.parent.GetComponent<Door>().Close();
        }
    }
}
