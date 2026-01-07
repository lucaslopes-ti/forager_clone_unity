using UnityEngine;

public class IslandSlotGrid : MonoBehaviour
{
   public int line;
   public Collider2D col;
   public bool isBusy;
   
   public void Busy(bool value)
   {
      isBusy = value;
      col.enabled = !isBusy;
      
   }
}
