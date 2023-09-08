using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShipType
{
    public static ShipType Destroyer = new("Destroyer");
    public static ShipType Hawk = new("Hawk");
    public static ShipType Challenger = new("Challenger");
    public static ShipType Goliath = new("Goliath");
    public static ShipType Lightning = new("Lightning");
    public static ShipType Drone = new("Drone");
    public static ShipType Scout = new("Scout");

    public string shipName;
    public int shipCost;

    public float maxHP;

    public float shipMaxSpeed;
    public float shipAcceleration;
    public float shipTurnRadius;

    public float bulletLifetime;
    public float bulletSpeed;
    public float bulletsPerSecond;
    public float bulletDamage;

    public ShipType(string name)
    {
        shipName = name;
        switch (name)
        {
            case "Destroyer":
                shipCost = 20;
                maxHP = 50;

                shipMaxSpeed = 3;
                shipAcceleration = 0.1f;
                shipTurnRadius = 50;

                bulletLifetime = 2;
                bulletSpeed = 8;
                bulletsPerSecond = 1;
                bulletDamage = 1.5f;
                break;
            case "Hawk":
                shipCost = 20;
                maxHP = 20;

                shipMaxSpeed = 5;
                shipAcceleration = 1;
                shipTurnRadius = 60;

                bulletLifetime = 1.75f;
                bulletSpeed = 12;
                bulletsPerSecond = 1;
                bulletDamage = 10;
                break;
            case "Challenger":
                shipCost = 20;
                maxHP = 60;

                shipMaxSpeed = 3;
                shipAcceleration = 0.08f;
                shipTurnRadius = 45;

                bulletLifetime = 0.75f;
                bulletSpeed = 12;
                bulletsPerSecond = 0.75f;
                bulletDamage = 3;
                break;
            case "Goliath":
                shipCost = 20;
                maxHP = 50;

                shipMaxSpeed = 3;
                shipAcceleration = 0.1f;
                shipTurnRadius = 50;

                bulletLifetime = 2;
                bulletSpeed = 8;
                bulletsPerSecond = 1;
                bulletDamage = 1.5f;
                break;
            case "Lightning":
                shipCost = 20;
                maxHP = 50;

                shipMaxSpeed = 3;
                shipAcceleration = 0.1f;
                shipTurnRadius = 50;

                bulletLifetime = 2;
                bulletSpeed = 8;
                bulletsPerSecond = 1;
                bulletDamage = 1.5f;
                break;
            case "Drone":
                shipCost = 20;
                maxHP = 50;

                shipMaxSpeed = 3;
                shipAcceleration = 0.1f;
                shipTurnRadius = 50;

                bulletLifetime = 2;
                bulletSpeed = 8;
                bulletsPerSecond = 1;
                bulletDamage = 1.5f;
                break;
            case "Scout":
                shipCost = 20;
                maxHP = 50;

                shipMaxSpeed = 3;
                shipAcceleration = 0.1f;
                shipTurnRadius = 50;

                bulletLifetime = 2;
                bulletSpeed = 8;
                bulletsPerSecond = 1;
                bulletDamage = 1.5f;
                break;
            default:
                shipCost = 20;
                maxHP = 50;

                shipMaxSpeed = 3;
                shipAcceleration = 0.1f;
                shipTurnRadius = 50;

                bulletLifetime = 2;
                bulletSpeed = 8;
                bulletsPerSecond = 1;
                bulletDamage = 1.5f;
                break;
        }
    }
}
