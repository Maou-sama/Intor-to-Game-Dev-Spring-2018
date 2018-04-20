using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    private ContactColor cc;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<ContactColor>();

    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cc.BeginContact(collision.gameObject.transform.position);
        }
    }
}
