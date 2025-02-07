using System.Collections;
using System.IO;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;

    public Vector2 customerComePosition;

    public bool startComeNewCustomer;

    private void Awake()
    {
        startComeNewCustomer = false;
        instance = this;
    }

    void Start()
    {

        StartCoroutine(WaitForCustomersInDayOne());

    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Customers") == null && startComeNewCustomer)
        {
            ComeNewCustomerInDayTwo();
        }
    }

    public void ComeNewCustomerInDayTwo()
    {
        startComeNewCustomer = false;  // 让 ComeNewCustomerInDayTwo() 方法在 Update 方法中只调用一次
        StartCoroutine(WaitForNewCustomerInDayTwo());
    }

    public IEnumerator WaitForNewCustomerInDayTwo()  // 只有在协程之中才能给 for 循环和一些其他操作添加缓冲
    {

        CustomerCheck.instance.rtf.anchoredPosition = new Vector2(CustomerCheck.instance.originalPosition.x, CustomerCheck.instance.originalPosition.y);

        GameManager.instance.targetPosition = CustomerCheck.instance.transform.position;

        CustomerCheck.instance.rtf.anchoredPosition = new Vector2(CustomerCheck.instance.originalPosition.x - GameManager.instance.customerSpacing * 1.5f, CustomerCheck.instance.originalPosition.y);

        if (Sky.instance.dayTime == 2 && Sky.instance.inDay)
        {

            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(2f);
                // 计算每个顾客的位置
                Vector3 customerPosition = GameManager.instance.queuePosition.position + new Vector3(-GameManager.instance.customerSpacing * i, 0, 0);
                var customerInstance = Instantiate(GameManager.instance.customerPrefab, customerPosition, Quaternion.identity);

                if (i == 0)
                {
                    customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().bird;
                }

                else if (i == 1)
                {
                    customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().fox;
                }

                customerInstance.transform.SetParent(GameManager.instance.check.transform, true);

                GameManager.instance.ServeNext = true;

            }
        }
    }
    public IEnumerator LoadNewCustomerInDayTwo()  // 只有在协程之中才能给 for 循环和一些其他操作添加缓冲
    {

        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(.001f);
            // 计算每个顾客的位置
            Vector3 customerPosition = GameManager.instance.queuePosition.position + new Vector3(-GameManager.instance.customerSpacing * i, 0, 0);
            var customerInstance = Instantiate(GameManager.instance.customerPrefab, customerPosition, Quaternion.identity);

            if (i == 0)
            {
                customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().bird;
            }

            else if (i == 1)
            {
                customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().fox;
            }

            customerInstance.transform.SetParent(GameManager.instance.check.transform, true);

            GameManager.instance.ServeNext = true;

            yield return new WaitForSeconds(.12f);
        }
        if (CustomerCheck.instance.transform.childCount == 1)
        {
            Vector3 customerPosition = (CustomerCheck.instance.transform.GetChild(0).position + new Vector3(GameManager.instance.customerSpacing, 0, 0));
            CustomerCheck.instance.transform.GetChild(0).position = customerPosition;
            GameManager.instance.UpdateOriginalPosition();
        }
    }

    public IEnumerator WaitForCustomersInDayOne()
    {
        if (File.Exists(Sky.instance.savePath))
        {
            yield return new WaitForSeconds(.2f);

            if (Sky.instance.dayTime == 2 && Sky.instance.inDay && GameObject.FindGameObjectWithTag("Customers") == null)
            {
                StartCoroutine(LoadNewCustomerInDayTwo());
            }
        }
    }

}
