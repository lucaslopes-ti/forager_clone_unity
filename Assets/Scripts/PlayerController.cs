using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movementInput;
    private Vector2 mousePosition;
    private bool isLookLeft;
    private bool isWalk;

    private bool isActionButton;
    
    [SerializeField] public float moveSpeed = 3f;

    private bool isAction;
    private float actionTimer;
    private float actionDuration = 0.5f; // Duração estimada da animação de ataque
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isLookLeft = transform.localScale.x < 0; // Inicializa baseado na escala inicial
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
        // Se o mouse está à direita e o personagem está olhando para a direita, vira para a esquerda
        if (mousePosition.x > transform.position.x && !isLookLeft)
        {
            Flip();
        }
        // Se o mouse está à esquerda e o personagem está olhando para a esquerda, vira para a direita
        else if (mousePosition.x < transform.position.x && isLookLeft)
        {
            Flip();
        }

        // Reset do timer de ação
        if (isAction)
        {
            actionTimer -= Time.deltaTime;
            if (actionTimer <= 0)
            {
                isAction = false;
            }
        }

        // Verifica se o botão foi pressionado (clique inicial)
        if (Input.GetButtonDown("Fire1") && !isAction)
        {
            isAction = true;
            isActionButton = true;
            actionTimer = actionDuration;
            animator.SetTrigger("Axe");
        }

        // Verifica se o botão está sendo segurado
        if (Input.GetButton("Fire1"))
        {
            isActionButton = true;
            // Se a ação terminou e o botão ainda está pressionado, inicia nova animação
            if (!isAction)
            {
                isAction = true;
                actionTimer = actionDuration;
                animator.SetTrigger("Axe");
            }
        }
        else
        {
            // Botão solto
            isActionButton = false;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (CoreGame._instance != null && CoreGame._instance.inventory != null)
            {
                // Chama ShowInventory usando reflection
                var showInventoryMethod = CoreGame._instance.inventory.GetType().GetMethod("ShowInventory");
                if (showInventoryMethod != null)
                {
                    showInventoryMethod.Invoke(CoreGame._instance.inventory, null);
                }
            }
        }


        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        // Bloqueia movimento durante a ação de ataque
        if (isActionButton || isAction)
        {
            movementInput = Vector2.zero;
            isWalk = false;
        }
        else
        {
            isWalk = movementInput.magnitude != 0;
        }

        if (animator != null)
        {
            animator.SetBool("isWalk", isWalk);
        }
    }

    private void Flip()
    {
        // Permite virar mesmo durante ações para melhor responsividade
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
        CoreGame._instance.gameManager.ObjectHit();
    }

    // FixedUpdate is called at a fixed interval and is used for physics
    void FixedUpdate()
    {
        // Bloqueia movimento durante ações (ataques)
        if (isActionButton || isAction)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            rb.linearVelocity = movementInput * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "loot":
                // Obtém o componente Loot usando o nome do tipo
                Component lootComp = collision.gameObject.GetComponent(typeof(MonoBehaviour));
                if (lootComp != null && lootComp.GetType().Name == "Loot")
                {
                    // Acessa a propriedade item via reflection
                    var itemField = lootComp.GetType().GetField("item");
                    if (itemField != null)
                    {
                        Item item = itemField.GetValue(lootComp) as Item;
                        if (item != null && CoreGame._instance != null)
                        {
                            // Tenta encontrar o Inventory automaticamente se não estiver atribuído
                            if (CoreGame._instance.inventory == null)
                            {
                                MonoBehaviour[] allMonoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
                                foreach (MonoBehaviour mb in allMonoBehaviours)
                                {
                                    if (mb != null && mb.GetType().Name == "Inventory")
                                    {
                                        CoreGame._instance.inventory = mb;
                                        break;
                                    }
                                }
                            }
                            
                            if (CoreGame._instance.inventory != null)
                            {
                                // Chama getItem usando reflection
                                var getItemMethod = CoreGame._instance.inventory.GetType().GetMethod("getItem");
                                if (getItemMethod != null)
                                {
                                    getItemMethod.Invoke(CoreGame._instance.inventory, new object[] { item, 1 });
                                    Destroy(collision.gameObject);
                                }
                            }
                            else
                            {
                                Debug.LogError("Inventory não encontrado! Certifique-se de que existe um objeto com o componente Inventory na cena.");
                            }
                        }
                        else if (item == null)
                        {
                            Debug.LogWarning("Item é null no Loot!");
                        }
                    }
                }
                break;
        }
    }
}