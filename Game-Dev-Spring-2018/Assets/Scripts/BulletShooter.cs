using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour
{

    [Header("Bullet Shooter's Properties")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private float destroyTime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float spawnTime;

    private bool startShooting;

    // Use this for initialization
    private void Start()
    {
        startShooting = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (startShooting)
        {
            StartCoroutine(ShootBullet(bullet));
            startShooting = false;
        }
    }

    private IEnumerator ShootBullet(GameObject bullet)
    {
        //Clone a bullet after a certain amount of time at the shooter position
        yield return new WaitForSeconds(spawnTime);
        GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
        Bullet cloneBullet = clone.GetComponent<Bullet>();
        cloneBullet.SetBulletDestroyTimer(destroyTime);
        cloneBullet.SetBulletSpeed(bulletSpeed);

        //Play the sound of bullet shot
        Sound.me.PlaySound(shootSound, 0.25f, 2.0f, 0.0f);

        //Start the couroutine again to loop shooting
        StartCoroutine(ShootBullet(bullet));
    }
}
