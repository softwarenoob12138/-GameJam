using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerInDream : MonoBehaviour
{
    public static InventoryManagerInDream instance;
    public Image equipmentSlot; // װ���۵�Image���
    public Image[] inventorySlots; // ��Ʒ���۵�Image����
    private int selectedInventorySlot = -1; // ��¼ѡ�е���Ʒ��������

    public Image[] inventorySlotsInMainGameScene;

    private void Awake()
    {
        instance = this;
    }

    public void OnInventorySlotClick(int slotIndex)
    {
        if (equipmentSlot.sprite == null && inventorySlots[slotIndex].sprite != null)
        {
            // ��װ����Ϊ���ҵ������Ʒ����������Ʒʱ������Ʒ�ƶ���װ����
            equipmentSlot.sprite = inventorySlots[slotIndex].sprite;
            inventorySlots[slotIndex].sprite = null;
        }
        else if (equipmentSlot.sprite != null && inventorySlots[slotIndex].sprite != null)
        {
            // ��װ���ۺ͵������Ʒ���۶�����Ʒʱ��������Ʒ
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

    // �ص��������������������ʱ����
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

    // �ص��������������������ʱ����
    private void OnEnable()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].sprite = inventorySlotsInMainGameScene[i].sprite;
        }
    }

}
