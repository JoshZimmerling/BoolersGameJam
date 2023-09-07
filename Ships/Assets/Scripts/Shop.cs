using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Button destroyer_button;
    [SerializeField] Button hawk_button;
    [SerializeField] Button challenger_button;
    [SerializeField] Button goliath_button;
    [SerializeField] Button lightning_button;
    [SerializeField] Button drone_button;
    [SerializeField] Button scout_button;

    [SerializeField] GameObject goldTextObject;
    Text goldText;
    float playerGold = 200f;

    PlayerController playerScript;

    public void SetupShop(PlayerController player)
    {

        playerScript = player;

        this.gameObject.SetActive(false);

        goldText = goldTextObject.GetComponent<Text>();
        UpdateGoldText();

        destroyer_button.onClick.AddListener(() => BuyShip(Ship.ShipTypes.Destroyer, 20));
        hawk_button.onClick.AddListener(() => BuyShip(Ship.ShipTypes.Hawk, 20));
        challenger_button.onClick.AddListener(() => BuyShip(Ship.ShipTypes.Challenger, 20));
        goliath_button.onClick.AddListener(() => BuyShip(Ship.ShipTypes.Goliath, 35));
        lightning_button.onClick.AddListener(() => BuyShip(Ship.ShipTypes.Lightning, 35));
        drone_button.onClick.AddListener(() => BuyShip(Ship.ShipTypes.Drone, 5));
        scout_button.onClick.AddListener(() => BuyShip(Ship.ShipTypes.Scout, 10));
    }

    private void BuyShip(Ship.ShipTypes type, float cost)
    {
        if (playerGold >= cost)
        {
            playerGold -= cost;
            UpdateGoldText();
            GameManager.Singleton.players[GameManager.Singleton.playerIndex].SpawnShipServerRPC(type, playerScript.OwnerClientId);
        }
    }

    public void OpenShop()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    public void UpdateGoldText()
    {
        goldText.text = "Gold: $" + playerGold;
    }

    
}
