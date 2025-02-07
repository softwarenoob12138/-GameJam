using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject check;
    private CustomerCheck customerCheck;

    public GameObject customerPrefab; // 顾客预制体
    public Transform queuePosition; // 队列的位置
    public Vector2 targetPosition; // 目标位置
    public float customerSpacing = 2f; // 顾客之间的间距
    public float moveSpeed;
    public bool ServeNext;

    public GameObject needs;
    public GameObject moods;
    public GameObject dialog;

    public GameObject mainGameScene;

    public GameObject dialogSceneInOpen;
    public GameObject dialogScene;
    public GameObject dialogSceneBadEnding;
    public GameObject dialogSceneGoodEnding;
    public GameObject dialogScene2;

    public GameObject dreamScene;
    public GameObject dreamScene1;
    public bool isGoodEnding;

    public bool fadeOut;

    private void Awake()
    {
        instance = this;
        mainGameScene = GameObject.FindGameObjectWithTag("MainGameScene");
        dreamScene = GameObject.FindGameObjectWithTag("Dream");
        dreamScene1 = GameObject.FindGameObjectWithTag("Dream1");
        dialogSceneInOpen = GameObject.FindGameObjectWithTag("DialogSceneInOpen");
        dialogScene = GameObject.FindGameObjectWithTag("DialogScene");
        dialogSceneBadEnding = GameObject.FindGameObjectWithTag("DialogScene2BadEnding");
        dialogSceneGoodEnding = GameObject.FindGameObjectWithTag("DialogScene2GoodEnding");
        dialogScene2 = GameObject.FindGameObjectWithTag("DialogScene3");

    }

    private void Start()
    {
        check = GameObject.FindWithTag("Customer");
        customerCheck = check.GetComponent<CustomerCheck>();
        targetPosition = CustomerCheck.instance.transform.position;

        mainGameScene.SetActive(false);
        dreamScene.SetActive(false);
        dreamScene1.SetActive(false);
        dialogScene.SetActive(false);
        dialogSceneBadEnding.SetActive(false);
        dialogSceneGoodEnding.SetActive(false);
        dialogScene2.SetActive(false);

        //ServeNext = true;

        //if (Sky.instance.dayTime == 1 && Sky.instance.inDay)
        //{

        //    // 初始化顾客  // 到时可能用存档系统处理


        //    for (int i = 0; i < 3; i++)
        //    {
        //        // 计算每个顾客的位置
        //        Vector3 customerPosition = queuePosition.position + new Vector3(-customerSpacing * i, 0, 0);
        //        var customerInstance = Instantiate(customerPrefab, customerPosition, Quaternion.identity);

        //        if (i == 0)
        //        {
        //            customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().cow;
        //        }

        //        else if (i == 1)
        //        {
        //            customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().fish;
        //        }

        //        else if(i == 2)
        //        {
        //            customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().frog;
        //        }

        //        customerInstance.transform.SetParent(check.transform, true);
        //    }


        //    //// 初始化顾客
        //    //for (int i = 0; i < 4; i++)
        //    //{
        //    //    // 计算每个顾客的位置
        //    //    Vector3 customerPosition = queuePosition.position + new Vector3(-customerSpacing * i, 0, 0);
        //    //    var customerInstance = Instantiate(customerPrefab, customerPosition, Quaternion.identity);
        //    //    customerInstance.transform.SetParent(queuePosition, true);
        //    //}

        //}
    }

    private void Update()
    {
        if (CustomerCheck.instance.transform.childCount > 0)
        {
            if (ServeNext && CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isServe == false)
            {
                ServeNextCustomer();
            }

            else if (ServeNext && CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isAcount == false && CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isServe == true)
            {
                AcountNextCustomer();
            }
        }
        else
        {
            ServeNext = false;
        }

    }

    public void StartDayOne()
    {
        if (Sky.instance.dayTime == 1 && Sky.instance.inDay)
        {
            StartCoroutine(WaitForNewCustomerInDayOne(2f));
        }
    }


    public void ServeNextCustomer()
    {
        queuePosition.position = Vector2.MoveTowards(queuePosition.position, targetPosition, Time.deltaTime * moveSpeed);

        if (Vector2.Distance(queuePosition.position, targetPosition) < 0.1f)
        {
            queuePosition.position = targetPosition;
            ServeNext = false;

            if (CustomerCheck.instance.transform.childCount > 0 && CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isTalk == false)
            {
                needs.SetActive(true);
            }

            else
            {
                dialog.SetActive(true);
            }

            if (!dialog.activeSelf)
            {
                StartCoroutine(customerCheck.CheckCustomer());
            }
        }

        UpdateOriginalPosition();

    }

    public void AcountNextCustomer()
    {
        queuePosition.position = Vector2.MoveTowards(queuePosition.position, targetPosition, Time.deltaTime * moveSpeed);

        if (Vector2.Distance(queuePosition.position, targetPosition) < 0.1f)
        {
            queuePosition.position = targetPosition;
            ServeNext = false;


            if (CustomerCheck.instance.transform.childCount > 0 && CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isTalk == false)
            {
                if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isServe == true)
                {
                    moods.SetActive(true);
                }
            }

            else
            {
                dialog.SetActive(true);
            }
        }
    }

    public void UpdateOriginalPosition()
    {
        for (int i = 0; i < CustomerCheck.instance.transform.childCount; i++)
        {
            if (CustomerCheck.instance.transform.GetChild(i).GetComponent<CustomerDrag>() != null)
            {

                CustomerCheck.instance.transform.GetChild(i).GetComponent<CustomerDrag>().originalPosition = CustomerCheck.instance.transform.GetChild(i).GetComponent<CustomerDrag>().transform.position;
            }
        }
    }

    public IEnumerator WaitForNewCustomerInDayOne(float time)  // 只有在协程之中才能给 for 循环和一些其他操作添加缓冲
    {

        CustomerCheck.instance.rtf.anchoredPosition = new Vector2(CustomerCheck.instance.originalPosition.x, CustomerCheck.instance.originalPosition.y);

        targetPosition = CustomerCheck.instance.transform.position;

        if (Sky.instance.dayTime == 1 && Sky.instance.inDay)
        {

            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(time);
                // 计算每个顾客的位置

                Vector3 customerPosition = queuePosition.position + new Vector3(-customerSpacing * i, 0, 0);
                var customerInstance = Instantiate(customerPrefab, customerPosition, Quaternion.identity);

                if (i == 0)
                {
                    customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().cow;
                }

                else if (i == 1)
                {
                    customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().fish;
                }

                else if (i == 2)
                {
                    customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().frog;
                }

                customerInstance.transform.SetParent(check.transform, true);

                ServeNext = true;

            }
        }
    }

    public IEnumerator LoadForNewCustomerInDayOne()  // 只有在协程之中才能给 for 循环和一些其他操作添加缓冲
    {

        CustomerCheck.instance.rtf.anchoredPosition = new Vector2(CustomerCheck.instance.originalPosition.x, CustomerCheck.instance.originalPosition.y);

        targetPosition = CustomerCheck.instance.transform.position;

        if (Sky.instance.dayTime == 1 && Sky.instance.inDay)
        {

            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(.001f);
                // 计算每个顾客的位置

                Vector3 customerPosition = queuePosition.position + new Vector3(-customerSpacing * i, 0, 0);
                var customerInstance = Instantiate(customerPrefab, customerPosition, Quaternion.identity);

                if (i == 0)
                {
                    customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().cow;
                }

                else if (i == 1)
                {
                    customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().fish;
                }

                else if (i == 2)
                {
                    customerInstance.GetComponent<CustomerDrag>().icon.sprite = customerInstance.GetComponent<CustomerDrag>().frog;
                }

                customerInstance.transform.SetParent(check.transform, true);

                ServeNext = true;

            }

            yield return new WaitForSeconds(.12f);

            if (CustomerCheck.instance.transform.childCount == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector3 customerPosition = CustomerCheck.instance.transform.GetChild(i).position + new Vector3(customerSpacing * 2, 0, 0);
                    CustomerCheck.instance.transform.GetChild(i).position = customerPosition;
                    UpdateOriginalPosition();

                }
            }

            if (CustomerCheck.instance.transform.childCount == 1)
            {
                Vector3 customerPosition = CustomerCheck.instance.transform.GetChild(0).position + new Vector3(customerSpacing * 2, 0, 0);
                CustomerCheck.instance.transform.GetChild(0).position = customerPosition;
                UpdateOriginalPosition();
            }
        }
    }

    public void ToDialogScene()
    {

        if (CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().frog && fadeOut == false)
        {
            fadeOut = true;
            StartCoroutine(LoadSceneToDialogWithFadeEffect(1.5f));
        }

        if (CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().fox && fadeOut == false)
        {
            fadeOut = true;
            StartCoroutine(LoadSceneToDialogWithFadeEffect(1.5f));
        }
    }

    public void ToDreamScene()
    {
        if (fadeOut == false)
        {
            fadeOut = true;
            StartCoroutine(LoadSceneToDreamWithFadeEffect(1.5f));
        }
    }

    IEnumerator LoadSceneToDialogWithFadeEffect(float _delay)
    {
        Sky.instance.fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        // 使用 LoadScene 方法前确定导入了 UnityEngine.SceneManagement 命名空间
        // 该方法 位于 UnityEngine.SceneManagement 命名空间下的 SceneManager 类中

        if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().frog
            || CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().fox)
        {
            CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().isTalk = false;
        }

        fadeOut = false;

        if (Sky.instance.dayTime == 1 && Sky.instance.inDay && CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().frog)
        {
            dialogScene.SetActive(true);

            mainGameScene.SetActive(false);

            Sky.instance.fadeScreen.FadeIn();
        }

        if (Sky.instance.dayTime == 2 && Sky.instance.inDay && CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().frog && !isGoodEnding)
        {
            dialogSceneBadEnding.SetActive(true);

            mainGameScene.SetActive(false);

            Sky.instance.fadeScreen.FadeIn();
        }

        if (Sky.instance.dayTime == 2 && Sky.instance.inDay && CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().frog && isGoodEnding)
        {
            dialogSceneGoodEnding.SetActive(true);

            mainGameScene.SetActive(false);

            Sky.instance.fadeScreen.FadeIn();
        }

        if (Sky.instance.dayTime == 2 && Sky.instance.inDay && CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).transform.GetComponent<CustomerDrag>().fox)
        {
            dialogScene2.SetActive(true);

            mainGameScene.SetActive(false);

            Sky.instance.fadeScreen.FadeIn();
        }
    }

    IEnumerator LoadSceneToDreamWithFadeEffect(float _delay)
    {
        Sky.instance.fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        // 使用 LoadScene 方法前确定导入了 UnityEngine.SceneManagement 命名空间
        // 该方法 位于 UnityEngine.SceneManagement 命名空间下的 SceneManager 类中


        fadeOut = false;

        Sky.instance.fadeScreen.FadeIn();
        if (Sky.instance.dayTime == 2)
        {
            dreamScene1.SetActive(true);
            mainGameScene.SetActive(false);
        }

        else
        {
            dreamScene.SetActive(true);
            mainGameScene.SetActive(false);

        }
    }

}