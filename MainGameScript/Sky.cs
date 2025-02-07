using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]

public class DayTime
{
    public int Day;
    public Sprite DayOrNight;
}

public class Sky : MonoBehaviour
{
    [SerializeField] private string toTanksScene;

    public static Sky instance;

    public DayTime InDay;

    public string savePath;

    public UI_FadeScreen fadeScreen;

    private GameObject sky;

    public Sprite day;
    public Sprite night;

    public Sprite dayOrNight;

    public bool inDay;
    public bool inNight;
    public int dayTime = 1;
    private GameObject endTheDayButton;
    private Text buttonText;

    private GameObject check;

    public List<GameObject> rooms;

    private List<Image> exitCustomers;

    public List<GameObject> dreams;

    public GameObject customerPrefab;

    public bool canChangeSky;

    public bool canChangeNight;

    private void Awake()
    {
        instance = this;
        canChangeSky = true;
    }

    void Start()
    {
        sky = GameObject.FindGameObjectWithTag("HotelDay");

        sky.GetComponent<Image>().sprite = day;


        check = GameObject.FindWithTag("Customer");

        SetDream();

        SetExitCustomer();

        SetRoomList();

        inDay = true;
        inNight = false;

        endTheDayButton = GameObject.FindWithTag("EndTheDayButton");
        buttonText = endTheDayButton.GetComponentInChildren<Text>();

        InDay = new DayTime();

    }

    private void Update()
    {

    }

    private void FindDream()
    {
        for (int i = 0; i < dreams.Count; i++)
        {
            if (dreams[i].transform.parent.GetComponentInChildren<CustomerSlot>().icon.sprite == dreams[i].transform.parent.GetComponentInChildren<CustomerSlot>().frog
                || dreams[i].transform.parent.GetComponentInChildren<CustomerSlot>().icon.sprite == dreams[i].transform.parent.GetComponentInChildren<CustomerSlot>().fox)
            {
                dreams[i].SetActive(true);
            }
        }
    }

    private void UnActiveDream()
    {
        for (int i = 0; i < dreams.Count; i++)
        {
            dreams[i].SetActive(false);
        }
    }

    private void SetDream() // 初始化梦网
    {
        dreams = new List<GameObject>();
        GameObject[] dms = GameObject.FindGameObjectsWithTag("梦网");

        foreach (var dream in dms)
        {
            dreams.Add(dream);
        }

        for (int i = 0; i < dreams.Count; i++)
        {
            dreams[i].SetActive(false);
        }

    }

    private void SetExitCustomer() // 初始化退房列表
    {
        exitCustomers = new List<Image>();
        GameObject[] cmt = GameObject.FindGameObjectsWithTag("Room");

        foreach (var exitCustomer in cmt)
        {
            Image exitIcon = exitCustomer.GetComponentInChildren<CustomerSlot>().icon;
            exitCustomers.Add(exitIcon);
        }
    }

    private void SetRoomList()  // 初始化房间列表
    {
        rooms = new List<GameObject>();
        GameObject[] rms = GameObject.FindGameObjectsWithTag("Room");

        foreach (var room in rms)
        {
            rooms.Add(room);
        }
    }


    //private void ExitRoom()  // 初始化退房住户
    //{
    //    int j = 0;
    //    for (int i = 0; i < rooms.Count; i++)
    //    {
    //        if (rooms[i].GetComponentInChildren<CustomerSlot>().icon.sprite == null)
    //        {
    //            j++;
    //        }
    //        // 计算每个顾客的位置
    //        Vector3 customerPosition = GameManager.instance.queuePosition.position + new Vector3(-GameManager.instance.customerSpacing * (i - j), 0, 0);

    //        if (rooms[i].GetComponentInChildren<CustomerSlot>().icon.sprite != null)  // 没住人的客房就不用出来住户了
    //        {
    //            var customerInstance = Instantiate(customerPrefab, customerPosition, Quaternion.identity);
    //            customerInstance.transform.SetParent(GameManager.instance.queuePosition, true);
    //            exitCustomers[i].sprite = rooms[i].GetComponentInChildren<CustomerSlot>().icon.sprite;
    //            customerInstance.GetComponent<CustomerDrag>().icon.sprite = exitCustomers[i].sprite;
    //            rooms[i].GetComponentInChildren<CustomerSlot>().icon.sprite = null;
    //        }

    //    }
    //}


    public void ChangeSky()
    {
        if (inDay && check.transform.childCount == 0 && GameManager.instance.ServeNext == false)
        {
            if (inDay && dayTime == 2 && !canChangeNight)
            {
                return;
            }
            else
            {
                DayManager.instance.startComeNewCustomer = false;
                StartCoroutine(FadeScreenToNight());
            }
        }

        else if (inNight && canChangeSky)
        {
            StartCoroutine(FadeScreenToDay());
            if (dayTime == 2)
            {
                StartCoroutine(FadeScreenToThanksScene());
            }
        }

    }

