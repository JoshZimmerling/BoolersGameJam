using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Ship_Shooting : NetworkBehaviour
{

    Ship.shipTypes typeOfShip;

    float bulletLifetime; 
    float bulletSpeed;
    float bulletsPerSecond;
    float bulletDamage;

    int bulletsShotCounter_Drone = 0;

    [SerializeField] GameObject BulletPrefab;
    [SerializeField] List<GameObject> spawnPointList;

    public enum ShotDirection
    {
        Right,
        Left,
        Up,
        Down
    }

    public override void OnNetworkSpawn()
    {
        
        typeOfShip = this.gameObject.GetComponent<Ship>().GetShipType();

        Debug.Log(typeOfShip);

        switch (typeOfShip)
        {
            case (Ship.shipTypes.Destroyer):
                bulletLifetime = 2f;
                bulletSpeed = 8f;
                bulletsPerSecond = 1f;
                bulletDamage = 1.5f;
                break;
            case (Ship.shipTypes.Hawk):
                bulletLifetime = 1.75f;
                bulletSpeed = 12f;
                bulletsPerSecond = 1f;
                bulletDamage = 10f;
                break;
            case (Ship.shipTypes.Challenger):
                bulletLifetime = 0.75f;
                bulletSpeed = 12f;
                bulletsPerSecond = 0.75f;
                bulletDamage = 3f;
                break;
            case (Ship.shipTypes.Goliath):
                bulletLifetime = 2f;
                bulletSpeed = 8f;
                bulletsPerSecond = 1f;
                bulletDamage = 1f;
                break;
            case (Ship.shipTypes.Lightning):
                bulletLifetime = 2f;
                bulletSpeed = 10f;
                bulletsPerSecond = 0.5f;
                bulletDamage = 10f;
                break;
            case (Ship.shipTypes.Drone):
                bulletLifetime = 0.75f;
                bulletSpeed = 12f;
                bulletsPerSecond = 2f;
                bulletDamage = 1f;
                break;
            case (Ship.shipTypes.Scout):
                bulletLifetime = 1.0f;
                bulletSpeed = 16f;
                bulletsPerSecond = 0.25f;
                bulletDamage = 4f;
                break;
        }
    }

    int counter = 0;
    void FixedUpdate()
    {
        if (!IsHost)
           return;

        counter++;
        if (counter >= 50f / bulletsPerSecond)
        {
            ShootBullet();
            counter = 0;
        }
    }

    private void ShootBullet()
    {
        if (spawnPointList.Count != 0)
        {
            if (typeOfShip == Ship.shipTypes.Drone)
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
            else
            {
                for (int i = 0; i < spawnPointList.Count; i++)
                {
                    CreateBullet(spawnPointList[i].GetComponent<Bullet_Spawn_Points>().GetDirection(), spawnPointList[i]);
                }
            }
        }
    }

    private void CreateBullet(ShotDirection dir, GameObject spawnPoint)
    {
        GameObject bullet = Instantiate(BulletPrefab, spawnPoint.transform.position + new Vector3(0, 0, -1), new Quaternion());
        switch (dir)
        {
            case ShotDirection.Right:
                bullet.GetComponent<Rigidbody2D>().velocity = (transform.rotation * new Vector2(1, 0) * bulletSpeed);
                break;
            case ShotDirection.Left:
                bullet.GetComponent<Rigidbody2D>().velocity = (transform.rotation * new Vector2(-1, 0) * bulletSpeed);
                break;
            case ShotDirection.Up:
                bullet.GetComponent<Rigidbody2D>().velocity = (transform.rotation * new Vector2(0, 1) * bulletSpeed);
                break;
            case ShotDirection.Down:
                bullet.GetComponent<Rigidbody2D>().velocity = (transform.rotation * new Vector2(0, -1) * bulletSpeed);
                break;
        }
        bullet.GetComponent<Bullet>().SetDamage(bulletDamage);
        bullet.GetComponent<Bullet>().SetBulletLifetime(bulletLifetime);
        
        bullet.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
    }
}
