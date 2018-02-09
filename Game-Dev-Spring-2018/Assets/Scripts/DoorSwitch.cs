using UnityEngine;
using System.Collections;

public class DoorSwitch : MonoBehaviour
{
    public GameObject door;
    public bool openDoor;

    private void Start()
    {
        openDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector2 doorPosition = door.transform.position;
                door.transform.position = Vector2.Lerp(doorPosition, doorPosition + new Vector2(0, 5), 3.0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            openDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            openDoor = false;
        }
    }
}
