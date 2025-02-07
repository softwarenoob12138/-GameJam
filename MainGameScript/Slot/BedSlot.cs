using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BedSlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Sprite MuBanChuang;
    public Sprite NiaoChao;
    public Sprite YuGang;

    public Image icon; // 显示在槽上的图标
    private Vector2 originalPosition; // 槽原始位置
    private bool originalRaycastTarget;

    void Start()
    {
        originalPosition = transform.position;
        originalRaycastTarget = icon.raycastTarget;
    }

    private void Update()
    {
        if (icon.sprite == null)
        {
            Color currentColor = icon.color;
            currentColor.a = 0f;

            Color currentColorDropDown = transform.GetChild(0).GetComponent<Image>().color;
            currentColorDropDown.a = 1f;

            transform.GetChild(0).GetComponent<Image>().color = currentColorDropDown;
            icon.color = currentColor;

        }
        else
        {
            Color currentColor = icon.color;
            currentColor.a = 1f;

            Color currentColorDropDown = transform.GetChild(0).GetComponent<Image>().color;
            currentColorDropDown.a = 0f;

            transform.GetChild(0).GetComponent<Image>().color = currentColorDropDown;

            icon.color = currentColor;

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (icon.sprite != null)
        {

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (icon.sprite != null)
        {
            transform.position = Input.mousePosition;
            icon.raycastTarget = false;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (icon.sprite != null)
        {
            icon.raycastTarget = originalRaycastTarget;

            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<BedSlot>() != null)
            {
                BedSlot targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<BedSlot>();
                MoveOrSwapItems(targetSlot);
            }
            else if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>() != null)
            {
                Slot targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>();
                MoveOrSwapItemsToSlot(targetSlot);
            }
            else
            {
                // 返回原位置
                transform.position = originalPosition;
            }
        }
    }

    private void MoveOrSwapItems(BedSlot targetSlot)
    {
        if (targetSlot.icon.sprite == null)
        {
            // 目标槽为空，直接移动Sprite
            targetSlot.icon.sprite = icon.sprite;
            icon.sprite = null;
        }
        else
        {
            // 交换Sprite
            Sprite tempSprite = targetSlot.icon.sprite;
            targetSlot.icon.sprite = icon.sprite;
            icon.sprite = tempSprite;
        }

        // 返回原位置
        transform.position = originalPosition;
    }

    private void MoveOrSwapItemsToSlot(Slot targetSlot)
    {

        icon.sprite = null;


        // 返回原位置
        transform.position = originalPosition;
    }
}
