using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public bool startMoving;
    public float speed;
    public bool moveUp;

    // Use this for initialization
    void Start()
    {
        startMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            if (transform.position.y <= -3.14f)
            {
                moveUp = true;
            }
            if (transform.position.y >= 25.0f)
            {
                moveUp = false;
            }

            if (moveUp)
            {
                transform.Translate(new Vector2(0, speed));
            }
            else
            {
                transform.Translate(new Vector2(0, -speed));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            startMoving = true;
        }
    }
}
