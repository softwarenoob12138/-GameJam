using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerInMainGame : MonoBehaviour
{
    public static InventoryManagerInMainGame instance;
    public Image[] inventorySlots; // 物品栏槽的Image数组

    public Image[] inventorySlotsInDreamScene;

    private void Awake()
    {
        instance = this;
    }

    // 回调方法，当该组件被禁用时调用
    private void OnDisable()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlotsInDreamScene[i].sprite = inventorySlots[i].sprite;
        }
    }

    // 回调方法，当该组件被启用时调用
    private void OnEnable()
    {
        DayManager.instance.startComeNewCustomer = false;
    }
}