using UnityEngine;
using System;


public class OrderInLayer : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private float playerY;
    public float offset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    public void LateUpdate()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        playerY = CoreGame._instance.playerController.positionY;
        if (transform.position.y < playerY - offset)
        {
            spriteRenderer.sortingLayerName = "PrimeiroPlano";
        }
        else
        {
            spriteRenderer.sortingLayerName = "SegundoPlano";
        }
    }
}
