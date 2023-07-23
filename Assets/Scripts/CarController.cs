using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration;
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
        if (inputEnabled)
        {
            targetVelocity = Vector3.zero;
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector3 movement = Vector3.zero;
            if(input.magnitude > 0) //if anything is pressed
            {
                //speed = Mathf.Lerp(speed, acceleration, Time.deltaTime);
                movement.x = input.x * speed;
                movement.z = input.y * speed;
            }

            targetVelocity = movement + overrideVelocity;
            //targetVelocity = direction * speed;
            rb.velocity = targetVelocity;
            //rb.AddForce(targetVelocity);
        }
    }
}
