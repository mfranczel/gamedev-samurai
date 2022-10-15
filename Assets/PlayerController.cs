using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor)), RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    public float force = 50f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500, movementMask))
            {
                // Move player to what we hit
                motor.MoveToPoint(hit.point);

                // Stop focusing on objects
            }*/
        } else if (Input.GetKey(KeyCode.UpArrow)) { 
			GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * force);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
			GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * force);
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.down * 2);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(Vector3.up * 2);
        } else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

                // Check if we hit an interactable object
                // If yes, set it as object
            }
        }
    }
}
