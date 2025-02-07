using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Sprite MuBanChuang;
    public Sprite NiaoChao;
    public Sprite YuGang;
    public Sprite XiaoTiQin;
    public Sprite HuaPing;
    public Sprite WangYuanJing;
    public Sprite ABagOfGold;
    public Sprite MaoTouYingHuaXiang;
    public Sprite MaoTan;
    public Sprite YingYueJiaHuaXiang;


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

            if (icon.sprite == MuBanChuang || icon.sprite == NiaoChao || icon.sprite == YuGang)
            {
                BedSlot targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<BedSlot>();
                if (targetSlot != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<BedSlot>() != null)
                {
                    MoveOrSwapItemsOnBedSlot(targetSlot);
                }
                else
                {
                    // 返回原位置
                    transform.position = originalPosition;
                }
            }

            if (icon.sprite == XiaoTiQin || icon.sprite == HuaPing || icon.sprite == ABagOfGold)
            {
                DeskSlot targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<DeskSlot>();
                if (targetSlot != null)
                {
                    MoveOrSwapItemsOnDeskSlot(targetSlot);
                }
                else
                {
                    // 返回原位置
                    transform.position = originalPosition;
                }
            }

            if (icon.sprite == WangYuanJing || icon.sprite == MaoTouYingHuaXiang || icon.sprite == MaoTan || icon.sprite == YingYueJiaHuaXiang)
            {
                WallSlot targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<WallSlot>();
                if (targetSlot != null)
                {
                    MoveOrSwapItemsOnWallSlot(targetSlot);
                }
                else
                {
                    // 返回原位置
                    transform.position = originalPosition;
                }
            }


        }
    }

    private void MoveOrSwapItemsOnBedSlot(BedSlot targetSlot)
    {
        if (targetSlot.icon.sprite == null)
        {
            // 目标槽为空，直接移动Sprite
            targetSlot.icon.sprite = icon.sprite;
        }
        // 返回原位置
        transform.position = originalPosition;
    }
    private void MoveOrSwapItemsOnDeskSlot(DeskSlot targetSlot)
    {
        if (targetSlot.icon.sprite == null)
        {
            // 目标槽为空，直接移动Sprite
            targetSlot.icon.sprite = icon.sprite;
        }

        // 返回原位置
        transform.position = originalPosition;
    }
    private void MoveOrSwapItemsOnWallSlot(WallSlot targetSlot)
    {
        if (targetSlot.icon.sprite == null)
        {
            // 目标槽为空，直接移动Sprite
            targetSlot.icon.sprite = icon.sprite;
        }

        // 返回原位置
        transform.position = originalPosition;
    }

}


