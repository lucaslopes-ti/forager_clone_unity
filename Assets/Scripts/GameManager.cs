using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;

[Serializable]
public struct ResourceLoot
{
    public GameObject resource;
    public int amount;
}



public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public float interactionDistance;
    public GameObject actionCursor;
    [SerializeField]
    private GameObject interacionObject;

    public float TimeToDelete = 3f;
    
    [Header("Player Energy")]
    public float PlayerEnergyMax = 100f;
    public float PlayerEnergy = 100f;

    public int playerLevel;

    public float distanceToSpawnResource;
    public float timeToSpawnResource;

    public GameObject objectToCraft;


    
    public void ActiveCursor(GameObject obj)
    {

        if (gameState == GameState.Inventory)
        {
            return;
        }

        if (actionCursor == null)
        {
            Debug.LogError("actionCursor não foi atribuído no Inspector do GameManager!");
            return;
        }
        
        if (obj == null)
        {
            return;
        }

        interacionObject = obj;
        if (Vector2.Distance(CoreGame._instance.playerController.transform.position, interacionObject.transform.position) <= interactionDistance)
        {
            actionCursor.transform.position = obj.transform.position;
            actionCursor.SetActive(true);
        }
        
    }

    public void DisableCursor()
    {
        

        if (actionCursor == null)
        {
            return;
        }
        
        actionCursor.SetActive(false);
        interacionObject = null;
    }

    public void ObjectHit()
    {
        if (interacionObject == null)
        {
            return;
        }
        
        if (actionCursor.activeSelf == true)
        {
            interacionObject.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
        }

        
    }

    void Start()
    {
        // Inicializa a energia do jogador
        PlayerEnergy = PlayerEnergyMax;
    }

    void FixedUpdate()
    {
        if (interacionObject != null)
        {
             if (Vector2.Distance(CoreGame._instance.playerController.transform.position, interacionObject.transform.position) <= interactionDistance)
            {
                actionCursor.SetActive(true);
            }
            else
            {
                actionCursor.SetActive(false);
            }

        }
    }

    public void AddEnergy(float amount)
    {
        PlayerEnergy += amount;
        if (PlayerEnergy > PlayerEnergyMax)
        {
            PlayerEnergy = PlayerEnergyMax;
        }
        Debug.Log($"Energia restaurada! Energia atual: {PlayerEnergy}/{PlayerEnergyMax}");
    }

    public void Loot(Item item, Vector3 position)
    {
        if (item == null)
        {
            Debug.LogError("Item é null!");
            return;
        }
        
        if (item.lootPreFab == null)
        {
            Debug.LogError("lootPreFab não está atribuído no Item!");
            return;
        }
        
        Debug.Log($"Gerando loot: {item.lootAmount} itens do tipo {item.itemName}");
        Debug.Log($"Prefab do loot: {(item.lootPreFab != null ? item.lootPreFab.name : "NULL")}");
        
        if (item.lootPreFab == null)
        {
            Debug.LogError($"lootPreFab não está atribuído no Item {item.itemName}!");
            return;
        }
        
        DisableCursor();
        int dir = -1;
        for (int i = 0; i < item.lootAmount; i++)
        {
            // Adiciona um pequeno offset aleatório para evitar sobreposição
            Vector3 spawnPosition = position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(0f, 0.1f), 0f);
            GameObject loot = Instantiate(item.lootPreFab, spawnPosition, transform.rotation);
            
            // Verifica se o objeto instanciado tem o componente Loot
            Component lootComponent = loot.GetComponent(typeof(MonoBehaviour));
            if (lootComponent != null && lootComponent.GetType().Name == "Loot")
            {
                loot.SendMessage("SetItem", item, SendMessageOptions.DontRequireReceiver);
                loot.SendMessage("Active", dir, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.LogWarning($"O prefab {item.lootPreFab.name} não tem o componente Loot! Verifique se está usando o prefab correto do loot.");
            }
            dir *= -1;
        }
    }

    public void ChangeGameState(GameState newState)
    {
        gameState = newState;
        switch (gameState)
        {
            case GameState.Inventory:
                actionCursor.SetActive(false);
                interacionObject = null;
                break;
            case GameState.Gameplay:
                break;

            case GameState.Craft:
                break;
        }
    }

    public bool PlayerDistance(Vector3 position)
    {
        float distance = Vector3.Distance(CoreGame._instance.playerController.transform.position, position);
        bool isReady = distance >= distanceToSpawnResource;
        return isReady;
    }

    public void SetCraftObject(IslandSlotGrid slot)
    {
        GameObject obj = Instantiate(objectToCraft);
        obj.transform.position = slot.transform.position;
        slot.isBusy = true;
        slot.ShowBorder(true);
        
        ChangeGameState(GameState.Gameplay);    
    }

}
