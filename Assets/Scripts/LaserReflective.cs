using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LaserReflective : MonoBehaviour
{
    private LineRenderer lr;

    [Header("Components Used For Making Laser")]
    [SerializeField] private GameObject hitPoint;
    [SerializeField] private LayerMask layerToDetect;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private ParticleSystem ps2;

    [Header("Laser's Properties")]
    [SerializeField] private float timeOnOff;

    private bool off;

    // Use this for initialization
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
        lr.useWorldSpace = true;
        StartCoroutine(TurnOff());
    }

    // Update is called once per frame
    private void Update()
    {
        /*if (!off)
        {
            lr.enabled = true;
            DrawLaser();
        }
        else
        {
            lr.enabled = false;
        }*/
        DrawLaser();
    }

    //Draw laser by casting a ray from the base of the laser to any collider within the layer
    //Set the hit point to the point of collision and draw a line
    //If the raycast hit player then player die
    void DrawLaser()
    {
        int laserLimit = 80;
        int laserReflected = 1;
        int vertexCounter = 1;
        bool loopActive = true;
        Vector2 laserDirection = transform.up;
        Vector2 lastLaserPosiiton = transform.position;

        lr.positionCount = 1;
        lr.SetPosition(0, transform.position);

        while (loopActive) {
            RaycastHit2D hit = Physics2D.Raycast(lastLaserPosiiton, laserDirection, Mathf.Infinity, layerToDetect);

            /*if (hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Movement>().Die();
            }*/
            if (hit)
            {
                if (hit.collider.tag == "ReflectiveSurface")
                {
                    Debug.Log("Bounce");
                    laserReflected++;
                    vertexCounter += 3;
                    lr.positionCount = vertexCounter;
                    lr.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosiiton, 0.01f));
                    lr.SetPosition(vertexCounter - 2, hit.point);
                    lr.SetPosition(vertexCounter - 1, hit.point);
                    lastLaserPosiiton = hit.point;
                    laserDirection = Vector2.Reflect(laserDirection, hit.normal);
                }
                else
                {
                    vertexCounter++;
                    lr.positionCount = vertexCounter;
                    lr.SetPosition(vertexCounter - 1, hit.point);

                    loopActive = false;
                }
            }
            else
            {
                laserReflected++;
                vertexCounter++;
                lr.positionCount = vertexCounter;
                lr.SetPosition(vertexCounter - 1, lastLaserPosiiton + (laserDirection.normalized * 100));

                loopActive = false;
            }
            if(laserReflected > laserLimit)
            {
                Debug.Log(laserReflected);
                loopActive = false;
            }
        }
    }
    
    //Play a particle system every 1/3 of the time require to shoot to imitate charging
    //Stop the particle systmes when the laser shoot
    private IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(timeOnOff / 3);
        ps.Play();
        yield return new WaitForSeconds(timeOnOff / 3);
        ps2.Play();
        yield return new WaitForSeconds(timeOnOff / 3);
        off = false;
        ps.Stop();
        ps2.Stop();
        StartCoroutine(TurnOff());
    }

    private IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(timeOnOff);
        off = true;
        StartCoroutine(TurnOn());
    }
}
