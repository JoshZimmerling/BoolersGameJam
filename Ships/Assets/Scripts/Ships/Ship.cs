using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Netcode;
using UnityEngine;
using System;

public class Ship : NetworkBehaviour
{
    public enum ShipTypes
    {
        Destroyer,
        Hawk,
        Challenger,
        Goliath,
        Lightning,
        Drone,
        Scout
    }

    // Ship Variables
    [SerializeField] ShipTypes shipType;
    [SerializeField] private float shipCost;

    [SerializeField] private float maxShipHP;
    private NetworkVariable<float> currentShipHP = new NetworkVariable<float>();

    [SerializeField] private float shipAcceleration;
    [SerializeField] private float shipMaxSpeed;
    [SerializeField] private float shipTurnRate;

    // Ship Components
    [SerializeField] private GameObject hpBar;

    [SerializeField] private SpriteRenderer accentsSprite;
    [SerializeField] private SpriteRenderer mapMarkerSprite;
    [SerializeField] private SpriteRenderer outlineSprite;

    public override void OnNetworkSpawn()
    {
        // Set maxHP
        if (IsHost) currentShipHP.Value = maxShipHP;

        // Update healthbar for both players when it changes
        currentShipHP.OnValueChanged += (float previousValue, float newValue) => {
            hpBar.transform.localScale = new Vector3(currentShipHP.Value / maxShipHP, 1, 1);
            hpBar.transform.localPosition = new Vector3((currentShipHP.Value / maxShipHP * 0.5f) - 0.5f, 0, 0);
        };

        // Changes based on ship owner
        if (!IsOwner) {
            GetComponentInChildren<SpriteMask>().enabled = false;
            outlineSprite.gameObject.SetActive(false);
            mapMarkerSprite.gameObject.SetActive(true);
        }

        // Set the team color
        Color teamColor = GameManager.Singleton.playerColors[OwnerClientId];
        accentsSprite.color = teamColor;
        mapMarkerSprite.color = teamColor;
        teamColor.a = 0f;
        outlineSprite.color = teamColor;
    }

    public void FixedUpdate()
    {
        if (!IsHost) return;

        ShipMovement();
        ShootingUpdate();
    }

    public void DoDamage(float damage)
    {
        currentShipHP.Value -= damage;
        if (currentShipHP.Value <= 0)
        {
            this.GetComponent<NetworkObject>().Despawn();
            Destroy(this.gameObject);
        }
    }


    // ======================= Outline Functions =========================
    #region
    public void Select()
    {
        Color newColor = outlineSprite.color;
        newColor.a = 1f;
        outlineSprite.color = newColor;
    }

    public void Unselect()
    {
        Color newColor = outlineSprite.color;
        newColor.a = 0f;
        outlineSprite.color = newColor;
    }
    #endregion
    // ========================= Ship Movement ===========================
    #region
    // TODO pub for tests
    // turn rate, stopping time, max speed, acceleration, 

    float angle;
    float totalVelocity;
    float distToTarget;
    float timeToStop;
    float brakeTimer;

    Vector2 targetPos;
    Vector2 track;

    bool noTarget = true;
    bool moving;
    bool backingUp;

    [SerializeField] float distToStop = 2;

    /*    LineRenderer lineRenderer;*/

    private void ShipMovement()
    {
        track = targetPos - (Vector2)transform.position;
        angle = Vector2.SignedAngle(track, transform.up);

        //path = (Vector2)transform.position - targetPos;
        distToTarget = Vector2.Distance(transform.position, targetPos);

        //up = transform.up;

        if (noTarget) { return; }

        // Stopping
        if (distToTarget < 0.1)
        {
            noTarget = true;
            totalVelocity = 0;
            moving = false;
            backingUp = false;
            return;
        }

        if (!backingUp)
        {
            // Turning
            if (MathF.Abs(angle) > 10)
            {
                if (angle > 0)
                {
                    transform.Rotate(0, 0, -shipTurnRate * Time.deltaTime);
                }
                else
                {
                    transform.Rotate(0, 0, shipTurnRate * Time.deltaTime);
                }
            }
            // Slowing turns
            else if (MathF.Abs(angle) > 1)
            {
                if (angle > 0)
                {
                    transform.Rotate(0, 0, (-10 - (Mathf.Abs(angle) * 3)) * Time.deltaTime);
                }
                else
                {
                    transform.Rotate(0, 0, (10 + (Mathf.Abs(angle) * 3)) * Time.deltaTime);
                }
            }
            // If the angle is small enough, will lock towards target
            else
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos - (Vector2)transform.position);
            }

            // Prevents moving the ship if not moving and too high an angle
            if (Mathf.Abs(angle) > 45 && !moving)
            {
                return;
            }

            moving = true;

            if (distToTarget < distToStop)
            {
                transform.Translate(Vector2.up * distToTarget * Time.deltaTime);
            }
            else
            {
                totalVelocity += shipAcceleration;
                if (totalVelocity > shipMaxSpeed)
                {
                    totalVelocity = shipMaxSpeed;
                }
                transform.Translate(Vector2.up * totalVelocity * Time.deltaTime);
            }
        }
        else // backing up
        {
            if (distToTarget < distToStop)
            {
                transform.Translate(-Vector2.up * distToTarget * Time.deltaTime);
            }
            else
            {
                totalVelocity += shipAcceleration;
                if (totalVelocity > shipMaxSpeed)
                {
                    totalVelocity = shipMaxSpeed;
                }
                transform.Translate(-Vector2.up * totalVelocity * Time.deltaTime);
            }
        }

    }

    [ServerRpc]
    public void BackupServerRPC()
    {
        targetPos = transform.position + (-transform.up * distToStop);
        backingUp = true;
        noTarget = false;
    }

    [ServerRpc]
    public void StopShipServerRPC()
    {
        targetPos = transform.position + transform.up * distToStop;
    }

    [ServerRpc]
    public void SetTargetDestinationServerRPC(Vector2 target)
    {
        noTarget = false;
        backingUp = false;
        targetPos = target;
    }
    #endregion
    // ========================= Ship Shooting ===========================
    #region
    [SerializeField] private float bulletLifetime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletsPerSecond;
    [SerializeField] private float bulletDamage;

    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] protected List<GameObject> spawnPointList;

    public enum ShotDirection
    {
        Right,
        Up,
        Left,
        Down
    }

    float counter = 0;
    private void ShootingUpdate()
    {
        counter += Time.deltaTime;
        if (counter >= 1/bulletsPerSecond)
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

    protected void CreateBullet(ShotDirection dir, GameObject spawnPoint)
    {
        GameObject bullet = Instantiate(BulletPrefab, spawnPoint.transform.position + new Vector3(0, 0, -1), Quaternion.Euler(0, 0, 90 * (int)dir + transform.rotation.eulerAngles.z));
        bullet.GetComponent<Bullet>().SetDamage(bulletDamage);
        bullet.GetComponent<Bullet>().SetBulletLifetime(bulletLifetime);
        bullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);

        bullet.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
    }
    #endregion
}
