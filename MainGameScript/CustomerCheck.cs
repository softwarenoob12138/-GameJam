using System.Collections;
using UnityEngine;

public class CustomerCheck : MonoBehaviour
{
    public static CustomerCheck instance;
    public Vector2 originalPosition;
    public RectTransform rtf;

    private void Awake()
    {

        instance = this;

        rtf = GetComponent<RectTransform>();
        originalPosition = rtf.anchoredPosition;

        // originalPosition = transform.position;
    }
    private void Start()
    {
        // StartCoroutine(DoCheck());
        StartCoroutine(WaitToSetServe());
        StartCoroutine(WaitToSetPosition());
    }

    private void Update()
    {

    }

    public IEnumerator CheckCustomer()
    {

        yield return new WaitForSeconds(.001f);

        if (transform.childCount > 0)  // 这行代码 确保至少有一个子对象存在
        {
            if (transform.GetChild(0) != null)
            {
                transform.GetChild(0).GetComponent<CustomerDrag>().icon.raycastTarget = true;
            }
        }

    }

    private IEnumerator DoCheck()
    {
        yield return new WaitForSeconds(.1f);

        StartCoroutine(CheckCustomer());


    }

    private IEnumerator WaitToSetPosition()
    {
        yield return new WaitForSeconds(.11f);
        rtf.anchoredPosition = new Vector2(originalPosition.x - GameManager.instance.customerSpacing * 1.5f, originalPosition.y);
        GameManager.instance.ServeNext = true;
        GameManager.instance.needs.SetActive(false);
        GameManager.instance.moods.SetActive(false);
        GameManager.instance.dialog.SetActive(false);
        if (transform.childCount > 0)  // 这行代码 确保至少有一个子对象存在
        {
            if (transform.GetChild(0) != null)
            {
                transform.GetChild(0).GetComponent<CustomerDrag>().icon.raycastTarget = false;
            }
        }
    }

    private IEnumerator WaitToSetServe()
    {
        yield return new WaitForSeconds(.12f);
        GameManager.instance.ServeNext = true;
        GameManager.instance.needs.SetActive(false);
        GameManager.instance.moods.SetActive(false);
        GameManager.instance.dialog.SetActive(false);
    }
}
