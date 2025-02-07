using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;

    private void Update()
    {
        if (icon.sprite == null)
        {
            Color currentColor = icon.color;
            currentColor.a = 0.3f;
            icon.color = currentColor;

        }
        else
        {
            Color currentColor = icon.color;
            currentColor.a = 1f;
            icon.color = currentColor;

        }
    }

    public void OnButtonClick()
    {
        InventoryManagerInDream.instance.OnEquipmentSlotClick();
    }
}
