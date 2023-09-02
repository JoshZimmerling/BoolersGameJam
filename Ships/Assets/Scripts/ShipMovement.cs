using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    //pub for tests
    public Vector2 targetPos;
    public float angle;
    public float totalVelocity;
    public Vector2 track;
    public float distToStop;

    Boolean alignedWithTarget;
    Boolean noTarget;

    [SerializeField] float thrust;
    [SerializeField] float maxVelocity;

    Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        alignedWithTarget = true;
        noTarget = true; 
    }

    private void FixedUpdate()
    {
      
        track = targetPos - (Vector2) transform.position;

        angle = Vector2.SignedAngle(track, transform.up);

        if (noTarget) { return; }

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
            if (totalVelocity < maxVelocity)
            {
                rb.AddForce(transform.up);
            }
        }

    }

    public void setTargetDestination(Vector2 target)
    {
        alignedWithTarget = false;
        noTarget = false;   
        targetPos = target;
    }
}
