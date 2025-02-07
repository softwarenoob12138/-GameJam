using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BedSlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Sprite MuBanChuang;
    public Sprite NiaoChao;
    public Sprite YuGang;

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
                // ����ԭλ��
                transform.position = originalPosition;
            }
        }
    }

    private void MoveOrSwapItems(BedSlot targetSlot)
    {
        if (targetSlot.icon.sprite == null)
        {
            // Ŀ���Ϊ�գ�ֱ���ƶ�Sprite
            targetSlot.icon.sprite = icon.sprite;
            icon.sprite = null;
        }
        else
        {
            // ����Sprite
            Sprite tempSprite = targetSlot.icon.sprite;
            targetSlot.icon.sprite = icon.sprite;
            icon.sprite = tempSprite;
        }

        // ����ԭλ��
        transform.position = originalPosition;
    }

    private void MoveOrSwapItemsToSlot(Slot targetSlot)
    {

        icon.sprite = null;


        // ����ԭλ��
        transform.position = originalPosition;
    }
}
