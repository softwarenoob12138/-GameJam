using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GetMood : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject hotel;

    public Sprite Happy;
    public Sprite Sad;

    public bool startToCheck;

    private void Start()
    {
        startToCheck = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (CustomerCheck.instance.transform.childCount > 0)
        {
            GameManager.instance.ServeNext = true;
        }

        GameManager.instance.targetPosition = new Vector2(GameManager.instance.queuePosition.position.x + GameManager.instance.customerSpacing, GameManager.instance.queuePosition.position.y);

        if (transform.GetComponent<Image>().sprite == Happy)
        {
            GoldManager.instance.goldCoinQuantity += 20;
            ShopManager.instance.UpdateGoldDisplay();
        }
        else 
        {
            GoldManager.instance.goldCoinQuantity += 10;
            ShopManager.instance.UpdateGoldDisplay();
            Debug.Log("No");
        }

        GameManager.instance.moods.SetActive(false);

        if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().cow)
        {
            CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isAcount = true;

        }
        if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().fish)
        {
            CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isAcount = true;

        }

        if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().frog)
        {
            CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isAcount = true;

        }

        if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().bird)
        {
            CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isAcount = true;

        }

        if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().fox)
        {
            CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isAcount = true;

        }

        CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().StartCoroutine(CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().Out(6f));
        CustomerCheck.instance.transform.GetChild(0).SetParent(hotel.transform, true);

    }

    private void OnEnable()  // 启用的时候做判断展示满意度
    {
        if (Sky.instance.dayTime >= 2)
        {

            if (startToCheck)
            {
                if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isHappy == true)
                {
                    transform.GetComponent<Image>().sprite = Happy;
                }
                else
                {
                    transform.GetComponent<Image>().sprite = Sad;
                }
            }
        }
    }

}