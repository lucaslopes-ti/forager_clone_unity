using Unity.VisualScripting;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public IslandSlotGrid slot;
    public Item item;
    public int hitAmount;

    private void Start()
    {
        hitAmount = item.itemAmount;
    }

    public void SetSlot(IslandSlotGrid s)
    {
        slot = s;
        transform.position = slot.transform.position;
        GetComponent<SpriteRenderer>().sortingOrder = s.line;
        

    }
    private void OnMouseOver()
    {
        if (CoreGame._instance != null && CoreGame._instance.gameManager != null)
        {
            CoreGame._instance.gameManager.ActiveCursor(this.gameObject);
        }
    }

    private void OnMouseExit()
    {
        if (CoreGame._instance != null && CoreGame._instance.gameManager != null)
        {
            CoreGame._instance.gameManager.DisableCursor();
        }
    }

    private void OnHit()
    {
        hitAmount--;
       if (hitAmount <= 0)
        {
            CoreGame._instance.gameManager.Loot(item, transform.position);
            slot.Busy(false);
            Destroy(this.gameObject);
        }
    }
}
