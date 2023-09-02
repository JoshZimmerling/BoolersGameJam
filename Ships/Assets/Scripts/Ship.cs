using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
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

    [SerializeField] ShipMovement moveController;
    [SerializeField] Ship_Shooting shootController;


    private void Start()
    {
        moveController = GetComponent<ShipMovement>();
        shootController = GetComponent<Ship_Shooting>();
    }

    public void setDestination(float x, float y)
    {
        moveController.setTargetDestination(new Vector2(x, y));
    }
}
