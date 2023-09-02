using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] GameObject hpBar;

    public void updateHPBar(float maxHP, float currHP)
    {
        float hpPercent = currHP / maxHP;
        hpBar.transform.localScale = new Vector3(hpPercent, 1, 1);
        hpBar.transform.localPosition = new Vector3((hpPercent * 0.5f) - 0.5f, 0, -0.1f);
    }
}
