using UnityEngine;
using System.Collections;

public class DoorSwitch2 : MonoBehaviour
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
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
            Vector2 doorPosition = door.transform.position;
            door.transform.position = Vector2.Lerp(doorPosition, doorPosition - new Vector2(0, 5), 3.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            openDoor = true;
        }
    }
}
