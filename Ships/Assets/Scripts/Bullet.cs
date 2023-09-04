using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    float dmg;
    float maxbulletLifetime;
    float bulletSpeed;

    public override void OnNetworkSpawn()
    {
        GetComponent<SpriteRenderer>().color = GameManager.Singleton.playerColors[OwnerClientId];
    }

    float bulletLifetime = 0;
    void FixedUpdate()
    {
        if (!IsHost)
            return;

        transform.Translate(new Vector3(1, 0) * bulletSpeed * Time.deltaTime);
        Debug.Log(new Vector3(1, 0) * bulletSpeed * Time.deltaTime);
        bulletLifetime += Time.deltaTime;

        if (bulletLifetime > maxbulletLifetime)
        {
            GetComponent<NetworkObject>().Despawn();
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsHost)
            return;

        //Ensures collision with enemy ship
        if (collision.GetComponent<Bullet>() == null &&
            collision.GetComponent<Ship>() != null &&
            collision.GetComponent<Ship>().OwnerClientId != OwnerClientId)
        {
            collision.GetComponent<Ship>().DoDamage(dmg);
            GetComponent<NetworkObject>().Despawn();
            Destroy(this.gameObject);
        }
    }

    public void SetBulletLifetime(float lifetime)
    {
        maxbulletLifetime = lifetime;
    }

    public void SetDamage(float damage)
    {
        dmg = damage;
    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }
}
