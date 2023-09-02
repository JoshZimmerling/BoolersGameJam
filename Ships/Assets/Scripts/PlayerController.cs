using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] List<Ship> ships;

    private void Start()
    {
        ships = new List<Ship>();

        /* TODO - Temp add ships */ 
        ships.Add(FindObjectOfType<Ship>());
    }

    // Update is called once per frame
    void Update()
    {
        // Only can be controlled by owner
/*        if (!IsOwner)
            return;*/

        Debug.Log("guh2");
        // Test code, remove later
        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey("w")) moveDir.y += 1f;
        if (Input.GetKey("a")) moveDir.x -= 1f;
        if (Input.GetKey("s")) moveDir.y -= 1f;
        if (Input.GetKey("d")) moveDir.x += 1f;
        transform.position += moveDir * Time.deltaTime;

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("guh");
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            foreach (Ship ship in ships)
            {
                ship.setDestination(worldPosition.x, worldPosition.y);
            }
        }
    }
}
