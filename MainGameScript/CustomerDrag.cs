using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomerDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Sprite cow;
    public Sprite fish;
    public Sprite frog;
    public Sprite fox;
    public Sprite bird;

    public RectTransform rtf;
    public Vector2 oldPosition; // 记录客人原始位置

    public Image icon; // 显示在槽上的图标
    public Vector2 originalPosition; // 槽原始位置

    public bool isInRoom;

    public bool isServe;
    public bool isAcount;
    public bool isTalk;

    public float moveSpeed;
    public GameObject hotel;

    public bool isDestroy;

    public bool isHappy;

    void Start()
    {
        InvokeRepeating("MoveAction", 0f, .6f);

        hotel = GameObject.FindGameObjectWithTag("Hotel");

        rtf = GetComponent<RectTransform>();
        oldPosition = rtf.anchoredPosition;

        originalPosition = transform.position;
        icon.raycastTarget = false;

        StartCoroutine(TalkCheck());

    }

    private void Update()
    {
        if (transform.parent == hotel.transform)
        {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;

            // 此处中断会终止此组件正在调用的协程
            Destroy(gameObject, 6f);
        }
    }

    //private void Update()
    //{
    //    if(startDrag == true)
    //    {
    //        transform.position = Input.mousePosition;
    //    }
    //}

    public void MoveAction()
    {
        if (transform.rotation == Quaternion.Euler(0, 0, 8))
        {
            transform.rotation = Quaternion.Euler(0, 0, -8);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 8);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isServe && !isAcount)
        {
            if (icon.sprite != null)
            {

            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isServe && !isAcount)
        {
            if (icon.sprite != null)
            {
                transform.position = Input.mousePosition;
                icon.raycastTarget = false;
                CancelInvoke("MoveAction");
            }
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isServe && !isAcount)
        {
            if (icon.sprite != null)
            {
                CustomerSlot targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<CustomerSlot>();

                if (targetSlot != null && targetSlot.icon.sprite == null)
                {
                    if (icon.sprite == fox)
                    {
                        Sky.instance.canChangeNight = true;
                    }

                    MoveOrSwapItems(targetSlot);

                    Destroy(gameObject);

                    if (CustomerCheck.instance.transform.childCount > 0)
                    {
                        GameManager.instance.ServeNext = true;
                    }

                    GameManager.instance.targetPosition = new Vector2(GameManager.instance.queuePosition.position.x + GameManager.instance.customerSpacing, GameManager.instance.queuePosition.position.y);
                    GameManager.instance.needs.SetActive(false);

                }
                else
                {
                    // 没安排客房返回原位置使 射线穿透不变
                    transform.position = originalPosition;
                    icon.raycastTarget = true;
                    InvokeRepeating("MoveAction", 0f, .6f);

                }
            }

        }
    }

    private void MoveOrSwapItems(CustomerSlot targetSlot)
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

    public IEnumerator Out(float goOut)
    {
        isDestroy = false;

        if (CustomerCheck.instance.transform.childCount == 1)
        {
            DayManager.instance.startComeNewCustomer = true;

        }

        yield return new WaitForSeconds(goOut);

        DayManager.instance.customerComePosition = rtf.anchoredPosition;


        Destroy(gameObject);  

    }

    public IEnumerator TalkCheck()
    {
        yield return new WaitForSeconds(.2f);

        if (icon.sprite == frog || icon.sprite == fox)
        {
            isTalk = true;
        }
    }
}
