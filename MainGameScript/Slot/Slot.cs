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


    public Image icon; // ��ʾ�ڲ��ϵ�ͼ��
    private Vector2 originalPosition; // ��ԭʼλ��
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
                    // ����ԭλ��
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
                    // ����ԭλ��
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
                    // ����ԭλ��
                    transform.position = originalPosition;
                }
            }


        }
    }

    private void MoveOrSwapItemsOnBedSlot(BedSlot targetSlot)
    {
        if (targetSlot.icon.sprite == null)
        {
            // Ŀ���Ϊ�գ�ֱ���ƶ�Sprite
            targetSlot.icon.sprite = icon.sprite;
        }
        // ����ԭλ��
        transform.position = originalPosition;
    }
    private void MoveOrSwapItemsOnDeskSlot(DeskSlot targetSlot)
    {
        if (targetSlot.icon.sprite == null)
        {
            // Ŀ���Ϊ�գ�ֱ���ƶ�Sprite
            targetSlot.icon.sprite = icon.sprite;
        }

        // ����ԭλ��
        transform.position = originalPosition;
    }
    private void MoveOrSwapItemsOnWallSlot(WallSlot targetSlot)
    {
        if (targetSlot.icon.sprite == null)
        {
            // Ŀ���Ϊ�գ�ֱ���ƶ�Sprite
            targetSlot.icon.sprite = icon.sprite;
        }

        // ����ԭλ��
        transform.position = originalPosition;
    }

}


