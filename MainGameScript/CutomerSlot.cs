using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomerSlot : MonoBehaviour
{
    public Sprite cow;
    public Sprite fish;
    public Sprite frog;
    public Sprite bird;
    public Sprite fox;

    public Image icon; // 显示在槽上的图标
    

    private void Update()
    {
        if (icon.sprite == null)
        {
            Color currentColor = icon.color;
            currentColor.a = 0f;

            Color currentColorDropDown = transform.GetChild(0).GetComponent<Image>().color;
            currentColorDropDown.a = 1f;

            transform.GetChild(0).GetComponent<Image>().color = currentColorDropDown;
            icon.color = currentColor;

        }
        else
        {
            Color currentColor = icon.color;
            currentColor.a = 1f;

            Color currentColorDropDown = transform.GetChild(0).GetComponent<Image>().color;
            currentColorDropDown.a = 0f;

            transform.GetChild(0).GetComponent<Image>().color = currentColorDropDown;

            icon.color = currentColor;

        }
    }

}
