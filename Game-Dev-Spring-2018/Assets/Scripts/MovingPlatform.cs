using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public bool startMoving;
    public bool pause;
    public float speed;
    public bool moveUp;
    public float moveDistance;
    public float waitTime;

    private float baseY;
    // Use this for initialization
    void Start()
    {
        startMoving = false;
        pause = false;
        baseY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
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

    void FixPositionAndChangeDirection(float positionY)
    {
        transform.position = new Vector2(transform.position.x, positionY);
        pause = true;
        StartCoroutine(ChangeDirection());
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(waitTime);
        moveUp = moveUp ? false : true;
        pause = false;
    }
}