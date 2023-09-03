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

    private void Start()
    {
        ships = new List<Ship>();

        GameObject newShop = Instantiate(shop);
        newShop.transform.SetParent(GameObject.Find("Canvas").transform);
        newShop.transform.localPosition = new Vector3(600, 0, -1);
        /*newShop.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        newShop.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        newShop.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);*/
        shopScript = newShop.GetComponent<Shop>();

        playerGold = 200f;
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
            foreach (Ship ship in ships)
            {
                ship.setDestination(worldPosition.x, worldPosition.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (Ship ship in ships)
            {
                ship.StopShip(); 
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            shopScript.openShop(this.gameObject);
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
