using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerInMainGame : MonoBehaviour
{
    public static InventoryManagerInMainGame instance;
    public Image[] inventorySlots; // ��Ʒ���۵�Image����

    public Image[] inventorySlotsInDreamScene;

    private void Awake()
    {
        instance = this;
    }

    // �ص��������������������ʱ����
    private void OnDisable()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlotsInDreamScene[i].sprite = inventorySlots[i].sprite;
        }
    }

    // �ص��������������������ʱ����
    private void OnEnable()
    {
        DayManager.instance.startComeNewCustomer = false;
    }
}