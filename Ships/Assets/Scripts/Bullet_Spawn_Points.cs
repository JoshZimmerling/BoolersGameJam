using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ship_Shooting;

public class Bullet_Spawn_Points : MonoBehaviour
{

    [SerializeField] ShotDirection Bullet_Direction;

    public ShotDirection GetDirection() 
    { 
        return Bullet_Direction;
    }
}
