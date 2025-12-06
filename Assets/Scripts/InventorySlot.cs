using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    public Image itemImage;
    public Text amountTxt;
    
    public void UpdateSlot(Item i, int amount)
    {
        item = i;
        itemImage.sprite = item.itemSprite;
        amountTxt.text = amount.ToString();
    }

    public void OnSlotClick()
    {

    }

    public void MouseEnter()
    {
        
    }

    public void MouseExit()
    {

    }
}
