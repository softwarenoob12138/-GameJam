using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject shop;
    public GoldManager goldManager;
    public List<Item> items;
    public Button buyMuBanChuang;
    public Button buyNiaoChao;
    public Button buyYuGang;
    public Button buyXiaoTiQin;
    public Button buyHuaPing;
    public Button buyWangYuanJing;
    public Button buyMaoTouYingHuaXiang;
    public Button buyMaoTan;

    public bool isBuyMuBanChuang;
    public bool isBuyNiaoChao;
    public bool isBuyYuGang;
    public bool isBuyXiaoTiQin;
    public bool isBuyHuaPing;
    public bool isBuyWangYuanJing;
    public bool isBuyMaoTouYingHuaXiang;
    public bool isBuyMaoTan;

    public Sprite MuBanChuang;
    public Sprite NiaoChao;
    public Sprite YuGang;
    public Sprite XiaoTiQin;
    public Sprite HuaPing;
    public Sprite WangYuanJing;
    public Sprite MaoTouYingHuaXiang;
    public Sprite MaoTan;

    public GameObject IsBuyMuBanChuang;
    public GameObject IsBuyNiaoChao;
    public GameObject IsBuyYuGang;
    public GameObject IsBuyXiaoTiQin;
    public GameObject IsBuyHuaPing;
    public GameObject IsBuyWangYuanJing;
    public GameObject IsBuyMaoTouYingHuaXiang;
    public GameObject IsBuyMaoTan;


    private void Awake()
    {
        instance = this;
        shop = GameObject.FindGameObjectWithTag("Shop");
        IsBuyMuBanChuang.SetActive(false);
        IsBuyNiaoChao.SetActive(false);
        IsBuyYuGang.SetActive(false);
        IsBuyXiaoTiQin.SetActive(false);
        IsBuyHuaPing.SetActive(false);
        IsBuyWangYuanJing.SetActive(false);
        IsBuyMaoTouYingHuaXiang.SetActive(false);
        IsBuyMaoTan.SetActive(false);
    }

    private void Start()
    {
        goldManager = FindObjectOfType<GoldManager>();

        // Ê¾ÀýÉÌÆ·ÁÐ±í
        items = new List<Item>
        {
            new("Ä¾°å´²", 10),
            new("Äñ³²", 10),
            new("Ô¡¸×", 10),
            new("Ð¡ÌáÇÙ", 10),
            new("»¨Æ¿", 10),
            new("ÍûÔ¶¾µ", 10),
            new("Ã¨Í·Ó¥»­Ïñ", 10),
            new("Ã«Ìº", 10),
        };

    }

    public void GoToShop()
    {
        shop.SetActive(true);
    }

    public void ExitShop()
    {
        shop.SetActive(false);
    }

    public void UpdateGoldDisplay()
    {
        GameObject.FindGameObjectWithTag("Coin").transform.GetComponent<Text>().text = goldManager.goldCoinQuantity.ToString();
    }

    public void BuyMuBanChuang()
    {
        if (!isBuyMuBanChuang)
        {
            if (goldManager.PurchaseItem(items[0]))
            {
                buyMuBanChuang.GetComponentInChildren<Image>().transform.GetComponentInChildren<Image>().transform.gameObject.SetActive(false);
                isBuyMuBanChuang = true;
                UpdateGoldDisplay();
                IsBuyMuBanChuang.SetActive(true);
                for (int i = 0; i < InventoryManagerInMainGame.instance.inventorySlots.Length; i++)
                {
                    if (InventoryManagerInMainGame.instance.inventorySlots[i].sprite == null)
                    {
                        InventoryManagerInMainGame.instance.inventorySlots[i].sprite = MuBanChuang;
                        break;
                    }
                }
            }
        }
    }

    public void BuyNiaoChao()
    {
        if (!isBuyNiaoChao)
        {
            if (goldManager.PurchaseItem(items[1]))
            {
                buyNiaoChao.GetComponentInChildren<Image>().transform.gameObject.SetActive(false);
                isBuyNiaoChao = true;
                UpdateGoldDisplay();
                IsBuyNiaoChao.SetActive(true);
                for (int i = 0; i < InventoryManagerInMainGame.instance.inventorySlots.Length; i++)
                {
                    if (InventoryManagerInMainGame.instance.inventorySlots[i].sprite == null)
                    {
                        InventoryManagerInMainGame.instance.inventorySlots[i].sprite = NiaoChao;
                        break;
                    }
                }
            }
        }
    }

    public void BuyYuGang()
    {
        if (!isBuyYuGang)
        {
            if (goldManager.PurchaseItem(items[2]))
            {
                buyYuGang.GetComponentInChildren<Image>().transform.gameObject.SetActive(false);
                isBuyYuGang = true;
                UpdateGoldDisplay();
                IsBuyYuGang.SetActive(true);
                for (int i = 0; i < InventoryManagerInMainGame.instance.inventorySlots.Length; i++)
                {
                    if (InventoryManagerInMainGame.instance.inventorySlots[i].sprite == null)
                    {
                        InventoryManagerInMainGame.instance.inventorySlots[i].sprite = YuGang;
                        break;
                    }
                }
            }
        }
    }

    public void BuyXiaoTiQin()
    {
        if (!isBuyXiaoTiQin)
        {
            if (goldManager.PurchaseItem(items[3]))
            {
                buyXiaoTiQin.GetComponentInChildren<Image>().transform.gameObject.SetActive(false);
                isBuyXiaoTiQin = true;
                UpdateGoldDisplay();
                IsBuyXiaoTiQin.SetActive(true);
                for (int i = 0; i < InventoryManagerInMainGame.instance.inventorySlots.Length; i++)
                {
                    if (InventoryManagerInMainGame.instance.inventorySlots[i].sprite == null)
                    {
                        InventoryManagerInMainGame.instance.inventorySlots[i].sprite = XiaoTiQin;
                        break;
                    }
                }
            }
        }
    }

    public void BuyHuaPing()
    {
        if (!isBuyHuaPing)
        {
            if (goldManager.PurchaseItem(items[4]))
            {
                buyHuaPing.GetComponentInChildren<Image>().transform.gameObject.SetActive(false);
                isBuyHuaPing = true;
                UpdateGoldDisplay();
                IsBuyHuaPing.SetActive(true);
                for (int i = 0; i < InventoryManagerInMainGame.instance.inventorySlots.Length; i++)
                {
                    if (InventoryManagerInMainGame.instance.inventorySlots[i].sprite == null)
                    {
                        InventoryManagerInMainGame.instance.inventorySlots[i].sprite = HuaPing;
                        break;
                    }
                }
            }
        }
    }
    public void BuyWangYuanJing()
    {
        if (!isBuyWangYuanJing)
        {
            if (goldManager.PurchaseItem(items[5]))
            {
                buyWangYuanJing.GetComponentInChildren<Image>().transform.gameObject.SetActive(false);
                isBuyWangYuanJing = true;
                UpdateGoldDisplay();
                IsBuyWangYuanJing.SetActive(true);
                for (int i = 0; i < InventoryManagerInMainGame.instance.inventorySlots.Length; i++)
                {
                    if (InventoryManagerInMainGame.instance.inventorySlots[i].sprite == null)
                    {
                        InventoryManagerInMainGame.instance.inventorySlots[i].sprite = WangYuanJing;
                        break;
                    }
                }
            }
        }
    }

    public void BuyMaoTouYingHuaXiang()
    {
        if (!isBuyMaoTouYingHuaXiang)
        {
            if (goldManager.PurchaseItem(items[6]))
            {
                buyMaoTouYingHuaXiang.GetComponentInChildren<Image>().transform.gameObject.SetActive(false);
                isBuyMaoTouYingHuaXiang = true;
                UpdateGoldDisplay();
                IsBuyMaoTouYingHuaXiang.SetActive(true);
                for (int i = 0; i < InventoryManagerInMainGame.instance.inventorySlots.Length; i++)
                {
                    if (InventoryManagerInMainGame.instance.inventorySlots[i].sprite == null)
                    {
                        InventoryManagerInMainGame.instance.inventorySlots[i].sprite = MaoTouYingHuaXiang;
                        break;
                    }
                }
            }
        }
    }

    public void BuyMaoTan()
    {
        if (!isBuyMaoTan)
        {
            if (goldManager.PurchaseItem(items[7]))
            {
                buyMaoTan.GetComponentInChildren<Image>().transform.gameObject.SetActive(false);
                isBuyMaoTan = true;
                UpdateGoldDisplay();
                IsBuyMaoTan.SetActive(true);
                for (int i = 0; i < InventoryManagerInMainGame.instance.inventorySlots.Length; i++)
                {
                    if (InventoryManagerInMainGame.instance.inventorySlots[i].sprite == null)
                    {
                        InventoryManagerInMainGame.instance.inventorySlots[i].sprite = MaoTan;
                        break;
                    }
                }
            }
        }
    }

}
