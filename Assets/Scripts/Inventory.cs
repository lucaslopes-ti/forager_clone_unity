using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    public GameObject inventoryPanel;
    public GameObject[] SubPanel;
    public int idSubPanel;
    public RectTransform SlotGrid;
    public GameObject slotPrefab;

    
    [Header("Item Info")]
    public GameObject ItemInfoWindow;
    public Image ItemImage;
    public Text ItemName;
    public Text ItemType;
    public Text ItemUseText;

    private List<GameObject> inventorySlots = new List<GameObject>();
    //public Dictionary<Item, GameObject> inventorySlots = new Dictionary<Item, GameObject>();


    public void getItem(Item item, int amount)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += amount;
        }
        else
        {
            inventory.Add(item, amount);
        }
    }

    public void UseItem(Item item)
    {
        if (item.itemUse == ItemUse.Consumable)
        {
            // Restaura energia do jogador se o item tiver energia para restaurar
            if (item.energyRestore > 0 && CoreGame._instance != null && CoreGame._instance.gameManager != null)
            {
                CoreGame._instance.gameManager.AddEnergy(item.energyRestore);
            }
        }
        
        int amount = inventory[item];
        amount -= 1;
        if (amount <= 0)
        {
            DeleteItem(item);
        }
        else if (amount > 0)
        {
            inventory[item] = amount;
            UpdateInventory();
        }
    }

    public void ShowInventory()
    {
        InventoryTabs(0);
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);

        if (isActive == true)
        {
            CoreGame._instance.gameManager.ChangeGameState(GameState.Inventory);
            UpdateInventory();
        }
        else
        {
            CoreGame._instance.gameManager.ChangeGameState(GameState.Gameplay);
        }
    }

    public void DeleteItem(Item item)
    {
        inventory.Remove(item);
        UpdateInventory();
        DisableItemInfo();
    }

     public void DisableItemInfo()
    {
        ItemInfoWindow.SetActive(false);
    }

    void UpdateInventory()
    {
        foreach (GameObject s in inventorySlots)
        {
            Destroy(s);
        }

        inventorySlots.Clear();
        
        foreach (KeyValuePair<Item, int> item in inventory)
        {
            GameObject newSlot = Instantiate(slotPrefab, SlotGrid);
            inventorySlots.Add(newSlot);
            newSlot.GetComponent<InventorySlot>().UpdateSlot(item.Key, item.Value);
        }
    }

    public void ShowItemInfo(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Item é null no ShowItemInfo!");
            return;
        }

        if (ItemImage != null)
        {
            ItemImage.sprite = item.itemSprite;
        }

        if (ItemName != null)
        {
            ItemName.text = item.itemName;
        }

        if (ItemUseText != null)
        {
            ItemUseText.text = item.itemDescription ?? "";
        }

        if (ItemType != null)
        {
            ItemType.text = item.itemUse.ToString();
        }

        if (ItemInfoWindow != null)
        {
            ItemInfoWindow.SetActive(true);
        }
        else
        {
            Debug.LogError("ItemInfoWindow não está atribuído no Inspector do Inventory!");
        }
    }

    public void InventoryTabs(int idTab)
    {
        foreach (GameObject t in SubPanel)
        {
            t.SetActive(false);
        }
        SubPanel[idTab].SetActive(true);

    }


   
}

