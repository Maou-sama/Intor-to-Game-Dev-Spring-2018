using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Properties")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float destroyTime;

    private void Start()
    {
        StartCoroutine(DeleteSelf());
    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    public void SetBulletDestroyTimer(float time)
    {
        destroyTime = time;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(new Vector2(bulletSpeed, 0f));
    }

    private IEnumerator DeleteSelf()
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
