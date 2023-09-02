using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    float bulletSpeed = 1;
    float bulletLifetime = 1;
    float bulletDamage;

    [SerializeField] GameObject BulletObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet = Instantiate(BulletObject);
            bullet.GetComponent<Rigidbody>().velocity = (new Vector2(1, 0) * bulletSpeed);

            Destroy(this.gameObject, bulletLifetime);
        }
    }
}
