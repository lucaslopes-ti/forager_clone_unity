using UnityEngine;
using System;

public enum ItemType
{
    Wood, Coal, Iron, Stone, Fruit // nomes dos itens

}

public enum ItemUse
{
    Material, Consumable
}

public enum GameState
{
    Gameplay, Inventory, Craft
}

[Serializable]
public struct ResourceLoot
{
    public GameObject resource;
    public int amount;
}

[Serializable]
public struct Recipe
{
    public Item item;
    public int amount;
}

[Serializable]
public struct RecipeIsReady
{
    public Craft recipe;
    public bool isReady;
}

public class CoreGame : MonoBehaviour
{
    public static CoreGame _instance;
    public PlayerController playerController;

    public GameManager gameManager;
    public MonoBehaviour inventory;
    void Awake()
    {
        _instance = this;
        
        // Busca automaticamente o Inventory se não estiver atribuído
        if (inventory == null)
        {
            MonoBehaviour[] allMonoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            foreach (MonoBehaviour mb in allMonoBehaviours)
            {
                if (mb != null && mb.GetType().Name == "Inventory")
                {
                    inventory = mb;
                    break;
                }
            }
            if (inventory == null)
            {
                Debug.LogWarning("Inventory não encontrado! Certifique-se de que existe um objeto com o componente Inventory na cena.");
            }
        }
    }

} 