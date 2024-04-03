using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastToGazeTarget2D : MonoBehaviour
{
    //public GameObject GazeTarget;
    //public GameObject Mask_Small_2D;
    RaycastHit hit;
    public Vector3 GazeTargetPosForShader;

    void Update()
    {
        //transform.LookAt(GazeTarget.transform);

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Vector3 hitPoint = hit.point;
            GazeTargetPosForShader = hitPoint;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        Debug.DrawRay(transform.position, forward, Color.green);
    }
}
