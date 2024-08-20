using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform target; // The target object to follow

    [SerializeField]
    private Vector3 offset; // Offset from the target object
    [SerializeField] private int TripBalls;

    [SerializeField]
    private float smoothSpeed = 0.125f; // Smooth speed for camera movement

    private void LateUpdate()
    {
        switch(TripBalls)
        {
            case -1:
                break;
            case 1:
                TripBalls1();
                break;
            case 2:
                TripBalls2();
                break;
            default:
                NormalMethod();
                break;
        }
    }
    private void NormalMethod()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, -10);
        Vector3 smoothedPosition = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10), desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
    private void TripBalls1()
    {
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = -1 * smoothedPosition;
        transform.LookAt(target);
    }

    private void TripBalls2()
    {
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
