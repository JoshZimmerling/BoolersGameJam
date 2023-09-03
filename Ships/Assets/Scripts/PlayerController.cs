using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] List<Ship> ships;
    [SerializeField] int playerNum;

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
    }

    public void SetShips(List<Ship> ships)
    {
        this.ships = ships;
    }
}
