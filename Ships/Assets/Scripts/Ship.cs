using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Netcode;
using UnityEngine;
using System;

public class Ship : NetworkBehaviour
{
    public enum ShipTypes
    {
        Destroyer,
        Hawk,
        Challenger,
        Goliath,
        Lightning,
        Drone,
        Scout
    }

    [SerializeField]
    ShipTypes shipType;

    float maxShipHP;
    NetworkVariable<float> currentShipHP = new NetworkVariable<float>();
    float shipAcceleration;
    float shipMaxSpeed;
    float shipCost;
    float shipTurnRate;

    [SerializeField] SpriteRenderer accentsSprite;
    [SerializeField] SpriteRenderer mapMarkerSprite;
    [SerializeField] Outline outline;
    
    Healthbar healthBar;

    public override void OnNetworkSpawn()
    {
        //shootController = GetComponent<Ship_Shooting>();
        healthBar = GetComponentInChildren<Healthbar>();

        // Initializing ship variables
        switch (shipType)
        {
            case (Ship.ShipTypes.Destroyer):
                maxShipHP = 50f;
                shipMaxSpeed = 3f;
                shipAcceleration = .1f;
                shipTurnRate = 50f;
                shipCost = 20f;
                break;
            case (Ship.ShipTypes.Hawk):
                maxShipHP = 20f;
                shipMaxSpeed = 5f;
                shipAcceleration = 1f;
                shipTurnRate = 60f;
                shipCost = 20f;
                break;
            case (Ship.ShipTypes.Challenger):
                maxShipHP = 60f;
                shipMaxSpeed = 3f;
                shipAcceleration = .08f;
                shipTurnRate = 45f;
                shipCost = 20f;
                break;
            case (Ship.ShipTypes.Goliath):
                maxShipHP = 170f;
                shipMaxSpeed = 3f;
                shipAcceleration = .05f;
                shipTurnRate = 30f;
                shipCost = 35f;
                break;
            case (Ship.ShipTypes.Lightning):
                maxShipHP = 30f;
                shipMaxSpeed = 5.5f;
                shipAcceleration = .3f;
                shipTurnRate = 60f;
                shipCost = 35f;
                break;
            case (Ship.ShipTypes.Drone):
                maxShipHP = 40f;
                shipMaxSpeed = 5f;
                shipAcceleration = .2f;
                shipTurnRate = 1000f;
                shipCost = 5f;
                break;
            case (Ship.ShipTypes.Scout):
                maxShipHP = 10f;
                shipMaxSpeed = 6f;
                shipAcceleration = 1f;
                shipTurnRate = 120f;
                shipCost = 10f;
                break;
        }
        if (IsHost)
            currentShipHP.Value = maxShipHP;

        // Disable the fog clearer and outline on opponent ships
        if (!IsOwner)
        {
            GetComponentInChildren<SpriteMask>().enabled = false;
            outline.gameObject.SetActive(false);
            mapMarkerSprite.gameObject.SetActive(true);
            gameObject.layer = LayerMask.NameToLayer("EnemyShip"); //Sets layer to hostile ships
        }

        // Set the team color
        accentsSprite.color = GameManager.Singleton.playerColors[OwnerClientId];
        mapMarkerSprite.color = GameManager.Singleton.playerColors[OwnerClientId];
        outline.SetupOutlineColor(GameManager.Singleton.playerColors[OwnerClientId]);

        // Update healthbar for both players when it changes
        currentShipHP.OnValueChanged += (float previousValue, float newValue) => {
            healthBar.UpdateHPBar(currentShipHP.Value / maxShipHP);
        };

        ShipMovementSpawn();
    }

    public void Update()
    {
        ShipMovement();
    }

    // Helper Functions
    public void DoDamage(float damage)
    {
        currentShipHP.Value -= damage;
        if (currentShipHP.Value <= 0)
        {
            this.GetComponent<NetworkObject>().Despawn();
            Destroy(this.gameObject);
        }
    }

    public void Select()
    {
        outline.SetSelected();
    }

    public void Unselect()
    {
        outline.SetUnselected();
    }

    // Getter Functions
    public ShipTypes GetShipType()
    {
        return shipType;
    }
    public float GetShipMaxSpeed()
    {
        return shipMaxSpeed;
    }
    public float GetShipAcceleration() 
    {
        return shipAcceleration;
    }
    public float GetShipTurnRate()
    {
        return shipTurnRate;
    }
    public float GetShipCost()
    {
        return shipCost;
    }

    // ======================= Ship Movement =========================

    //TODO pub for tests

    // turn rate, stopping time, max speed, acceleration, 

    float angle;
    float totalVelocity;
    float distToTarget;
    float timeToStop;
    float brakeTimer;

    Vector2 targetPos;
    Vector2 track;

    bool noTarget;
    Boolean moving;
    Boolean backingUp;

    Ship ship;

    [SerializeField] float distToStop;

    /*    LineRenderer lineRenderer;*/


    // Start is called before the first frame update
    public void ShipMovementSpawn()
    {
        noTarget = true;
        ship = transform.GetComponent<Ship>();

        distToStop = 2;
    }

    private void ShipMovement()
    {
        if (!IsHost)
            return;

        track = targetPos - (Vector2)transform.position;
        angle = Vector2.SignedAngle(track, transform.up);

        //path = (Vector2)transform.position - targetPos;
        distToTarget = Vector2.Distance(transform.position, targetPos);

        //up = transform.up;

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
                    transform.Rotate(0, 0, -ship.GetShipTurnRate() * Time.deltaTime);
                }
                else
                {
                    transform.Rotate(0, 0, ship.GetShipTurnRate() * Time.deltaTime);
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
                totalVelocity += ship.GetShipAcceleration();
                if (totalVelocity > ship.GetShipMaxSpeed())
                {
                    totalVelocity = ship.GetShipMaxSpeed();
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
                totalVelocity += ship.GetShipAcceleration();
                if (totalVelocity > ship.GetShipMaxSpeed())
                {
                    totalVelocity = ship.GetShipMaxSpeed();
                }
                transform.Translate(-Vector2.up * totalVelocity * Time.deltaTime);
            }
        }

    }

    [ServerRpc]
    public void BackupServerRPC()
    {
        targetPos = transform.position + (-transform.up * distToStop);
        backingUp = true;
        noTarget = false;
    }

    [ServerRpc]
    public void StopShipServerRPC()
    {
        targetPos = transform.position + transform.up * distToStop;
    }

    [ServerRpc]
    public void SetTargetDestinationServerRPC(Vector2 target)
    {
        noTarget = false;
        backingUp = false;
        targetPos = target;
    }
}
