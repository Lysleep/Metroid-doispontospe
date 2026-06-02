using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    #region variaveis de movimento
    [SerializeField] private float moveSpeed = 5;
    //1 - Declarar o Input System
    private InputControls inputControls;
    
    //4 - Variável para receber input de movimentação
    private float InputDirectionX => inputControls.Player.Move.ReadValue<Vector2>().x;
    #endregion
    PlayerAnimController playerAnim;
    private SpriteRenderer spriteRenderer;
   
    private void Awake()
    {
        //2 - Inicializar o Input System
        inputControls = new InputControls();
        
        //3 - Habilitar o Input System
        inputControls.Enable();
        playerAnim = GetComponent<PlayerAnimController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CheckMovementForAnimation();
        FlipSpriteAccordingToInput();
        
        Vector2 moveDirection = new Vector2(InputDirectionX, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
       
    }

    void CheckMovementForAnimation()
    {
        if (InputDirectionX != 0f)
        {
            playerAnim.SetIsMovingParam(true);
        }
        else
        {
            playerAnim.SetIsMovingParam(false);
        }
    }

    void FlipSpriteAccordingToInput()
    {
        if (InputDirectionX > 0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (InputDirectionX < 0f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
