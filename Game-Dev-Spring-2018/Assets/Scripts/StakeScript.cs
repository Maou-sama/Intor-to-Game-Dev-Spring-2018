using UnityEngine;
using System.Collections;

public class StakeScript : MonoBehaviour
{
    public float moveAmount;

    private Vector2 originalPosition;
    private Vector2 laterPosition;

    // Use this for initialization
    void Start()
    {
        originalPosition = transform.position;
        laterPosition = originalPosition + new Vector2(0, -1.3f);   
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= originalPosition.y)
        {
            LeanTween.move(gameObject, laterPosition, 2.0f);
        }

        else if(transform.position.y <= laterPosition.y)
        {
            LeanTween.move(gameObject, originalPosition, 2.0f);
        }
    }
}
