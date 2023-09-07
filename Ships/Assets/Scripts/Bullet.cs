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
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyShip"))
            collision.GetComponent<Ship>().DoDamage(dmg); // Shooting itself rn.. need to deal with bullets differently

        GetComponent<NetworkObject>().Despawn();
        Destroy(this.gameObject);
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
