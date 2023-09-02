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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                foreach(Ship ship in ships)
                {
                    
                }
            }
        }
    }
}
