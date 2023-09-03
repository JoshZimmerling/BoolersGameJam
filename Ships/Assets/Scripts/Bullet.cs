using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    float dmg;
    float maxbulletLifetime;
    NetworkVariable<int> parentPlayerNum = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        parentPlayerNum.OnValueChanged += (int previousValue, int newValue) =>
        {
            if (newValue == 1)
                GetComponent<SpriteRenderer>().color = Color.red;
            else if (newValue == 2)
                GetComponent<SpriteRenderer>().color = Color.blue;
            else
                GetComponent<SpriteRenderer>().color = Color.black;
        };
    }

    float bulletLifetime = 0;
    void FixedUpdate()
    {
        if (!IsHost)
            return;

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

        //Ensures not colliding with another bullet
        if (collision.GetComponent<Bullet>() == null)
        {
            if(collision.GetComponent<Ship>() != null)
            {
                if(collision.GetComponent<Ship>().getPlayerNum() != parentPlayerNum.Value)
                {
                    collision.GetComponent<Ship>().doDamage(dmg);
                    GetComponent<NetworkObject>().Despawn();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void setBulletLifetime(float lifetime)
    {
        maxbulletLifetime = lifetime;
    }

    public void setDamage(float damage)
    {
        dmg = damage;
    }

    public void setParentPlayerNum(int num)
    {
        parentPlayerNum.Value = num;
    }
}
