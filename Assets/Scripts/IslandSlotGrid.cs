using UnityEngine;

public class IslandSlotGrid : MonoBehaviour
{
   public int line;
   public Collider2D col;
   public bool isBusy;

   public SpriteRenderer spriteRenderer;

   void Awake()
   {
      // Busca automaticamente o SpriteRenderer se não estiver atribuído
      if (spriteRenderer == null)
      {
         spriteRenderer = GetComponent<SpriteRenderer>();
         if (spriteRenderer == null)
         {
            Debug.LogWarning($"SpriteRenderer não encontrado no GameObject {gameObject.name}. O ShowBorder não funcionará.");
         }
      }
      
      // Busca automaticamente o Collider2D se não estiver atribuído
      if (col == null)
      {
         col = GetComponent<Collider2D>();
      }
   }
   
   public void Busy(bool value)
   {
      isBusy = value;
      if (col != null)
      {
         col.enabled = !isBusy;
      }
      
   }

   public void ShowBorder(bool value)
   {
      if (spriteRenderer != null)
      {
         spriteRenderer.enabled = value;
      }
      else
      {
         Debug.LogWarning($"SpriteRenderer não está atribuído no {gameObject.name}. Não é possível mostrar/esconder a borda.");
      }
   }

   private void OnMouseDown()
   {
      if (CoreGame._instance.gameManager.gameState == GameState.Craft && isBusy == false)
      {
        CoreGame._instance.gameManager.SetCraftObject(this);
      }
   }
}
