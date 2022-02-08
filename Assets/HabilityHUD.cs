using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabilityHUD : MonoBehaviour
{
    public static HabilityHUD instance;

    //Referencias
    public Image[] slots = new Image[3];
    public RectMask2D[] cooldowns = new RectMask2D[3];

    public Sprite[] habilities = new Sprite[9];

    void Awake()
    {
        if (instance) Destroy(gameObject);
        instance = this;
    }

    public void SetHability(int index, int hability)
    {
        slots[index].sprite = habilities[hability];
    }

    public void UpdateCooldown(int index, float perc)
    {
        float factor = 1;
        if (index == 1) factor = 2;
        RectMask2D mask = cooldowns[index];
        Vector4 aux = cooldowns[index].padding;
        aux.y = (1 - perc) * (100 - 4.5f * factor) * factor + 4.5f * factor;
        cooldowns[index].padding = aux;
    }
}
