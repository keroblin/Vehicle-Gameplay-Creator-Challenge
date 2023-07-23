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

        //movement
        if (inputEnabled)
        {
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
            Debug.Log(heightMod);

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), heightMod ,Input.GetAxisRaw("Vertical"));
            Vector3 movement = Vector3.zero;
            if(input.magnitude > 0) //if anything is pressed
            {
                //speed = Mathf.Lerp(speed, acceleration, Time.deltaTime);
                movement = input * (speed*Time.deltaTime);
            }

            targetVelocity = movement + overrideVelocity;
            //targetVelocity = direction * speed;
            rb.AddForce(targetVelocity,ForceMode.VelocityChange);
            //rb.velocity = targetVelocity;
            //rb.AddForce(targetVelocity);
        }


    }
}
