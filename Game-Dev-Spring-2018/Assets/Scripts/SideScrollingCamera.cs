using UnityEngine;
using System.Collections;

public class SideScrollingCamera: MonoBehaviour
{

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > -2.6f)
        {
            if (transform.position.y < -0.5f)
            {
                transform.position = new Vector3(player.transform.position.x, -0.5f, transform.position.z);
            }

            else
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2.1f, transform.position.z);
            }
        }
    }
}
