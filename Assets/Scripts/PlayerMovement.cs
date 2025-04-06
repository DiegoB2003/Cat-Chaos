using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{   
    public float speed = 5.0f;
    public float rotationSpeed = 90;
    public float force = 250f;
    public static float noiseLevel;

    Rigidbody rb;
    Transform t;
    bool isGrounded = false; //Check if player is grounded

    public Vector2 turn; //Vector2 stores direction of the mouse movement

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked; //Locks mouse in place and makes it invisible
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime represents the time that passed since the last frame
        //the multiplication below ensures that GameObject moves constant speed every frame
        Vector3 moveDirection = new Vector3(0, 0, 0);

        moveDirection += Input.GetKey(KeyCode.W) ? transform.forward : Vector3.zero;
        moveDirection -= Input.GetKey(KeyCode.S) ? transform.forward : Vector3.zero;
        moveDirection += Input.GetKey(KeyCode.D) ? transform.right : Vector3.zero;
        moveDirection -= Input.GetKey(KeyCode.A) ? transform.right : Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A)) {
            // Set the trigger value to True for the parameter Dance.
            anim.SetBool("isMoving", true);
        } else {
            anim.SetBool("isMoving", false);
        }

        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + moveDirection * speed * Time.deltaTime);

        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        turn.y = Mathf.Clamp(turn.y, -90, 90); // Clamp the vertical rotation
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        //Jump only if grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * force);
        }
    }

    private void OnCollisionEnter(Collision collision) //If player is grounded, set isGrounded to true
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision) //If player is not grounded, set isGrounded to false
    {
        isGrounded = false;
    }
}