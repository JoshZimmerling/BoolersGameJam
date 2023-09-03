using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipMovement : NetworkBehaviour
{
    //TODO pub for tests

    // turn rate, stopping time, max speed, acceleration, 

    float angle;
    float totalVelocity;
    float distToTarget;
    float timeToStop;
    float brakeTimer; 

    NetworkVariable<Vector2> targetPos = new NetworkVariable<Vector2>(new Vector2(), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    Vector2 track;
    Vector2 up;
    Vector2 path; 

    Boolean noTarget;
    Boolean moving;

    Ship ship;

    [SerializeField] float distToStop;

/*    LineRenderer lineRenderer;*/


    // Start is called before the first frame update
    void Start()
    {
        noTarget = true;
        ship = transform.GetComponent<Ship>();

/*        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(.1f, .1f);*/
        distToStop = 2;
    }

    private void FixedUpdate()
    {
        if (!IsHost)
            return;

        track = targetPos.Value - (Vector2) transform.position;
        angle = Vector2.SignedAngle(track, transform.up);

        path = (Vector2)transform.position - targetPos.Value;
        distToTarget = Vector2.Distance(transform.position, targetPos.Value); 
        
        up = transform.up; 

        if (noTarget) { return; }


        // Stopping
        if (distToTarget < 0.1) 
        {
            noTarget = true;
            totalVelocity = 0;
            moving = false;
            return; 
        }

        // Turning
        if (MathF.Abs(angle) > 10)
        {
            if (angle > 0)
            {
                transform.Rotate(0, 0, -ship.getShipTurnRate() * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, ship.getShipTurnRate() * Time.deltaTime);
            }
        }
        // Slowing turns
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
        // If the angle is small enough, will lock towards target
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos.Value - (Vector2)transform.position);
        }

        // Prevents moving the ship if not moving and too high an angle
        if (Mathf.Abs(angle) > 45 && !moving)
        {
            return;
        }


        moving = true;
/*        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetPos);*/

        if (distToTarget < distToStop)
        {
            transform.Translate(Vector2.up * distToTarget * Time.deltaTime);
        }
        else
        {
            totalVelocity += ship.getShipAcceleration();
            if (totalVelocity > ship.getShipMaxSpeed())
            {
                totalVelocity = ship.getShipMaxSpeed();
            }
            transform.Translate(Vector2.up * totalVelocity * Time.deltaTime);
        }

    }
    
    public void StopShip ()
    {
        targetPos.Value = transform.position + transform.up * distToStop; 
    }

    public void setTargetDestination(Vector2 target)
    {
        noTarget = false;
        targetPos.Value = target;
    }
}
