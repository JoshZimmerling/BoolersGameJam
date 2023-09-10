using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] RectTransform buttonContainer;
    private bool shopOpen = false;

    PlayerController playerScript;

    public void SetupShop(PlayerController player)
    {
        playerScript = player;

        transform.Find("Toggle Shop Button").GetComponent<Button>().onClick.AddListener(() => ToggleShop()); ;

        Color playerColor = GameManager.Singleton.playerColors[playerScript.OwnerClientId];
        foreach (NetworkPrefab prefab in GameManager.Singleton.shipList.PrefabList)
        {
            Transform shipPrefab = prefab.Prefab.transform;
            Transform button = Instantiate(buttonPrefab, buttonContainer).transform;
            button.Find("Ship Name").GetComponent<TMP_Text>().text = shipPrefab.GetComponent<Ship>().GetShipType().ToString();
            button.Find("Ship Sprite").GetComponent<Image>().sprite = shipPrefab.GetComponent<SpriteRenderer>().sprite;
            button.Find("Ship Color").GetComponent<Image>().sprite = shipPrefab.Find("Ship Accent").GetComponent<SpriteRenderer>().sprite;
            button.Find("Ship Color").GetComponent<Image>().color = playerColor;
            float shipCost = shipPrefab.GetComponent<Ship>().GetShipCost();
            button.Find("Ship Cost").GetComponent<TMP_Text>().text = "$: " + shipCost;

            button.GetComponent<Button>().onClick.AddListener(() => BuyShip(shipPrefab.GetComponent<Ship>().GetShipType(), shipCost));
        }
    }

    float playerGold = 200f;

    private void BuyShip(Ship.ShipTypes type, float cost)
    {
        if (playerGold >= cost)
        {
            playerGold -= cost;
            GameManager.Singleton.players[GameManager.Singleton.playerIndex].SpawnShipServerRPC(type, playerScript.OwnerClientId);
        }
    }

    public void ToggleShop()
    {
        shopOpen = !shopOpen;
        if (shopOpen)
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        else
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 0);
    }
}
