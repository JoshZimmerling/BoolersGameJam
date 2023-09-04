using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameLogic : NetworkBehaviour
{
    List<Ship> player1Ships;
    List<Ship> player2Ships;

    [SerializeField] PlayerController player1Controller;
    [SerializeField] PlayerController player2Controller;

    PlayerController[] players;

    public override void OnNetworkSpawn()
    {
        player1Ships = new List<Ship>();
        player2Ships = new List<Ship>();    

        player1Controller = gameObject.transform.GetComponent<PlayerController>();
        player2Controller = null;

        players = new PlayerController[2];

    }

    private void FixedUpdate()
    {
        if (!IsHost) { return; }
        
        if (player2Controller == null)
        {
            players = FindObjectsOfType<PlayerController>();

            if (players.Length > 1) 
            {
                foreach (PlayerController player in players)
                {
                    if (player.GetComponent<NetworkObject>().OwnerClientId == 1)
                    {
                        player2Controller = player;
                    }
                }
            }
        }
    }



}
