using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Netcode;
using UnityEngine;

public class Ship : NetworkBehaviour
{
    
    public enum shipTypes
    {
        Destroyer,
        Hawk,
        Challenger,
        Goliath,
        Lightning,
        Drone,
        Scout
    }
    [SerializeField] shipTypes shipType;
    [SerializeField] int playerNum;

    float maxShipHP;
    NetworkVariable<float> currentShipHP = new NetworkVariable<float>();
    float shipAcceleration;
    float shipMaxSpeed;
    float shipCost;
    float shipTurnRate; 

    ShipMovement moveController;
    Ship_Shooting shootController;
    Healthbar healthBar;

    public override void OnNetworkSpawn()
    {
        // Update healthbar for both players when it changes
        currentShipHP.OnValueChanged += (float previousValue, float newValue) =>
        {
            healthBar.updateHPBar(maxShipHP, currentShipHP.Value);
        };
    }

    private void Start()
    {
        moveController = GetComponent<ShipMovement>();
        shootController = GetComponent<Ship_Shooting>();
        healthBar = GetComponentInChildren<Healthbar>();

        shipAcceleration = .1f;

        switch (shipType)
        {
            case (Ship.shipTypes.Destroyer):
                maxShipHP = 50f;
                shipMaxSpeed = 3f;
                shipAcceleration = .1f;
                shipTurnRate = 50f;
                shipCost = 20f;
                break;
            case (Ship.shipTypes.Hawk):
                maxShipHP = 20f;
                shipMaxSpeed = 5f;
                shipAcceleration = 1f;
                shipTurnRate = 60f;
                shipCost = 20f;
                break;
            case (Ship.shipTypes.Challenger):
                maxShipHP = 60f;
                shipMaxSpeed = 3f;
                shipAcceleration = .08f;
                shipTurnRate = 45f;
                shipCost = 20f;
                break;
            case (Ship.shipTypes.Goliath):
                maxShipHP = 150f;
                shipMaxSpeed = 3f;
                shipAcceleration = .05f;
                shipTurnRate = 30f;
                shipCost = 35f;
                break;
            case (Ship.shipTypes.Lightning):
                maxShipHP = 30f;
                shipMaxSpeed = 5f;
                shipAcceleration = .3f;
                shipTurnRate = 60f;
                shipCost = 35f;
                break;
            case (Ship.shipTypes.Drone):
                maxShipHP = 40f;
                shipMaxSpeed = 5f;
                shipAcceleration = .2f;
                shipTurnRate = 1000f;
                shipCost = 5f;
                break;
            case (Ship.shipTypes.Scout):
                maxShipHP = 10f;
                shipMaxSpeed = 6f;
                shipAcceleration = 1f;
                shipTurnRate = 120f;
                shipCost = 10f;
                break;
        }

        if (IsHost)
            currentShipHP.Value = maxShipHP;
    }

    public void setDestination(Vector2 dest)
    {
        moveController.setTargetDestinationServerRPC(dest);
    }

    public void StopShip()
    {
        moveController.StopShipServerRPC(); 
    }

    public void doDamage(float damage)
    {
        currentShipHP.Value -= damage;
        if (currentShipHP.Value <= 0)
        {
            this.GetComponent<NetworkObject>().Despawn();
            Destroy(this.gameObject);
        }
    }

    public shipTypes getShipType()
    {
        return shipType;
    }
    public int getPlayerNum()
    {
        return playerNum;
    }
    public float getShipMaxSpeed()
    {
        return shipMaxSpeed;
    }

    public float getShipAcceleration() 
    {
        return shipAcceleration;
    }

    public float getShipTurnRate()
    {
        return shipTurnRate;
    }
    public float getShipCost()
    {
        return shipCost;
    }
}
