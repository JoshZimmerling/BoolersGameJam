using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Drone : Ship
{
    int bulletsShotCounter_Drone = 0;
    protected override void ShootBullet()
    {
        bulletsShotCounter_Drone++;
        if (bulletsShotCounter_Drone == 4)
        {
            bulletsShotCounter_Drone = 0;
        }

        //Bullet going up
        if (bulletsShotCounter_Drone == 0)
            CreateBullet(ShotDirection.Up, spawnPointList[0]);

        //Bullet going right
        if (bulletsShotCounter_Drone == 1)
            CreateBullet(ShotDirection.Right, spawnPointList[1]);

        //Bullet going down
        if (bulletsShotCounter_Drone == 2)
            CreateBullet(ShotDirection.Down, spawnPointList[2]);

        //Bullet going left
        if (bulletsShotCounter_Drone == 3)
            CreateBullet(ShotDirection.Left, spawnPointList[3]);
    }
}
