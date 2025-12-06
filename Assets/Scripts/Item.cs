using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "ScriptableObject/item", order = 1)]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public int itemAmount;
    public string itemName;
    public Sprite itemSprite;
    [TextArea(1, 4)]
    public string itemDescription;
    public GameObject lootPreFab;
    public int lootAmount;

}
