using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
        switch (typeOfShip)
        {
            case (Ship.shipTypes.Destroyer):
                shoot_destroyer();
                break;

            case (Ship.shipTypes.Hawk):
                shoot_hawk();
                break;

            case (Ship.shipTypes.Challenger):
                shoot_challenger();
                break;

            case (Ship.shipTypes.Goliath):
                shoot_goliath();
                break;

            case (Ship.shipTypes.Lightning):
                shoot_lightning();
                break;

            case (Ship.shipTypes.Drone):
                shoot_drone();
                bulletsShotCounter_Drone++;
                if(bulletsShotCounter_Drone == 4)
                {
                    bulletsShotCounter_Drone = 0;
                }
                break;

            case (Ship.shipTypes.Scout):
                shoot_scout();
                break;
        }
    }

    private void shoot_destroyer()
    {
        //Bullet going up
        createBullet(0f, 1f, 0, 1);

        //Bullet going down
        createBullet(0f, -1f, 0, -1);

        //Bullets going right
        createBullet(0.25f, 0.25f, 1, 0);
        createBullet(0.25f, -0.25f, 1, 0);

        //Bullets going left
        createBullet(-0.25f, 0.25f, -1, 0);
        createBullet(-0.25f, -0.25f, -1, 0);
    }
    private void shoot_hawk()
    {
        //Bullet going up
        createBullet(0f, 0.5f, 0, 1);
    }
    private void shoot_challenger()
    {
        //Bullets going right
        createBullet(0.5f, 0.4f, 1, 0);
        createBullet(0.5f, 0.2f, 1, 0);
        createBullet(0.5f, 0f, 1, 0);
        createBullet(0.5f, -0.2f, 1, 0);
        createBullet(0.5f, -0.4f, 1, 0);
    }
    private void shoot_goliath()
    {
        //Bullets going right
        createBullet(0.5f, 0.3f, 1, 0);
        createBullet(0.5f, 0.1f, 1, 0);
        createBullet(0.5f, -0.1f, 1, 0);
        createBullet(0.5f, -0.3f, 1, 0);

        //Bullets going left
        createBullet(-0.5f, 0.3f, -1, 0);
        createBullet(-0.5f, 0.1f, -1, 0);
        createBullet(-0.5f, -0.1f, -1, 0);
        createBullet(-0.5f, -0.3f, -1, 0);
    }
    private void shoot_lightning()
    {
        //Bullets going up
        createBullet(0.25f, 0.5f, 0, 1);
        createBullet(-0.25f, 0.5f, 0, 1);
    }
    private void shoot_drone()
    {
        //Bullet going up
        if(bulletsShotCounter_Drone == 0)
            createBullet(0f, 0.5f, 0, 1);

        //Bullet going down
        if (bulletsShotCounter_Drone == 2)
            createBullet(0f, -0.5f, 0, -1);

        //Bullet going right
        if (bulletsShotCounter_Drone == 1)
            createBullet(0.5f, 0f, 1, 0);

        //Bullet going left
        if (bulletsShotCounter_Drone == 3)
            createBullet(-0.5f, 0f, -1, 0);
    }
    private void shoot_scout()
    {
        //Bullet going up
        createBullet(0f, 0.5f, 0, 1);

        //Bullet going down
        createBullet(0f, -0.5f, 0, -1);
    }

    private void createBullet(float xOffset, float yOffset, int xVelo, int yVelo)
    {
        GameObject bullet = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + xOffset, this.transform.position.y + yOffset), new Quaternion());
        //bullet.GetComponent<Rigidbody2D>().velocity = (transform.rotation * new Vector2(xVelo, yVelo) * bulletSpeed);
        bullet.GetComponent<Bullet>().setDamage(bulletDamage);
        bullet.GetComponent<Bullet>().setParentPlayerNum(parentPlayerNum);
        if(parentPlayerNum == 1)
            bullet.GetComponent<SpriteRenderer>().color = Color.red;
        else if(parentPlayerNum == 2)
            bullet.GetComponent<SpriteRenderer>().color = Color.blue;
        else
            bullet.GetComponent<SpriteRenderer>().color = Color.black;
        bullet.GetComponent<NetworkObject>().Spawn(true);
        Destroy(bullet, bulletLifetime);
    }
}
