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

    Vector2 targetPos;
    Vector2 track;
    Vector2 up;
    Vector2 path;

    bool noTarget;
    Boolean moving;
    Boolean backingUp;

    Ship ship;

    [SerializeField] float distToStop;

/*    LineRenderer lineRenderer;*/


    // Start is called before the first frame update
    void Start()
    {
        noTarget = true;
        ship = transform.GetComponent<Ship>();

        distToStop = 2;
    }

    private void FixedUpdate()
    {

        if (!IsHost)
            return;

        track = targetPos - (Vector2) transform.position;
        angle = Vector2.SignedAngle(track, transform.up);

        path = (Vector2)transform.position - targetPos;
        distToTarget = Vector2.Distance(transform.position, targetPos); 
        
        up = transform.up; 

        if (noTarget) { return; }

        // Stopping
        if (distToTarget < 0.1) 
        {
            noTarget = true;
            totalVelocity = 0;
            moving = false;
            backingUp = false;
            return; 
        }

        if (!backingUp)
        {
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
                transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos - (Vector2)transform.position);
            }

            // Prevents moving the ship if not moving and too high an angle
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
                totalVelocity += ship.getShipAcceleration();
                if (totalVelocity > ship.getShipMaxSpeed())
                {
                    totalVelocity = ship.getShipMaxSpeed();
                }
                transform.Translate(Vector2.up * totalVelocity * Time.deltaTime);
            }
        }
        else // backing up
        {
            if (distToTarget < distToStop)
            {
                transform.Translate(-Vector2.up * distToTarget * Time.deltaTime);
            }
            else
            {
                totalVelocity += ship.getShipAcceleration();
                if (totalVelocity > ship.getShipMaxSpeed())
                {
                    totalVelocity = ship.getShipMaxSpeed();
                }
                transform.Translate(-Vector2.up * totalVelocity * Time.deltaTime);
            }
        }

    }

    [ServerRpc]
    public void BackupServerRPC ()
    {
        targetPos = transform.position + (-transform.up * distToStop);
        backingUp = true;
        noTarget = false; 
    }

    [ServerRpc]
    public void StopShipServerRPC ()
    {
        targetPos = transform.position + transform.up * distToStop; 
    }

    [ServerRpc]
    public void setTargetDestinationServerRPC(Vector2 target)
    {
        noTarget = false;
        backingUp = false; 
        targetPos = target;
    }
}
