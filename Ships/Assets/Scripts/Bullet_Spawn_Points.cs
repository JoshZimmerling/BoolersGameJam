using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ship_Shooting;

public class Bullet_Spawn_Points : MonoBehaviour
{

    [SerializeField] shotDirection Bullet_Direction;

    public shotDirection GetDirection() 
    { 
        return Bullet_Direction;
    }
}
