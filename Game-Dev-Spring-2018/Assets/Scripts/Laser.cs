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
    // Use this for initialization

    
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
        lr.useWorldSpace = true;
        StartCoroutine(TurnOnOff());
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(hit != false)
            hitPosition.position = hit.point;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, hitPosition.position);
    }
    
    IEnumerator TurnOnOff()
    {
        yield return new WaitForSeconds(timeOnOff);
        off = !off;
        StartCoroutine(TurnOnOff());
    }
}
