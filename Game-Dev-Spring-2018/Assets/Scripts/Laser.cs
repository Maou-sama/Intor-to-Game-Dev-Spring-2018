using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public bool off;
    public float timeOnOff;
    // Use this for initialization

    
    void Start()
    {
        off = false;
        StartCoroutine(TurnOnOff());
    }

    // Update is called once per frame
    void Update()
    {
            if (off)
            {
                LaserState(false);
            }

            else
            {
                LaserState(true);
            }
    }

    void LaserState(bool state)
    {
        GetComponent<BoxCollider2D>().enabled = state;
        GetComponent<SpriteRenderer>().enabled = state;
    }

    IEnumerator TurnOnOff()
    {
        yield return new WaitForSeconds(timeOnOff);
        off = !off;
        StartCoroutine(TurnOnOff());
    }
}
