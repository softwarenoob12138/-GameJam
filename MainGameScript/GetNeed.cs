using UnityEngine;
using UnityEngine.UI;

public class GetNeed : MonoBehaviour
{

    public static GetNeed instance;

    public Sprite MuBanChuang;
    public Sprite YuGang;

    public Image need1;
    public Image need2;
    public Image need3;

    public bool gameStart;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Color currentColor = need1.color;
        currentColor.a = 0f;

        need1.color = currentColor;
        need3.color = currentColor;

    }
    private void OnEnable()
    {
        if (gameStart)
        {
            if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().cow
                || CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().fox)
            {
                need2.sprite = ShopManager.instance.MuBanChuang;
            }

            if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().fish
                || CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().frog)
            {
                need2.sprite = ShopManager.instance.YuGang;
            }

            if (CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().icon.sprite == CustomerCheck.instance.transform.GetChild(0).GetComponent<CustomerDrag>().bird)
            {
                need2.sprite = ShopManager.instance.NiaoChao;
            }

        }
    }
}
