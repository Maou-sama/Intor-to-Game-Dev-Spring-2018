using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float destroyTime;
    public float dieTimer;

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
            if (!collision.gameObject.GetComponent<Movement>().crouching)
            {
                collision.gameObject.SetActive(false);
                Camera.main.gameObject.GetComponent<ShakeScreen>().Screenshake(0.8f, 1.6f);
                StartCoroutine(Die());
            }
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(dieTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
