using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipSelection : MonoBehaviour
{

    public RectTransform selectionBox;
    private Vector2 startPos;


    private void Update()
    {
        startPos = Input.mousePosition;

        if ( Input.GetMouseButtonDown(0) )
        {
            UpdateBox(Input.mousePosition);
        }

        if ( Input.GetMouseButtonUp(0))
        {
            ReleaseBox();
        }
    }

    void UpdateBox(Vector2 curMouse)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }

        float width = curMouse.x - startPos.x;
        float height = curMouse.y - startPos.y; 

        selectionBox.sizeDelta = new Vector2( Mathf.Abs(width), Mathf.Abs(height) );
        selectionBox.anchoredPosition = startPos + new Vector2( width / 2, height /2 );
    }

    void ReleaseBox()
    {
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

    }
}
