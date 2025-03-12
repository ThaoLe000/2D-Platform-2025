using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform newRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x)
            {
                newRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                newRoom.GetComponent<Room>().ActivateRoom(false);
                previousRoom.GetComponent<Room>().ActivateRoom(true);
            }
        }
    }
}
