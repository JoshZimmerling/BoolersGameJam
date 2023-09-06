using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] GameObject hpBar;

    public void UpdateHPBar(float hpPercent)
    {
        hpBar.transform.localScale = new Vector3(hpPercent, 1, 1);
        hpBar.transform.localPosition = new Vector3((hpPercent * 0.5f) - 0.5f, 0, 0);
    }
}
