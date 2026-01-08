using UnityEngine;

public class IslandSlotGrid : MonoBehaviour
{
   public int line;
   public Collider2D col;
   public bool isBusy;

   public SpriteRenderer spriteRenderer;

   
   public void Busy(bool value)
   {
      isBusy = value;
      col.enabled = !isBusy;
      
   }

   public void ShowBorder(bool value)
   {
      spriteRenderer.enabled = value;
   }

   private void OnMouseDown()
   {
      if (CoreGame._instance.gameManager.gameState == GameState.Craft && isBusy == false)
      {
        CoreGame._instance.gameManager.SetCraftObject(this);
      }
   }
}
