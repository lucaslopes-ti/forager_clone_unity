using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    public Image itemImage;
    public Text amountTxt;

    
    private bool isDelete;
    public Image deleteBar;
    private float deltatime;

    private float perc;


    private void Update()
    {
        if (isDelete == true)
        {
            deltatime += Time.deltaTime;
            perc = deltatime / CoreGame._instance.gameManager.TimeToDelete;
            deleteBar.fillAmount = perc;

            if (deltatime >= CoreGame._instance.gameManager.TimeToDelete)
            {
                if (CoreGame._instance != null && CoreGame._instance.inventory != null && item != null)
                {
                    var deleteItemMethod = CoreGame._instance.inventory.GetType().GetMethod("DeleteItem");
                    if (deleteItemMethod != null)
                    {
                        deleteItemMethod.Invoke(CoreGame._instance.inventory, new object[] { item });
                    }
                }
            }
        }
    }
    public void UpdateSlot(Item i, int amount)
    {
        deleteBar.gameObject.SetActive(false);
        item = i;
        itemImage.sprite = item.itemSprite;
        amountTxt.text = amount.ToString();
    }

    public void OnSlotClick(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        if (item.itemUse == ItemUse.Consumable)
        {
            if (CoreGame._instance != null && CoreGame._instance.inventory != null && item != null)
            {
                var useItemMethod = CoreGame._instance.inventory.GetType().GetMethod("UseItem");
                if (useItemMethod != null)
                {
                    useItemMethod.Invoke(CoreGame._instance.inventory, new object[] { item });
                }
            }
        }

        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            if (CoreGame._instance != null && CoreGame._instance.inventory != null && item != null)
            {
                var showItemInfoMethod = CoreGame._instance.inventory.GetType().GetMethod("ShowItemInfo");
                if (showItemInfoMethod != null)
                {
                    showItemInfoMethod.Invoke(CoreGame._instance.inventory, new object[] { item });
                }
            }
        }
        else if (pointerData.button == PointerEventData.InputButton.Right)
        {
            isDelete = true;
            if (CoreGame._instance != null && CoreGame._instance.inventory != null)
            {
                var disableItemInfoMethod = CoreGame._instance.inventory.GetType().GetMethod("DisableItemInfo");
                if (disableItemInfoMethod != null)
                {
                    disableItemInfoMethod.Invoke(CoreGame._instance.inventory, null);
                }
            }
            deltatime = 0;
            deleteBar.fillAmount = 0.1f;
            deleteBar.gameObject.SetActive(true);
        }
    }

    public void OnSlotUp(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            isDelete = false;
        }
    }

    public void MouseEnter()
    {
        if (CoreGame._instance != null && CoreGame._instance.inventory != null && item != null)
        {
            var showItemInfoMethod = CoreGame._instance.inventory.GetType().GetMethod("ShowItemInfo");
            if (showItemInfoMethod != null)
            {
                showItemInfoMethod.Invoke(CoreGame._instance.inventory, new object[] { item });
            }
        }
    }

    public void MouseExit()
    {
        if (CoreGame._instance != null && CoreGame._instance.inventory != null)
        {
            var disableItemInfoMethod = CoreGame._instance.inventory.GetType().GetMethod("DisableItemInfo");
            if (disableItemInfoMethod != null)
            {
                disableItemInfoMethod.Invoke(CoreGame._instance.inventory, null);
            }
        }

        isDelete = false;
    }
    
}
