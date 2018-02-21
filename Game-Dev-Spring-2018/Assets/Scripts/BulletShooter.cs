using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour
{
    public GameObject bullet;
    public float destroyTime;
    public float bulletSpeed;
    public float spawnTime;

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
        GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
        clone.GetComponent<Bullet>().destroyTime = destroyTime;
        clone.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(ShootBullet(bullet));
    }
}
