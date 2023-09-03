using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Ship_Shooting : NetworkBehaviour
{

    Ship.shipTypes typeOfShip;
    int parentPlayerNum;

    float bulletLifetime; 
    float bulletSpeed;
    float bulletsPerSecond;
    float bulletDamage;

    int bulletsShotCounter_Drone = 0;

    [SerializeField] GameObject BulletPrefab;
    [SerializeField] List<GameObject> spawnPointList;

    public enum shotDirection
    {
        Right,
        Left,
        Up,
        Down
    }

    // Start is called before the first frame update
    void Start()
    {
        typeOfShip = this.gameObject.GetComponent<Ship>().getShipType();
        parentPlayerNum = this.gameObject.GetComponent<Ship>().getPlayerNum();

        switch (typeOfShip)
        {
            case (Ship.shipTypes.Destroyer):
                bulletLifetime = 2f;
                bulletSpeed = 8f;
                bulletsPerSecond = 2f;
                bulletDamage = 2f;
                break;
            case (Ship.shipTypes.Hawk):
                bulletLifetime = 2f;
                bulletSpeed = 8f;
                bulletsPerSecond = 1f;
                bulletDamage = 7f;
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
                bulletSpeed = 8f;
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
                bulletLifetime = 0.2f;
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
            shootBullet();
            counter = 0;
        }
    }

    private void shootBullet()
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
                    createBullet(shotDirection.Up, spawnPointList[0]);

                //Bullet going right
                if (bulletsShotCounter_Drone == 1)
                    createBullet(shotDirection.Right, spawnPointList[1]);

                //Bullet going down
                if (bulletsShotCounter_Drone == 2)
                    createBullet(shotDirection.Down, spawnPointList[2]);

                //Bullet going left
                if (bulletsShotCounter_Drone == 3)
                    createBullet(shotDirection.Left, spawnPointList[3]);
            }
            else
            {
                for (int i = 0; i < spawnPointList.Count; i++)
                {
                    createBullet(spawnPointList[i].GetComponent<Bullet_Spawn_Points>().GetDirection(), spawnPointList[i]);
                }
            }
        }
    }

    private void createBullet(shotDirection dir, GameObject spawnPoint)
    {
        GameObject bullet = Instantiate(BulletPrefab, spawnPoint.transform.position + new Vector3(0, 0, -1), new Quaternion());
        switch (dir)
        {
            case shotDirection.Right:
                bullet.GetComponent<Rigidbody2D>().velocity = (transform.rotation * new Vector2(1, 0) * bulletSpeed);
                break;
            case shotDirection.Left:
                bullet.GetComponent<Rigidbody2D>().velocity = (transform.rotation * new Vector2(-1, 0) * bulletSpeed);
                break;
            case shotDirection.Up:
                bullet.GetComponent<Rigidbody2D>().velocity = (transform.rotation * new Vector2(0, 1) * bulletSpeed);
                break;
            case shotDirection.Down:
                bullet.GetComponent<Rigidbody2D>().velocity = (transform.rotation * new Vector2(0, -1) * bulletSpeed);
                break;
        }
        bullet.GetComponent<Bullet>().setDamage(bulletDamage);
        bullet.GetComponent<Bullet>().setBulletLifetime(bulletLifetime);
        
        bullet.GetComponent<NetworkObject>().Spawn(true);
        bullet.GetComponent<Bullet>().setParentPlayerNum(parentPlayerNum);
    }
}
