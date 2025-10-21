using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina;
    [SerializeField] private float staminaRegenRate = 5f;
    [SerializeField] private float staminaDrainRate = 25f;
    [SerializeField] private float attackDrain = 10f;
    [SerializeField] private float drinkRestoreAmount = 30f;

    [SerializeField] private float sprintMultiplier = 1.8f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [SerializeField] private Slider staminaBar;
    [SerializeField] private CanvasGroup staminaCanvasGroup;
    [SerializeField] private float fadeSpeed = 5f;

    private PlayerMovement playerMovement;
    private Rigidbody rb;
    private bool isSprinting = false;
    private bool canSprint = true;
    
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }

    
    void Update()
    {
        HandleSprint();
        RegenerateStamina();
    }

    public void HandleSprint()
    {
        bool sprintKeyPressed = Input.GetKey(sprintKey);
        bool isMoving = rb.linearVelocity.magnitude > 0.1f && playerMovement != null;

        if (sprintKeyPressed && isMoving && canSprint && currentStamina > 0)

        {
            if (!isSprinting)
            {
                isSprinting = true;
                playerMovement.moveSpeed *= sprintMultiplier;
            }

            currentStamina -= staminaDrainRate * Time.deltaTime;
        }
        else if (isSprinting)
        {
            isSprinting = false;
            playerMovement.moveSpeed /= sprintMultiplier;
        }

        if (currentStamina <= 0)
        {
            currentStamina = 0;
            canSprint = false;

            if (isSprinting)
            {
                isSprinting = false;
                playerMovement.moveSpeed /= sprintMultiplier;
            }
            UpdateStaminaUI();

        }
    }

    public void RegenerateStamina()
    {
        if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
            
            if (currentStamina > 20)
                canSprint = true;
           
        }
        UpdateStaminaUI();
    }

    public void SpendStaminaOnAttack()
    {
        currentStamina -= attackDrain;
        if (currentStamina < 0)
            currentStamina = 0;

        canSprint = currentStamina > 5f;

        UpdateStaminaUI();
    }

    public void RestoreStaminaByDrink(float amount)
        {
        currentStamina += amount;
        if (currentStamina > maxStamina)
            currentStamina = maxStamina;
        
        UpdateStaminaUI();
    }

    private void UpdateStaminaUI( bool instant = false)
    {
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina / maxStamina;
        }

        if (staminaCanvasGroup != null)
        {
            float targetAlpha = currentStamina < maxStamina ? 1.0f : 0.0f;
            if (instant)
                staminaCanvasGroup.alpha = targetAlpha;
            else
                staminaCanvasGroup.alpha = Mathf.Lerp(staminaCanvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
        }
    }

    public void FadeStaminaBar()
    {
        if (staminaCanvasGroup != null)
        {
            float targetAlpha = currentStamina < maxStamina ? 1f : 0f;
            staminaCanvasGroup.alpha = Mathf.Lerp(staminaCanvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
        }
    }
}
