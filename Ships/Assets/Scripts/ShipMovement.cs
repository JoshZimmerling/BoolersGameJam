using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipMovement : MonoBehaviour
{
    //TODO pub for tests

    // turn rate, stopping time, max speed, acceleration, 

    float angle;
    float totalVelocity;
    float timeToStop;
    float distToTarget;
    float secondsToSlow;
    float force;

    Vector2 targetPos;
    Vector2 track;



    Boolean noTarget;
    public Boolean moving; 

    [SerializeField] float acceleration;
    [SerializeField] float turnRate;
    [SerializeField] float maxVelocity;
    [SerializeField] float distToStop;


    // Start is called before the first frame update
    void Start()
    {
        noTarget = true;
    }

    private void FixedUpdate()
    {
      
        track = targetPos - (Vector2) transform.position;
        angle = Vector2.SignedAngle(track, transform.up);

        path = (Vector2)transform.position - targetPos;
        distToTarget = Vector2.Distance(transform.position, targetPos); 
        
        up = transform.up; 

        if (noTarget) { return; }


        if (distToTarget < 0.1) 
        {
            noTarget = true;
            totalVelocity = 0;
            moving = false;
            return; 
        }

        if (MathF.Abs(angle) > 10)
        {
            if (angle > 0)
            {
                transform.Rotate(0, 0, -turnRate * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, turnRate * Time.deltaTime);
            }
        }
        else if (MathF.Abs(angle) > 1)
        {
            if (angle > 0)
            {
                transform.Rotate(0, 0, (-10 - (Mathf.Abs(angle) * 3)) * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, (10 + (Mathf.Abs(angle) * 3)) * Time.deltaTime);
            }
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos - (Vector2)transform.position);
        }

        if (Mathf.Abs(angle) > 45 && !moving)
        {
            return;
        }

        moving = true;

        if (distToTarget < distToStop)
        {
            transform.Translate(Vector2.up * distToTarget * Time.deltaTime);
        }
        else
        {
            totalVelocity += acceleration;
            if (totalVelocity > maxVelocity)
            {
                totalVelocity = maxVelocity;
            }
            transform.Translate(Vector2.up * totalVelocity * Time.deltaTime);
        }
    }

    public void setTargetDestination(Vector2 target)
    {
        noTarget = false;   
        targetPos = target;
    }
}
