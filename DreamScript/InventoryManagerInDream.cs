using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerInDream : MonoBehaviour
{
    public static InventoryManagerInDream instance;
    public Image equipmentSlot; // 装备槽的Image组件
    public Image[] inventorySlots; // 物品栏槽的Image数组
    private int selectedInventorySlot = -1; // 记录选中的物品栏槽索引

    public Image[] inventorySlotsInMainGameScene;

    private void Awake()
    {
        instance = this;
    }

    public void OnInventorySlotClick(int slotIndex)
    {
        if (equipmentSlot.sprite == null && inventorySlots[slotIndex].sprite != null)
        {
            // 当装备槽为空且点击的物品栏槽中有物品时，将物品移动到装备槽
            equipmentSlot.sprite = inventorySlots[slotIndex].sprite;
            inventorySlots[slotIndex].sprite = null;
        }
        else if (equipmentSlot.sprite != null && inventorySlots[slotIndex].sprite != null)
        {
            // 当装备槽和点击的物品栏槽都有物品时，交换物品
            Sprite tempSprite = equipmentSlot.sprite;
            equipmentSlot.sprite = inventorySlots[slotIndex].sprite;
            inventorySlots[slotIndex].sprite = tempSprite;
        }

        selectedInventorySlot = slotIndex;
    }

    public void OnEquipmentSlotClick()
    {
        if (equipmentSlot.sprite != null)
        {
            Sprite tempSprite = equipmentSlot.sprite;

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].sprite == null)
                {

                    equipmentSlot.sprite = null;
                    inventorySlots[i].sprite = tempSprite;
                    return;
                }
                else
                {
                    equipmentSlot.sprite = tempSprite;
                }
            }
        }
    }

    // 回调方法，当该组件被禁用时调用
    private void OnDisable()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlotsInMainGameScene[i].sprite = inventorySlots[i].sprite;
        }

        for (int i = 0; i < inventorySlotsInMainGameScene.Length; i++)
        {
            if (InventoryManagerInMainGame.instance.inventorySlots[i].sprite == null)
            {
                InventoryManagerInMainGame.instance.inventorySlots[i].sprite = equipmentSlot.sprite;
                break;
            }
        }

        equipmentSlot.sprite = null;
    }

    // 回调方法，当该组件被启用时调用
    private void OnEnable()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].sprite = inventorySlotsInMainGameScene[i].sprite;
        }
    }

}
