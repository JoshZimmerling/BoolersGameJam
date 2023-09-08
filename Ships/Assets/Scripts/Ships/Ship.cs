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
    [SerializeField] private float shipCost;

    [SerializeField] private float maxShipHP;
    private NetworkVariable<float> currentShipHP = new NetworkVariable<float>();

    // Ship Components
    private Transform hpBar;
    private SpriteRenderer outlineSprite;

    public override void OnNetworkSpawn()
    {
        // Finding ship components
        hpBar = transform.Find("HealthBar/Health");
        outlineSprite = transform.Find("Outline").GetComponent<SpriteRenderer>();
        SpriteRenderer accentsSprite = transform.Find("ShipAccent").GetComponent<SpriteRenderer>();
        SpriteRenderer mapMarkerSprite = transform.Find("MapMarker").GetComponent<SpriteRenderer>();


        // Setting up healthbar
        if (IsHost) currentShipHP.Value = maxShipHP;

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
}