    public IEnumerator FadeScreenToNight()
    {
        endTheDayButton.SetActive(false);

        fadeScreen.FadeOut();

        yield return new WaitForSeconds(2f);

        sky.GetComponent<Image>().sprite = night;

        inDay = false;
        inNight = true;

        CustomerCheck.instance.rtf.anchoredPosition = new Vector2(CustomerCheck.instance.originalPosition.x, CustomerCheck.instance.originalPosition.y);

        GameManager.instance.targetPosition = CustomerCheck.instance.transform.position;

        CustomerCheck.instance.rtf.anchoredPosition = new Vector2(CustomerCheck.instance.originalPosition.x - GameManager.instance.customerSpacing, CustomerCheck.instance.originalPosition.y);

        fadeScreen.FadeIn();

        endTheDayButton.SetActive(true);

        FindDream();

        BackgroundMusicManager.instance.SwitchToMusic2();

    }

    public IEnumerator FadeScreenToDay()
    {
        endTheDayButton.SetActive(false);

        fadeScreen.FadeOut();

        yield return new WaitForSeconds(2f);

        sky.GetComponent<Image>().sprite = day;

        inDay = true;
        inNight = false;

        GameObject dt = GameObject.FindWithTag("DayTime");

        dayTime++;

        dt.transform.GetComponent<Text>().text = dayTime.ToString();

        StartCoroutine(WaitForExitRoomCustomer());

        fadeScreen.FadeIn();

        endTheDayButton.SetActive(true);

        UnActiveDream();

        BackgroundMusicManager.instance.SwitchToMusic1();
    }

    public IEnumerator FadeScreenToThanksScene()
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(toTanksScene);
    }

    public IEnumerator WaitForExitRoomCustomer()  // 只有在协程之中才能给 for 循环和一些其他操作添加缓冲
    {

        CustomerCheck.instance.rtf.anchoredPosition = new Vector2(CustomerCheck.instance.originalPosition.x, CustomerCheck.instance.originalPosition.y);

        GameManager.instance.targetPosition = CustomerCheck.instance.transform.position;

        CustomerCheck.instance.rtf.anchoredPosition = new Vector2(CustomerCheck.instance.originalPosition.x - GameManager.instance.customerSpacing * 1.5f, CustomerCheck.instance.originalPosition.y);

        int j = 0;

        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].GetComponentInChildren<CustomerSlot>().icon.sprite == null)
            {
                j++;
            }
            // 计算每个顾客的位置
            Vector3 customerPosition = GameManager.instance.queuePosition.position + new Vector3(-GameManager.instance.customerSpacing * (i - j), 0, 0);

            if (rooms[i].GetComponentInChildren<CustomerSlot>().icon.sprite != null)  // 没住人的客房就不用出来住户了
            {
                var customerInstance = Instantiate(customerPrefab, customerPosition, Quaternion.identity);
                exitCustomers[i].sprite = rooms[i].GetComponentInChildren<CustomerSlot>().icon.sprite;


                if (exitCustomers[i].sprite == customerInstance.GetComponent<CustomerDrag>().cow
                    && rooms[i].GetComponentInChildren<BedSlot>().icon.sprite == rooms[i].GetComponentInChildren<BedSlot>().MuBanChuang
                    || exitCustomers[i].sprite == customerInstance.GetComponent<CustomerDrag>().fox
                    && rooms[i].GetComponentInChildren<BedSlot>().icon.sprite == rooms[i].GetComponentInChildren<BedSlot>().MuBanChuang
                    || exitCustomers[i].sprite == customerInstance.GetComponent<CustomerDrag>().fish
                    && rooms[i].GetComponentInChildren<BedSlot>().icon.sprite == rooms[i].GetComponentInChildren<BedSlot>().YuGang
                    || exitCustomers[i].sprite == customerInstance.GetComponent<CustomerDrag>().frog
                    && rooms[i].GetComponentInChildren<BedSlot>().icon.sprite == rooms[i].GetComponentInChildren<BedSlot>().YuGang
                    || exitCustomers[i].sprite == customerInstance.GetComponent<CustomerDrag>().bird
                    && rooms[i].GetComponentInChildren<BedSlot>().icon.sprite == rooms[i].GetComponentInChildren<BedSlot>().NiaoChao)
                {
                    customerInstance.GetComponent<CustomerDrag>().isHappy = true;
                }

                customerInstance.GetComponent<CustomerDrag>().icon.sprite = exitCustomers[i].sprite;
                yield return new WaitForSeconds(.01f);
                customerInstance.GetComponent<CustomerDrag>().isServe = true;
                customerInstance.transform.SetParent(GameManager.instance.queuePosition, true);
                rooms[i].GetComponentInChildren<CustomerSlot>().icon.sprite = null;
                GameManager.instance.ServeNext = true;
                yield return new WaitForSeconds(2f);


            }
        }
    }
}



