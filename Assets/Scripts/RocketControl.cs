using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    Rigidbody2D rbody;
    Vector2 touchDelta;
    Vector2 touchStart;
    public float torqueCoeff;
    public float thrustCoeff;
    float thrustGain;

    //This script is used only to control the rocket
    // Start is called before the first frame update
    void Start()
    {
        touchDelta = Vector2.zero;
        thrustGain = 0f;
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {       
        ProcessInput();
        ApplyTorque();
        ApplyThrust();
    }

    void ProcessInput()
    {
        //Get input from touch screen
        //Input.GetTouch(); https://docs.unity3d.com/ScriptReference/Input.GetTouch.html
        //Touch https://docs.unity3d.com/ScriptReference/Touch.html
        try
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    touchStart = touch.position;
                    thrustGain = 1.0f;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    touchDelta = touch.position - touchStart;
                    touchStart = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    touchDelta = Vector2.zero;
                    thrustGain = 0.0f;
                    break;
            }
        }
        catch (System.ArgumentException)
        {
            return;
        }

        //manipulate rigidbody2D 
        //rbody.AddForce();
        //rbody.velocity =
        //rbody.AddTorque();
        //rbody.angularVelocity =
    }

    void ApplyTorque()
    {
        float yDelta = touchDelta.x;
        rbody.AddTorque(torqueCoeff * yDelta);
    }

    void ApplyThrust()
    {
        Vector2 up = rbody.transform.up;
        rbody.AddForce(thrustCoeff * thrustGain * up);
    }
}
