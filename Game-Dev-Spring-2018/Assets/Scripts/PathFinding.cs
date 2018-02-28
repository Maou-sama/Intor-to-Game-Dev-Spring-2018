using UnityEngine;
using System.Collections;

public class PathFinding : MonoBehaviour
{
    public Transform[] path;
    public float speed = 5.0f;
    public float reachDist = 1.0f;
    public int currentPoint = 0;
    public float rotateTime;

    // Update is called once per frame
    void Update()
    {
        //Vector3 dir = path[currentPoint].position - transform.position;
        float rotateAngle = Vector3.SignedAngle(path[currentPoint].position - transform.position, Vector3.up, Vector3.forward);

        LeanTween.rotateZ(gameObject, -rotateAngle, rotateTime);

        float dist = Vector3.Distance(path[currentPoint].position, transform.position);
        transform.position = Vector3.Lerp(transform.position, path[currentPoint].position, Time.deltaTime * speed);

        if (dist <= reachDist)
        {
            currentPoint++;
        }

        if(currentPoint >= path.Length)
        {
            currentPoint = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (path.Length > 0)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                { 
                    Gizmos.DrawSphere(path[i].position, reachDist);
                }
            }

            for (int i = 0; i < path.Length - 1; i++)
            {
                if (path[i] != null && path[i + 1] != null)
                {
                    Gizmos.DrawLine(path[i].position, path[i + 1].position);
                }
            }

            if (path[path.Length - 1] != null)
            {
                Gizmos.DrawLine(path[path.Length - 1].position, path[0].position);
            }

        }
    }
}
