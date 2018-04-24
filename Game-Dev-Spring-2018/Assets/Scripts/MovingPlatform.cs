using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    private ContactColor cc;

    [Header("Moving Platform's Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float moveDistance;
    [SerializeField] private float waitTime;

    private bool moveUp = true;
    private bool startMoving = false;
    private bool pause = false;
    private float baseY;

    // Use this for initialization
    private void Start()
    {
        baseY = transform.position.y;
        cc = GetComponent<ContactColor>();
    }

    // Move the platform up and down and change direction whenever hitting the highest/lowest point
    private void Update()
    {
        if (startMoving)
        {
            if (!pause)
            {
                if (moveUp)
                {
                    transform.Translate(new Vector2(0, speed));
                }
                else
                {
                    transform.Translate(new Vector2(0, -speed));
                }
            }
            if (transform.position.y < baseY && pause == false)
            {
                FixPositionAndChangeDirection(baseY);
            }
            if (transform.position.y > baseY + moveDistance && pause == false)
            {
                FixPositionAndChangeDirection(baseY + moveDistance);
            }
        }
    }

    //Clamp the platform to the right position after hitting the highest/lowest point then change direciton
    private void FixPositionAndChangeDirection(float positionY)
    {
        transform.position = new Vector2(transform.position.x, positionY);
        pause = true;
        StartCoroutine(ChangeDirection());
    }

    //Pause for a certain amount of time before changing direction
    private IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(waitTime);
        moveUp = moveUp ? false : true;
        pause = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cc.BeginContact(gameObject.transform.position);
            startMoving = true;
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}