using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float destroyTime;

    void Start()
    {
        StartCoroutine(DeleteSelf());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(bulletSpeed, 0f));
    }

    IEnumerator DeleteSelf()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Movement>().Die();
        }
    }

    
}
