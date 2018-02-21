using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float destroyTime;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(bulletSpeed, 0f));
        StartCoroutine(DeleteSelf());
    }

    IEnumerator DeleteSelf()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
