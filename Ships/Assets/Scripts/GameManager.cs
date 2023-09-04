using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Singleton;

    [SerializeField]
    public PlayerController[] players = new PlayerController[8];
    [SerializeField]
    public Color[] playerColors = new Color[8]; //TODO: Make it so you can pick colors
    [SerializeField]
    public GameObject[] playerSpawns = new GameObject[8];
    [SerializeField]
    public GameObject[] shipPrefabs = new GameObject[7];

    public Shop shop;

    public int playerIndex = 0;

    void Start()
    {
        Singleton = this;
    }

    public void StartGame(int playerID)
    {
        playerIndex = playerID;

        Debug.Log("WASD/Mouse - Move Camera\n" +
                  "Scroll Wheel - Zoom Camera\n" +
                  "E - Toggle Camera Lock\n" +
                  "Left Click - Select Ships\n" +
                  "Right Click - Move Selected Ships\n" +
                  "Q - Stop Selected Ships\n" +
                  "Z - Move Selected Ships Backwards\n" +
                  "R - Open Shop\n");

        GameManager.Singleton.shop.SetupShop(players[playerIndex]);

        // Set camera to spawn platform
        Camera.main.transform.position = new Vector3(playerSpawns[playerIndex].transform.position.x, playerSpawns[playerIndex].transform.position.y, Camera.main.transform.position.z);
    }

    public void AddPlayer(PlayerController player)
    {
        players[player.OwnerClientId] = player;
        playerSpawns[player.OwnerClientId].SetActive(true);
        playerSpawns[player.OwnerClientId].GetComponent<SpriteRenderer>().color = playerColors[player.OwnerClientId]; // TODO: Set spawns to white
    }
}
