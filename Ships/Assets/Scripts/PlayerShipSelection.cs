using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class PlayerShipSelection : MonoBehaviour
{

    [SerializeField] Transform selectionBox;

    private Vector2 startPos;
    private Vector2 curPos;

    private float curWidth;
    private float curHeight;

    private PlayerController player;

    public List<Collider2D> hitColliders;
    public List<Ship> shipsFromHit;


    private void Start()
    {
        hitColliders = new List<Collider2D>();

        player = transform.GetComponent<PlayerController>();    
    }


    private void Update()
    {
        if ( Input.GetMouseButtonDown(0) )
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if ( Input.GetMouseButton(0) ) 
        {
            UpdateBox(Input.mousePosition);
        }

        if ( Input.GetMouseButtonUp(0))
        {
            ReleaseBox();
        }
    }

    void UpdateBox(Vector2 mousePos)
    {
        selectionBox.gameObject.SetActive(true);

        curPos = Camera.main.ScreenToWorldPoint(mousePos);

        curWidth = startPos.x - curPos.x;
        curHeight = startPos.y - curPos.y;

        selectionBox.localScale = new Vector2(curWidth, curHeight);
        
        selectionBox.transform.position = new Vector3( startPos.x - (curWidth / 2), startPos.y - (curHeight / 2) , -1 );
    }

    void ReleaseBox()
    {
        Transform box = selectionBox.transform;
        ContactFilter2D contactFilter = new ContactFilter2D();

        Physics2D.OverlapBox(box.position, new Vector2(Mathf.Abs(box.localScale.x), Mathf.Abs(box.localScale.y)), 0, contactFilter, hitColliders);

        int playerID = (int)player.GetComponent<NetworkObject>().OwnerClientId + 1;

        shipsFromHit.Clear(); 
        foreach (Collider2D col in hitColliders)
        {
            Ship ship = col.GetComponent<Ship>();
            if(ship != null)
                if (playerID == ship.getPlayerNum())
                    shipsFromHit.Add(ship);
        }

        player.SetShips(shipsFromHit);

        selectionBox.gameObject.SetActive(false);
    }
}
