using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockStart_CollisionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GM.GetGM().clock.Begin();
            Destroy(gameObject);
        }
    }
}
