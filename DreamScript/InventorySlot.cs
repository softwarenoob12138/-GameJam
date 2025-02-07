using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public int slotIndex;

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
        if (GameObject.FindWithTag("Dream") != null)
        {
            InventoryManagerInDream.instance.OnInventorySlotClick(slotIndex);
        }
        else
        {
            InventoryManagerInDream2.instance.OnInventorySlotClick(slotIndex);
        }
    }
}
