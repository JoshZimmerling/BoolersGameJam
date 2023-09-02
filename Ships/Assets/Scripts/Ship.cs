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
    float currentShipHP;
    float shipSpeed;
    float shipCost;

    ShipMovement moveController;
    Ship_Shooting shootController;
    Healthbar healthBar;

    private void Start()
    {
        moveController = GetComponent<ShipMovement>();
        shootController = GetComponent<Ship_Shooting>();
        healthBar = GetComponentInChildren<Healthbar>();

        switch (shipType)
        {
            case (Ship.shipTypes.Destroyer):
                maxShipHP = 50f;
                currentShipHP = maxShipHP;
                shipSpeed = 3f;
                shipCost = 20f;
                break;
            case (Ship.shipTypes.Hawk):
                maxShipHP = 20f;
                currentShipHP = maxShipHP;
                shipSpeed = 5f;
                shipCost = 20f;
                break;
            case (Ship.shipTypes.Challenger):
                maxShipHP = 60f;
                currentShipHP = maxShipHP;
                shipSpeed = 3f;
                shipCost = 20f;
                break;
            case (Ship.shipTypes.Goliath):
                maxShipHP = 150f;
                currentShipHP = maxShipHP;
                shipSpeed = 3f;
                shipCost = 35f;
                break;
            case (Ship.shipTypes.Lightning):
                maxShipHP = 20f;
                currentShipHP = maxShipHP;
                shipSpeed = 5f;
                shipCost = 35f;
                break;
            case (Ship.shipTypes.Drone):
                maxShipHP = 40f;
                currentShipHP = maxShipHP;
                shipSpeed = 5f;
                shipCost = 5f;
                break;
            case (Ship.shipTypes.Scout):
                maxShipHP = 10f;
                currentShipHP = maxShipHP;
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
        currentShipHP -= damage;
        healthBar.updateHPBar(maxShipHP, currentShipHP);
        if (currentShipHP <= 0)
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
