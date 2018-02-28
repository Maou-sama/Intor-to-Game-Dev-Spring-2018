using UnityEngine;
using System.Collections;

public class SideScrollingCamera: MonoBehaviour
{

    public GameObject player;
    public Vector3 originalTransform;
    public Vector3 zoomTransform;
    public bool zoomOut;

    private void Start()
    {
        originalTransform = transform.position;
        zoomTransform = new Vector3(originalTransform.x, originalTransform.y, originalTransform.z * 2);
        zoomOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position = zoomTransform;
            zoomOut = true;
        }

        else
        {
            transform.position = originalTransform;
            zoomOut = false;
        }
        
        if (player.transform.position.y > -2.6f)
        {
            if (transform.position.y < -2.1f)
            {
                transform.position = new Vector3(player.transform.position.x, -2.1f, transform.position.z);
            }

            else
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, transform.position.z);
            }
        }
    }
}
