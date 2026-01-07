using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "ScriptableObject/item", order = 1)]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public ItemUse itemUse;
    public int itemAmount;
    public string itemName;
    public Sprite itemSprite;
    [TextArea(1, 4)]
    public string itemDescription; 
    public float energyRestore; // quantidade de energia que o item restaura (apenas para consumíveis)
    public GameObject lootPreFab;
    public int lootAmount;
    public bool isRecoverEnergy;
    public int EnergyAmount;
    public bool isRecoverMana;

    public bool isRecoverHP;
    public int HPAmount;

}
