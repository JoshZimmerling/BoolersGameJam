using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    Vector2 targetPos;
    Boolean alignedWithTarget;

    float dotAngle;

    [SerializeField] float thrust;
    [SerializeField] float currentSpeed;
    [SerializeField] float maxSpeed;

    Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Debug.Log(Vector2.Dot(transform.position, targetPos));

        currentSpeed += thrust;
        if (currentSpeed > maxSpeed) { currentSpeed = maxSpeed; }
        rb.velocity = new Vector2(0, currentSpeed); 
    }

    public void setTargetDestination(Vector2 target)
    {
        targetPos = target;
    }
}
