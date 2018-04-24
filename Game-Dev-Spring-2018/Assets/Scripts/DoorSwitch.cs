using UnityEngine;
using System.Collections;

public class DoorSwitch : MonoBehaviour
{
    [Header("Door Switch's Properties")]
    [SerializeField] private GameObject door;
    [SerializeField] private Color colorToChange;
    [SerializeField] private bool moveDown;

    private bool openDoor;

    private void Start()
    {
        openDoor = false;
    }

    //Move door up/down depends on the direction specified
    private void Update()
    {
        if (openDoor)
        {
            gameObject.GetComponent<SpriteRenderer>().color = colorToChange;
            Vector2 doorPosition = door.transform.position;

            if (moveDown)
            {
                LeanTween.move(door, doorPosition + new Vector2(0, -door.transform.localScale.x), 3.0f);
            }
            else
            {
                LeanTween.move(door, doorPosition + new Vector2(0, door.transform.localScale.x), 3.0f);
            }

            openDoor = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            openDoor = true;
        }
    }
}
