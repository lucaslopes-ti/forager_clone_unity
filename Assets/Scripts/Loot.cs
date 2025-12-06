using UnityEngine;

public class Loot : MonoBehaviour
{

    public Item item;
    private float startYPosition;
    private Rigidbody2D rb;
    private bool isActive;
    public Collider2D col;
    
    void Update()
    {
        if (isActive == true && transform.position.y < startYPosition - (Random.Range(0.2f, 0.6f)))
        {
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
            col.enabled = true;
            isActive = false;
        }
    }

    void Active(int dir)
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
        startYPosition = transform.position.y;
        rb.gravityScale = 1.8f;
        // Aplica força vertical para cima e força horizontal baseada na direção
        Vector2 force = new Vector2(Random.Range(20, 35) * dir, 250);
        rb.AddForce(force);
        isActive = true;
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    public Item GetItem()
    {
        return item;
    }
}
