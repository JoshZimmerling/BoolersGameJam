using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShipShooting : NetworkBehaviour
{
    [SerializeField] private float bulletLifetime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletsPerSecond;
    [SerializeField] private float bulletDamage;

    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] protected List<GameObject> spawnPointList; // TODO: Make this easier to work with

    public enum ShotDirection
    {
        Right,
        Up,
        Left,
        Down
    }

    float counter = 0;
    public void FixedUpdate()
    {
        if (!IsHost) return;

        counter += Time.deltaTime;
        if (counter >= 1 / bulletsPerSecond)
        {
            ShootBullet();
            counter = 0;
        }
    }

    protected virtual void ShootBullet()
    {
        if (spawnPointList.Count == 0) return;

        for (int i = 0; i < spawnPointList.Count; i++)
        {
            CreateBullet(spawnPointList[i].GetComponent<Bullet_Spawn_Points>().GetDirection(), spawnPointList[i]);
        }
    }

    protected void CreateBullet(ShotDirection dir, GameObject spawnPoint) // TODO: something better than shot direction and a constructor or somthing for the bullets
    {
        GameObject bullet = Instantiate(BulletPrefab, spawnPoint.transform.position + new Vector3(0, 0, -1), Quaternion.Euler(0, 0, 90 * (int)dir + transform.rotation.eulerAngles.z));
        bullet.GetComponent<Bullet>().SetDamage(bulletDamage);
        bullet.GetComponent<Bullet>().SetBulletLifetime(bulletLifetime);
        bullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);

        bullet.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
    }
}
