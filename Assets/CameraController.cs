using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private float currZoom = 10f;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 10f;
    public float maxZoom = 10f;
    public float yawSpeed = 100f;
    private float yawInput = 0f;


    void Update()
    {
        currZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        
        currZoom = Mathf.Clamp(currZoom, minZoom, maxZoom);

        yawInput -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, yawInput);
    }
}
