using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float interactionDistance;
    public GameObject actionCursor;
    [SerializeField]
    private GameObject interacionObject;
    public void ActiveCursor(GameObject obj)
    {
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
        
        DisableCursor();
        int dir = -1;
        for (int i = 0; i < item.lootAmount; i++)
        {
            // Adiciona um pequeno offset aleatório para evitar sobreposição
            Vector3 spawnPosition = position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0f, 0.1f), 0f);
            GameObject loot = Instantiate(item.lootPreFab, spawnPosition, transform.rotation);
            loot.SendMessage("SetItem", item, SendMessageOptions.DontRequireReceiver);
            loot.SendMessage("Active", dir, SendMessageOptions.DontRequireReceiver);
            dir *= -1;
        }
    }

}
