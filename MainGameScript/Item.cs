using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public int price;

    public Item(string name, int price)
    {
        this.name = name;
        this.price = price;
    }
}
