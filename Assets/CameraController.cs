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

    public GameObject player;
    public float cameraDistance = 10.0f;

    public float sensitivity = 1.0f;
    void Update()
    {
        currZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        
        currZoom = Mathf.Clamp(currZoom, minZoom, maxZoom);
        
        float rotateHorizontal = Input.GetAxis ("Mouse X");
        float rotateVertical = Input.GetAxis ("Mouse Y");
        transform.RotateAround (player.transform.position, -Vector3.up, rotateHorizontal * sensitivity);
        transform.RotateAround (Vector3.zero, transform.right, rotateVertical * sensitivity);

        // yawInput -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        // transform.position = player.transform.position - player.transform.forward * cameraDistance;
        // transform.LookAt (player.transform.position);
        // transform.position = new Vector3 (transform.position.x, transform.position.y + 5, transform.position.z);
        
        
        
        // transform.position = target.position - offset * currZoom;
        // transform.LookAt(target.position + Vector3.up * pitch);
        // transform.RotateAround(target.position, Vector3.up, yawInput);
    }
}
