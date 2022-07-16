//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.175f;
    public Vector3 playerOffset;
    private Vector3 velocity = Vector3.zero;


    // Use FixedUpdate if there is jittery problems with the camera
    private void LateUpdate()
    {
        Vector3 finalPos = target.position + playerOffset;
        // SmoothDamp already uses Time.deltaTime so the speed should be consistent regardless of framerate
        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref velocity, smoothSpeed);
    }

}
