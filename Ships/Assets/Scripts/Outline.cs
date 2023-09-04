using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    SpriteRenderer outlineSprite;

    // Start is called before the first frame update
    void Start()
    {
        outlineSprite = GetComponent<SpriteRenderer>();
    }

    public void SetSelected()
    {
        outlineSprite.color = Color.white;
    }

    public void SetUnselected()
    {
        outlineSprite.color = new Color(0.25f, 0.25f, 0.25f); 
    }
    
}
