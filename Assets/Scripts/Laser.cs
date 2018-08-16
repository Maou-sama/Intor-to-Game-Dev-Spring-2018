using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;

    [Header("Components Used For Making Laser")]
    [SerializeField] private Transform hitPosition;
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
        if (!off)
        {
            lr.enabled = true;
            DrawLaser();
        }
        else
        {
            lr.enabled = false;
        }
    }

    //Draw laser by casting a ray from the base of the laser to any collider within the layer
    //Set the hit point to the point of collision and draw a line
    //If the raycast hit player then player die
    void DrawLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, layerToDetect);

        if(hit.collider.tag == "Player")
        {
            hit.collider.gameObject.GetComponent<Movement>().Die();
        }

        if (hit != false)
        {
            hitPosition.position = hit.point;
        }

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, hitPosition.position);
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
