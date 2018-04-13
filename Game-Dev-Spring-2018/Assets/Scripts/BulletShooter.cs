using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour
{
    public GameObject bullet;
    public AudioClip shootSound;
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
        yield return new WaitForSeconds(spawnTime);
        GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
        Bullet cloneBullet = clone.GetComponent<Bullet>();
        cloneBullet.destroyTime = destroyTime;
        cloneBullet.bulletSpeed = bulletSpeed;
        Sound.me.PlaySound(shootSound, 0.25f, 2.0f, 0.0f);
        StartCoroutine(ShootBullet(bullet));
    }
}
