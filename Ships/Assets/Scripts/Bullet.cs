using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float dmg;

    [SerializeField] GameObject BulletObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createBullet(float spd, float time, float damage, Vector2 velo)
    {

        GameObject bullet = Instantiate(BulletObject);
        bullet.GetComponent<Rigidbody>().velocity = (velo * spd);

        Destroy(this.gameObject, time);
    }
}
