using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player2D : MonoBehaviour, ICanInteract
{
    [Header("Attributes")]
    [SerializeField] private float startHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkOffset = 0.25f;
    [SerializeField] private float checkRadius = 0.1f;

    private bool isGrounded;

    private CurrencyHandler healthHandler;
    private CurrencyHandler collectableHandler;
    public CurrencyHandler HealthHandler => healthHandler;
    public CurrencyHandler CollectableHandler => collectableHandler;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [Header("UI")]
    [SerializeField] private Slider healthbarSlider;
    [SerializeField] private TextMeshProUGUI collectableCounterTMP;

    private InputManager inputManager;
    private Vector2 moveInput;
    private Vector2 movePosition;

    private void Start()
    {
        inputManager = InputManager.instance;
        inputManager.OnDirectionalMovement += PlayerMovementInput;
        inputManager.OnJumpPressed += InputManager_OnJumpPressed;

        healthHandler = new CurrencyHandler(currentAmount: startHealth, maxAmount: startHealth);
        collectableHandler = new CurrencyHandler(0);

        healthbarSlider.maxValue = healthHandler.MaxValue;
        healthbarSlider.minValue = healthHandler.MinValue;
        healthbarSlider.value = healthHandler.Value;

        healthHandler.OnModified += (type, amount, current) => healthbarSlider.value = current;
        collectableHandler.OnModified += (type, amount, current) => collectableCounterTMP.text = current.ToString();

    }

    private void InputManager_OnJumpPressed()
    {

        if (isGrounded)
        {
            Debug.Log("Jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void PlayerMovementInput(Vector2 obj)
    {
        moveInput = obj;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position - (Vector2.up * checkOffset), checkRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        if (moveInput.magnitude > 0)
        {
            rb.velocity = new Vector2(moveInput.x * movementSpeed, rb.velocity.y);
            //rb.AddForce(Vector2.right * moveInput.x * movementSpeed, ForceMode2D.Force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position - (Vector2.up * checkOffset), checkRadius);
    }

    public void Interact(IInteractable interactable)
    {
        interactable.Interact(this);
    }
}
