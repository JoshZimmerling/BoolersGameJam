using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_ShootingTemp : MonoBehaviour
{
    public enum shipType
    {
        Ship_A,
        Ship_B
    }
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifetime;
    [SerializeField] float bulletDamage;
    [Range(0.0f, 25.0f)] public float bulletsPerSecond;

    [SerializeField] GameObject BulletObject;

    // Start is called before the first frame update
    void Start()
    {
        
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
        GameObject bullet = Instantiate(BulletObject, this.transform.position, new Quaternion());
        bullet.GetComponent<Rigidbody2D>().velocity = (new Vector2(1, 0) * bulletSpeed);

        Destroy(bullet, bulletLifetime);
    }
}
