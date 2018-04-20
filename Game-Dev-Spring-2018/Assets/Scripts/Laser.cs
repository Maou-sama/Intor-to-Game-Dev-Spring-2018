using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    public Transform hitPosition;
    public LayerMask layerToDetect;
    public bool off;
    public float timeOnOff;

    public ParticleSystem ps;
    public ParticleSystem ps2;

    // Use this for initialization

    
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
        lr.useWorldSpace = true;
        StartCoroutine(TurnOff());
    }

    // Update is called once per frame
    void Update()
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

    void DrawLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, layerToDetect);
        if(hit.collider.tag == "Player")
        {
            hit.collider.gameObject.GetComponent<Movement>().Die();
        }
        if(hit != false)
            hitPosition.position = hit.point;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, hitPosition.position);
    }
    
    IEnumerator TurnOn()
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

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(timeOnOff);
        off = true;
        StartCoroutine(TurnOn());
    }
}
