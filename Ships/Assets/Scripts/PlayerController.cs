using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] List<Ship> ships;
    float playerGold;
    [SerializeField] GameObject shop;
    Shop shopScript;
    Camera_Control cameraScript;

    int playerNum;
    GameObject spawnPlatform;

    float xMax;
    float yMax;
    float xMin;
    float yMin;
    float xDiff;
    float yDiff;
    Vector2 shipCenter;

    private void Start()
    {
        ships = new List<Ship>();

        GameObject newShop = Instantiate(shop, GameObject.Find("Canvas").transform);
        newShop.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        newShop.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        newShop.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        newShop.GetComponent<RectTransform>().anchoredPosition = new Vector3(-100, 0, -1);

        shopScript = newShop.GetComponent<Shop>();

        playerGold = 200f;
        playerNum = (int)this.GetComponent<NetworkObject>().OwnerClientId + 1;

        cameraScript = GameObject.Find("Main Camera").GetComponent<Camera_Control>();
        if(playerNum == 1)
        {
            spawnPlatform = GameObject.Find("p1Spawn");
            if (IsOwner) 
                cameraScript.transform.position = new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, cameraScript.transform.position.z);
        }
        else
        {
            spawnPlatform = GameObject.Find("p2Spawn");
            if(IsOwner)
                cameraScript.transform.position = new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, cameraScript.transform.position.z);
        }

        Debug.Log("WASD/Mouse - Move Camera\n" +
                  "Scroll Wheel - Zoom Camera\n" +
                  "E - Toggle Camera Lock\n" +
                  "Left Click - Select Ships\n" +
                  "Right Click - Move Selected Ships\n" +
                  "Q - Stop Ships\n" +
                  "R - Open Shop\n");
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
            return;

        // Setting the target destination for the ships
        if (Input.GetButtonDown("Fire2"))
        {
            
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            if (ships.Count == 1)
            {
                ships[0].setDestination(worldPosition);
            }
            else
            {
                SetDestinationInFormation(); 
            }
        }

        //WASD keys are used in the Camera_Control script

        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Ship ship in ships)
            {
                ship.StopShip(); 
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            shopScript.openShop(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            cameraScript.toggleLockState();
        }
    }

    public void SetDestinationInFormation()
    {
        if (ships.Count == 0) 
        {
            return;
        }
        else 
        {
            xMax = ships[0].transform.position.x;
            yMax = ships[0].transform.position.y;
            xMin = ships[0].transform.position.x;
            yMin = ships[0].transform.position.y;
        }

        foreach (Ship ship in ships)
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

        foreach (Ship ship in ships)
        {
            ship.setDestination((Vector2) worldPosition + ((Vector2) ship.transform.position - shipCenter));
        }

    }

    [ServerRpc]
    public void createShipServerRPC(Ship.shipTypes shipType, float cost, int playerNum)
    {
        if (getPlayerGold() >= cost)
        {
            changePlayerGold(-cost);

            //SPAWN THE SHIP
            GameObject ship = null;

            if (playerNum == 1)
            {
                switch (shipType)
                {
                    case Ship.shipTypes.Destroyer:
                        ship = Instantiate(shopScript.p1_destroyer_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p1_destroyer_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 225f));
                        break;
                    case Ship.shipTypes.Hawk:
                        ship = Instantiate(shopScript.p1_hawk_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p1_hawk_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 225f));
                        break;
                    case Ship.shipTypes.Challenger:
                        ship = Instantiate(shopScript.p1_challenger_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p1_challenger_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 225f));
                        break;
                    case Ship.shipTypes.Goliath:
                        ship = Instantiate(shopScript.p1_goliath_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p1_goliath_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 225f));
                        break;
                    case Ship.shipTypes.Lightning:
                        ship = Instantiate(shopScript.p1_lightning_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p1_lightning_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 225f));
                        break;
                    case Ship.shipTypes.Drone:
                        ship = Instantiate(shopScript.p1_drone_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p1_drone_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 225f));
                        break;
                    case Ship.shipTypes.Scout:
                        ship = Instantiate(shopScript.p1_scout_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p1_scout_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 225f));
                        break;
                }
            }
            else
            {
                switch (shipType)
                {
                    case Ship.shipTypes.Destroyer:
                        ship = Instantiate(shopScript.p2_destroyer_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p2_destroyer_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 45f));
                        break;
                    case Ship.shipTypes.Hawk:
                        ship = Instantiate(shopScript.p2_hawk_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p2_hawk_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 45f));
                        break;
                    case Ship.shipTypes.Challenger:
                        ship = Instantiate(shopScript.p2_challenger_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p2_challenger_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 45f));
                        break;
                    case Ship.shipTypes.Goliath:
                        ship = Instantiate(shopScript.p2_goliath_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p2_goliath_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 45f));
                        break;
                    case Ship.shipTypes.Lightning:
                        ship = Instantiate(shopScript.p2_lightning_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p2_lightning_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 45f));
                        break;
                    case Ship.shipTypes.Drone:
                        ship = Instantiate(shopScript.p2_drone_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p2_drone_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 45f));
                        break;
                    case Ship.shipTypes.Scout:
                        ship = Instantiate(shopScript.p2_scout_prefab, new Vector3(spawnPlatform.transform.position.x, spawnPlatform.transform.position.y, shopScript.p2_scout_prefab.transform.position.z), Quaternion.Euler(0f, 0f, 45f));
                        break;
                }
            }
            ship.GetComponent<NetworkObject>().SpawnWithOwnership((ulong)(ship.GetComponent<Ship>().getPlayerNum() - 1));
        }
    }

    public void SetShips(List<Ship> ships)
    {
        this.ships = ships;
    }

    public float getPlayerGold()
    {
        return playerGold;
    }
    public void changePlayerGold(float gold)
    {
        playerGold += gold;
    }
}
