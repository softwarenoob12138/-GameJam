using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

    public int goldCoinQuantity;

    private void Awake()
    {
        instance = this;
    }

    public void GetPrimaryGold()
    {
        goldCoinQuantity = 20;
    }

    public bool PurchaseItem(Item item)
    {
        if (goldCoinQuantity >= item.price)
        {
            goldCoinQuantity -= item.price;
            return true;
        }

        else
        {
            return false;
        }
    }

    public void GetAccount()
    {

    }
}
