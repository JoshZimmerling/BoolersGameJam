using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] List<Ship> selectedShips;
    Camera_Control cameraScript;

    public override void OnNetworkSpawn()
    {
        GameManager.Singleton.AddPlayer(this);
        selectedShips = new List<Ship>();
        cameraScript = Camera.main.GetComponent<Camera_Control>();

        if (IsOwner)
            GameManager.Singleton.StartGame((int)OwnerClientId);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
            return;

        // Setting the target destination for the ships
        if (Input.GetButtonDown("Fire2"))
        {

            VerifySelection();

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            if (selectedShips.Count == 1)
            {
                selectedShips[0].SetTargetDestinationServerRPC(worldPosition);
            }
            else
            {
                SetDestinationInFormation(); 
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Ship ship in selectedShips)
            {
                ship.StopShipServerRPC(); 
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (Ship ship in selectedShips)
            {
                ship.BackupServerRPC();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Singleton.shop.OpenShop();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            cameraScript.toggleLockState();
        }
    }

    private void VerifySelection()
    {
        for (int i = 0; i < selectedShips.Count; i++) 
        {
            if (selectedShips[i] == null) 
            {
                selectedShips.RemoveAt(i);
            }
        }
    }

    // Ship movement / selection
    float xMax;
    float yMax;
    float xMin;
    float yMin;
    float xDiff;
    float yDiff;
    Vector2 shipCenter;
    public void SetDestinationInFormation()
    {
        if (selectedShips.Count == 0) 
        {
            return;
        }
        else 
        {
            xMax = selectedShips[0].transform.position.x;
            yMax = selectedShips[0].transform.position.y;
            xMin = selectedShips[0].transform.position.x;
            yMin = selectedShips[0].transform.position.y;
        }

        foreach (Ship ship in selectedShips)
        {
            if (ship.transform.position.x > xMax) { xMax = ship.transform.position.x; }
            if (ship.transform.position.x < xMin) { xMin = ship.transform.position.x; }
            if (ship.transform.position.y > yMax) { yMax = ship.transform.position.y; }
            if (ship.transform.position.y < yMin) { yMin = ship.transform.position.y; }
        }

        xDiff = xMax - xMin;
        yDiff = yMax - yMin;

        shipCenter = new Vector2(xMin + (xDiff / 2), yMin + (yDiff / 2)); 

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        foreach (Ship ship in selectedShips)
        {
            ship.SetTargetDestinationServerRPC((Vector2) worldPosition + ((Vector2) ship.transform.position - shipCenter));
        }

    }

    public void SetShips(List<Ship> ships)
    {
        VerifySelection();

        foreach (Ship ship in this.selectedShips)
        {
            ship.Unselect();
        }

        foreach (Ship ship in ships)
        {
            ship.Select(); 
        }

        this.selectedShips.Clear(); 
        foreach (Ship newShip in ships)
        {
            this.selectedShips.Add(newShip);
        }
    }

    [ServerRpc]
    public void SpawnShipServerRPC(Ship.ShipTypes shipType, ulong playerID)
    {
        Vector3 spawnPos = GameManager.Singleton.playerSpawns[playerID].transform.position;
        GameObject ship = Instantiate(GameManager.Singleton.shipPrefabs[(int)shipType], spawnPos, Quaternion.LookRotation(new Vector3(0, 0, 1), -spawnPos));
        ship.GetComponent<NetworkObject>().SpawnWithOwnership(playerID);
        ship.transform.parent = transform;
    }
}
