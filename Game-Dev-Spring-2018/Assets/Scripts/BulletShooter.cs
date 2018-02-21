using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour
{
    public GameObject bullet;
    public float destroyTime;

    private bool startShooting;
    // Use this for initialization
    void Start()
    {
        startShooting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startShooting)
        {
            StartCoroutine(ShootBullet(bullet));
            startShooting = false;
        }
    }

    IEnumerator ShootBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2.5f);
        GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
        clone.GetComponent<Bullet>().destroyTime = destroyTime;
        StartCoroutine(ShootBullet(bullet));
    }
}
