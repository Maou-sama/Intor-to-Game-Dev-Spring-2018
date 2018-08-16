using UnityEngine;
using System.Collections;

public class PathFinding : MonoBehaviour
{
    [Header("Path Finding Object's Properties")]
    [SerializeField] private Transform[] path;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float reachDist = 1.0f;

    private float rotateTime = 0.1f;
    private int currentPoint = 0;
    

    // Update is called once per frame
    private void Update()
    {
        //Rotate according to the direction its traveling
        float rotateAngle = Vector3.SignedAngle(path[currentPoint].position - transform.position, Vector3.up, Vector3.forward);
        LeanTween.rotateZ(gameObject, -rotateAngle, rotateTime);

        //Find the distance from the next point to the object and lerp to there
        float dist = Vector3.Distance(path[currentPoint].position, transform.position);
        transform.position = Vector3.Lerp(transform.position, path[currentPoint].position, Time.deltaTime * speed);

        //Change the current point to the next one if object within the reach distance of the point
        if (dist <= reachDist)
        {
            currentPoint++;
        }

        //Loop back to the first point
        if(currentPoint >= path.Length)
        {
            currentPoint = 0;
        }
    }

    //Draw on the gizmo the path of the object
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
