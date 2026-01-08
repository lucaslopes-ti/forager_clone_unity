using UnityEngine;

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