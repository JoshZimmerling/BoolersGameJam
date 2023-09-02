using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Shooting : MonoBehaviour
{

    Ship.shipTypes typeOfShip;

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
        switch(typeOfShip)
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
        GameObject bullet1 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 0.5f), new Quaternion());
        bullet1.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 1) * bulletSpeed);
        Destroy(bullet1, bulletLifetime);

        //Bullet going down
        GameObject bullet2 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x, this.transform.position.y - 0.5f), new Quaternion());
        bullet2.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, -1) * bulletSpeed);
        Destroy(bullet2, bulletLifetime);

        //Bullets going right
        GameObject bullet3 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y + 0.25f), new Quaternion());
        bullet3.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet3, bulletLifetime);
        GameObject bullet4 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y - 0.25f), new Quaternion());
        bullet4.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet4, bulletLifetime);

        //Bullets going left
        GameObject bullet5 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y + 0.25f), new Quaternion());
        bullet5.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, 0) * bulletSpeed);
        Destroy(bullet5, bulletLifetime);
        GameObject bullet6 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y - 0.25f), new Quaternion());
        bullet6.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, 0) * bulletSpeed);
        Destroy(bullet6, bulletLifetime);
    }
    private void shoot_hawk()
    {
        //Bullet going up
        GameObject bullet1 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 0.5f), new Quaternion());
        bullet1.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 1) * bulletSpeed);
        Destroy(bullet1, bulletLifetime);
    }
    private void shoot_challenger()
    {
        //Bullets going right
        GameObject bullet1 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y + 0.4f), new Quaternion());
        bullet1.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet1, bulletLifetime);
        GameObject bullet2 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y + 0.2f), new Quaternion());
        bullet2.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet2, bulletLifetime);
        GameObject bullet3 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y), new Quaternion());
        bullet3.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet3, bulletLifetime);
        GameObject bullet4 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y - 0.2f), new Quaternion());
        bullet4.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet4, bulletLifetime);
        GameObject bullet5 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y - 0.4f), new Quaternion());
        bullet5.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet5, bulletLifetime);
    }
    private void shoot_goliath()
    {
        //Bullets going right
        GameObject bullet1 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y + 0.3f), new Quaternion());
        bullet1.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet1, bulletLifetime);
        GameObject bullet2 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y + 0.1f), new Quaternion());
        bullet2.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet2, bulletLifetime);
        GameObject bullet3 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y - 0.1f), new Quaternion());
        bullet3.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet3, bulletLifetime);
        GameObject bullet4 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y - 0.3f), new Quaternion());
        bullet4.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
        Destroy(bullet4, bulletLifetime);

        //Bullets going left
        GameObject bullet5 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y + 0.3f), new Quaternion());
        bullet5.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, 0) * bulletSpeed);
        Destroy(bullet5, bulletLifetime);
        GameObject bullet6 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y + 0.1f), new Quaternion());
        bullet6.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, 0) * bulletSpeed);
        Destroy(bullet6, bulletLifetime);
        GameObject bullet7 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y - 0.1f), new Quaternion());
        bullet7.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, 0) * bulletSpeed);
        Destroy(bullet7, bulletLifetime);
        GameObject bullet8 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y - 0.3f), new Quaternion());
        bullet8.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, 0) * bulletSpeed);
        Destroy(bullet8, bulletLifetime);
    }
    private void shoot_lightning()
    {
        //Bullets going up
        GameObject bullet1 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.25f, this.transform.position.y + 0.5f), new Quaternion());
        bullet1.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 1) * bulletSpeed);
        Destroy(bullet1, bulletLifetime);
        GameObject bullet2 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x - 0.25f, this.transform.position.y + 0.5f), new Quaternion());
        bullet2.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 1) * bulletSpeed);
        Destroy(bullet2, bulletLifetime);
    }
    private void shoot_drone()
    {
        //Bullet going up
        if(bulletsShotCounter_Drone == 0)
        {
            GameObject bullet1 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 0.5f), new Quaternion());
            bullet1.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 1) * bulletSpeed);
            Destroy(bullet1, bulletLifetime);
        }

        //Bullet going down
        if (bulletsShotCounter_Drone == 2)
        {
            GameObject bullet2 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x, this.transform.position.y - 0.5f), new Quaternion());
            bullet2.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, -1) * bulletSpeed);
            Destroy(bullet2, bulletLifetime);
        }

        //Bullet going right
        if (bulletsShotCounter_Drone == 1)
        {
            GameObject bullet3 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y), new Quaternion());
            bullet3.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);
            Destroy(bullet3, bulletLifetime);
        }

        //Bullet going left
        if (bulletsShotCounter_Drone == 3)
        {
            GameObject bullet4 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y), new Quaternion());
            bullet4.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1, 0) * bulletSpeed);
            Destroy(bullet4, bulletLifetime);
        }
    }
    private void shoot_scout()
    {
        //Bullet going up
        GameObject bullet1 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 0.5f), new Quaternion());
        bullet1.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 1) * bulletSpeed);
        Destroy(bullet1, bulletLifetime);

        //Bullet going down
        GameObject bullet2 = Instantiate(BulletPrefab, new Vector2(this.transform.position.x, this.transform.position.y - 0.5f), new Quaternion());
        bullet2.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, -1) * bulletSpeed);
        Destroy(bullet2, bulletLifetime);
    }
}
