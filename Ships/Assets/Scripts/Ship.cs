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

    [SerializeField]
    shipTypes shipType;
    int playerNum;

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
    ShipMovement moveController;

    public override void OnNetworkSpawn()
    {
        moveController = GetComponent<ShipMovement>();
        //shootController = GetComponent<Ship_Shooting>();
        healthBar = GetComponentInChildren<Healthbar>();

        // Initializing ship variables
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
                maxShipHP = 170f;
                shipMaxSpeed = 3f;
                shipAcceleration = .05f;
                shipTurnRate = 30f;
                shipCost = 35f;
                break;
            case (Ship.shipTypes.Lightning):
                maxShipHP = 30f;
                shipMaxSpeed = 5.5f;
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

        // Set the team color
        accentsSprite.color = GameManager.Singleton.playerColors[OwnerClientId];
        mapMarkerSprite.color = GameManager.Singleton.playerColors[OwnerClientId];
        outline.SetupOutlineColor(GameManager.Singleton.playerColors[OwnerClientId]);

        // Update healthbar for both players when it changes
        currentShipHP.OnValueChanged += (float previousValue, float newValue) => {
            healthBar.UpdateHPBar(currentShipHP.Value / maxShipHP);
        };

        // Disable the fog clearer and outline on opponent ships
        if (!IsOwner) {
            GetComponentInChildren<SpriteMask>().enabled = false;
            outline.gameObject.SetActive(false);
        }
    }

    // Movement Functions
    public void SetDestination(Vector2 dest)
    {
        moveController.SetTargetDestinationServerRPC(dest);
    }

    public void StopShip()
    {
        moveController.StopShipServerRPC(); 
    }

    public void ReverseShip()
    {
        moveController.BackupServerRPC(); 
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
    public shipTypes GetShipType()
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
}
