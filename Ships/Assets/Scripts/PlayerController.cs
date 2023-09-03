using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] List<Ship> ships;
    [SerializeField] int playerNum;

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

        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (Ship ship in ships)
            {
                ship.StopShip(); 
            }
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

    public void SetShips(List<Ship> ships)
    {
        this.ships = ships;
    }
}
