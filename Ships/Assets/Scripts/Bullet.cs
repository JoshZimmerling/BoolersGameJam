using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float dmg;
    int parentPlayerNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ensures not colliding with another bullet
        if(collision.GetComponent<Bullet>() == null)
        {
            if(collision.GetComponent<Ship>() != null)
            {
                if(collision.GetComponent<Ship>().getPlayerNum() != parentPlayerNum)
                {
                    collision.GetComponent<Ship>().doDamage(dmg);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void setDamage(float damage)
    {
        dmg = damage;
    }

    public void setParentPlayerNum(int num)
    {
        parentPlayerNum = num;
    }
}
