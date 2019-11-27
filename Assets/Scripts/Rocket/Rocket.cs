﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rocket : MonoBehaviour
{
    Rigidbody2D rbody;
    public float speed;
    //Vector2 currentPos;
    Vector2 targetPos;
    public float pCoeff;
    public Animator netAnim;

    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        targetPos = GetCurrentPos();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    void Update()
    {
        GetTargetPos();
    }

    Vector2 GetCurrentPos()
    {
        return transform.position;
    }

    void UpdateTarget(Vector2 target)
    {
        //rbody.velocity = Vector2.zero;
        targetPos = target;
        rbody.transform.up = targetPos - (Vector2)rbody.transform.position;
    }

    void GetTargetPos()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
            //https://stackoverflow.com/questions/38198745/how-to-detect-left-mouse-click-but-not-when-the-click-occur-on-a-ui-button-compo
        //    return;
        //}

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPos = Input.mousePosition;
            UpdateTarget(Camera.main.ScreenToWorldPoint(screenPos));
        }

        try
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    //touchStart = touch.position;
                    //thrustGain = 1.0f;
                    UpdateTarget(touch.position);
                    break;

                //Determine if the touch is a moving touch
                /*case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    touchDelta = touch.position - touchStart;
                    touchStart = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    touchDelta = Vector2.zero;
                    thrustGain = 0.0f;
                    break;*/
            }
        }
        catch (System.ArgumentException)
        {
            return;
        }

    }

    void Movement()
    {
        //rbody.AddForce(force * Vector2.up);
        float currSpeed = rbody.velocity.magnitude;
        Vector2 currPos = GetCurrentPos();
        Vector2 delta = targetPos - currPos;
        Vector2 deltaNorm = delta.normalized;
        //rbody.transform.LookAt(targetPos);
        float desiredSpeed = speed;
        if (delta.magnitude < 1f)
        {
            desiredSpeed *= delta.magnitude * speed;
        }
        if (delta.magnitude < 0.01)
        {
            anim.SetBool("moving", false);
            rbody.velocity = Vector2.zero;
        }
        else
        {
            if (currSpeed < desiredSpeed)
            {
                anim.SetBool("moving", true);
                rbody.AddForce(deltaNorm * speed);
            }
        }
        /*
        pCoeff = Mathf.Min(delta.magnitude, 1f)*0.5f;
        if (delta.magnitude < 0.01)
        {
            rbody.velocity = Vector2.zero;
        }
        else
        {
            rbody.AddForce(deltaNorm * speed * pCoeff);
        }*/
    }

    public void Scoop(GameObject g)
    {
        //TODO: stop moving when scooping
        //GameObject g = GameObject.Find("Collectable");
        Vector2 location = g.transform.position;
        Vector2 dir = ((Vector2)transform.position - location);
        float ang = Vector2.SignedAngle(dir, (Vector2)transform.up);
        if (ang > 0)
        {
            print("Scoop way 1");
            netAnim.SetTrigger("scoop_left");
        }
        else
        {
            print("Scoop way 2");
            netAnim.SetTrigger("scoop_right");
        }
    }


}
