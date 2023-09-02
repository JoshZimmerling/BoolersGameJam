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

    float shipHP;
    float shipSpeed;
    float shipCost;
    int playerNum;

    ShipMovement moveController;
    Ship_Shooting shootController;

    private void Start()
    {
        moveController = GetComponent<ShipMovement>();
        shootController = GetComponent<Ship_Shooting>();

        switch (shipType)
        {
            case (Ship.shipTypes.Destroyer):
                shipHP = 50f;
                shipSpeed = 3f;
                shipCost = 20f;
                break;
            case (Ship.shipTypes.Hawk):
                shipHP = 20f;
                shipSpeed = 5f;
                shipCost = 20f;
                break;
            case (Ship.shipTypes.Challenger):
                shipHP = 60f;
                shipSpeed = 3f;
                shipCost = 20f;
                break;
            case (Ship.shipTypes.Goliath):
                shipHP = 150f;
                shipSpeed = 3f;
                shipCost = 35f;
                break;
            case (Ship.shipTypes.Lightning):
                shipHP = 20f;
                shipSpeed = 5f;
                shipCost = 35f;
                break;
            case (Ship.shipTypes.Drone):
                shipHP = 40f;
                shipSpeed = 5f;
                shipCost = 5f;
                break;
            case (Ship.shipTypes.Scout):
                shipHP = 10f;
                shipSpeed = 6f;
                shipCost = 10f;
                break;
        }
    }

    public void setDestination(float x, float y)
    {
        moveController.setTargetDestination(new Vector2(x, y));
    }

    public void doDamage(float damage)
    {
        shipHP -= damage;
        //TODO: Health bar updates here
        if(shipHP <= 0)
        {
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
}
