using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipMovement : MonoBehaviour
{
    //TODO pub for tests
    public Vector2 targetPos;
    public float angle;
    public float totalVelocity;
    public Vector2 track;
    public float timeToStop;
    public float distToTarget;
    public float secondsToSlow;
    public float force;
    public Vector2 path;
    public Vector2 up;

    Boolean noTarget;

    [SerializeField] float thrust;
    [SerializeField] float maxVelocity;
    [SerializeField] float distToStop;

    Rigidbody2D rb; 

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
            return; 
        }

        if (MathF.Abs(angle) > 1)
        {
            if (angle > 0)
            {
                transform.Rotate(0, 0, -40 * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, 40 * Time.deltaTime);
            }
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos - (Vector2)transform.position);
        }

        if (Mathf.Abs(angle) > 45)
        {
            return;
        }
        
        if (distToTarget < distToStop)
        {
            transform.Translate(Vector2.up * distToTarget * Time.deltaTime);
        }
        else
        {
            totalVelocity += thrust;
            if (totalVelocity > maxVelocity)
            {
                totalVelocity = maxVelocity;
            }
            transform.Translate(Vector2.up * totalVelocity * Time.deltaTime);
        }


        /*        if (noTarget) { return; }

                if (!alignedWithTarget && MathF.Abs(angle) > 0.2)
                {
                    if (angle > 0)
                    {
                        rb.angularVelocity = -20;
                    }
                    else
                    {
                        rb.angularVelocity = 20;
                    }
                }
                else
                {
                    rb.angularVelocity = 0;
                    totalVelocity = rb.velocity.x + rb.velocity.y;
                    distToTarget = Vector2.Distance(transform.position, targetPos);

                    if (distToTarget < distToStop)
                    {
                        rb.AddForce(-transform.up);
                    }

                    else if (totalVelocity < maxVelocity)
                    {
                        rb.AddForce(transform.up);
                    }
                }*/

    }

    public void setTargetDestination(Vector2 target)
    {
        noTarget = false;   
        targetPos = target;
    }
}
