using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    SpriteRenderer outlineSprite;

    public void SetupOutlineColor(Color color)
    {
        outlineSprite = GetComponent<SpriteRenderer>();
        color.a = 0f;
        outlineSprite.color = color;
    }

    public void SetSelected()
    {
        Color newColor = outlineSprite.color;
        newColor.a = 1f;
        outlineSprite.color = newColor;
    }

    public void SetUnselected()
    {
        Color newColor = outlineSprite.color;
        newColor.a = 0f;
        outlineSprite.color = newColor;
    }
    
}
