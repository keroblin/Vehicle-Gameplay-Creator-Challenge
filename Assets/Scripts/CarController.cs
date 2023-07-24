using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration;
    public float mouseSensitivity = 14f;
    public float speed = 3f;
    public Rigidbody rb;
    public bool inputEnabled;

    public Vector3 targetVelocity;
    public Vector3 overrideVelocity; //might not need based on how we do this, but can be used to alter the velocity from outside objects

    Camera cam;

    void Start()
    {
        cam = Camera.main; //going to use for looking later
    }

    void FixedUpdate()
    {

        //movement
        if (inputEnabled)
        {
            //rotation
            Quaternion rotation = rb.rotation;
            Vector3 euler = rotation.eulerAngles;
            float leanMod = 0f;
            if (Input.GetKey(KeyCode.E))
            {
                leanMod = -1;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                leanMod = 1;
            }
            euler += new Vector3(0f, 0f, leanMod * ((speed*2)*Time.deltaTime));
            //mouse input
            Vector2 lookAngle;
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookAngle = Vector2.zero;
                rb.rotation = Quaternion.identity;
                Cursor.lockState = CursorLockMode.Locked;
            }

            lookAngle = new Vector2(Input.GetAxis("Mouse Y") * mouseSensitivity, Input.GetAxis("Mouse X") * mouseSensitivity);
            euler.x -= lookAngle.x;
            euler.y += lookAngle.y;

            rotation = Quaternion.Euler(euler);
            rb.MoveRotation(rotation);
            
            //main movement
            targetVelocity = Vector3.zero;

            float heightMod = 0f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                heightMod = -1;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                heightMod = 1;
            }

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), heightMod ,Input.GetAxisRaw("Vertical"));
            Vector3 movement = Vector3.zero;
            if(input.magnitude > 0) //if anything is pressed
            {
                //speed = Mathf.Lerp(speed, acceleration, Time.deltaTime);
                movement = input * (speed*Time.deltaTime);
            }
            movement = transform.TransformDirection(movement);
            targetVelocity = movement + overrideVelocity;
            rb.AddForce(targetVelocity,ForceMode.Impulse);
        }

    }
}
