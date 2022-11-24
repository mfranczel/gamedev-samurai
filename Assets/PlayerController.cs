using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    public float force = 50f;
    public GameObject player;
    private Vector3 movement;
    private Vector3 rotationSpeed = new Vector3(0, 40, 0);
    private Rigidbody rb;
    
    private float walk = 1.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = FindObjectOfType<PlayerMotor>();
        rb = player.GetComponent<Rigidbody>();
        movement = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftShift)) walk = (walk + run);
        // if (Input.GetKeyUp(KeyCode.LeftShift)) walk = (walk - run);
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        movement = new Vector3(Horizontal, 0f, Vertical);

        /*if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.left * 2);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.R)) {
            transform.Rotate(Vector3.right * 2);
        } else */if (Input.GetMouseButtonDown(1))
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

    void FixedUpdate()
    {
        // Quaternion deltaRotation = Quaternion.Euler(movement.x * Time.deltaTime * rotationSpeed);
        // rb.MoveRotation(rb.rotation * deltaRotation);
        // rb.MovePosition(rb.position + Time.fixedDeltaTime *  movement.z * force * transform.forward);
        
        rb.MovePosition(rb.position +  walk * Time.fixedDeltaTime * movement);
    }
}
