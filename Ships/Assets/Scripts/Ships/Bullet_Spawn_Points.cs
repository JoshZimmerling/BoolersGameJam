using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShipShooting;

public class Bullet_Spawn_Points : MonoBehaviour
{

    [SerializeField] ShotDirection Bullet_Direction;

    public ShotDirection GetDirection() 
    { 
        return Bullet_Direction;
    }
}
