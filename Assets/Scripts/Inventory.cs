using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    public GameObject inventoryPanel;
    public RectTransform SlotGrid;
    public GameObject slotPrefab;

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

    public void ShowInventory()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("inventoryPanel não está atribuído! Atribua o painel de inventário no Inspector do componente Inventory.");
            return;
        }
        
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);

        if (isActive == true)
        {
            UpdateInventory();
        }
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
}

