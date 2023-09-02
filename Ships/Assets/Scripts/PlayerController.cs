using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
