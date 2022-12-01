using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float lookSensitivity;
    public float minXLook;
    public float maxXLook;
    public Transform camAnchor;

    public bool invertXRotation;

    private float curXRot;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (PauseMenu.GamePaused) {
            return;
        }
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        // Rotate horizontally
        transform.eulerAngles += Vector3.up * x * lookSensitivity;

        // Add vertical movement and clamp
        if (invertXRotation)
        {
            curXRot += y * lookSensitivity;
        }
        else {
            curXRot -= y * lookSensitivity;
        }
        curXRot = Mathf.Clamp(curXRot, minXLook, maxXLook);

        Vector3 clampedAngle = camAnchor.eulerAngles;
        clampedAngle.x = curXRot;
        camAnchor.eulerAngles = clampedAngle;
    }
}
