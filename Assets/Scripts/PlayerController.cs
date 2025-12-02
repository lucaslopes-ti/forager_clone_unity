using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movementInput;
    private Vector2 mousePosition;
    private bool isLookLeft;
    private bool isWalk;
    
    [SerializeField] public float moveSpeed = 3f;

    private bool isAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Converte a posição do mouse do espaço da tela para o espaço do mundo
        // Para câmeras 2D, usamos a distância da câmera até o plano do jogo
        float distanceFromCamera = Camera.main.transform.position.z - transform.position.z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));
        mousePosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
       
        // Verifica se precisa virar o personagem baseado na posição do mouse
        // Se o mouse está à direita e o personagem não está olhando para a direita, vira
        if (mousePosition.x > transform.position.x && !isLookLeft)
        {
            Flip();
        }
        // Se o mouse está à esquerda e o personagem está olhando para a direita, vira
        else if (mousePosition.x < transform.position.x && isLookLeft)
        {
            Flip();
        }

        if (Input.GetButtonDown("Fire1") && isAction == false)
        {
            isAction = true;
            animator.SetTrigger("Axe");
        }

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        isWalk = movementInput.magnitude != 0;

        if (animator != null)
        {
            animator.SetBool("isWalk", isWalk);
        }
    }

    private void Flip()
    {
        if (isAction == true)
        {
            return;
        }
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, 1, 1);  
    }

    private void ActionComplete()
    {
        isAction = false;
    }

    public void AxeHit()
    {

    }

    // FixedUpdate is called at a fixed interval and is used for physics
    void FixedUpdate()
    {
        // Permite movimento mesmo durante ações (ataques)
        rb.linearVelocity = movementInput * moveSpeed;
    }
}
