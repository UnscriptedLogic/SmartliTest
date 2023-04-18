using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, ICanInteract
{
    [Header("Attributes")]
    [SerializeField] private float startHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool canCollectItems = true;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundcheckOffset;
    [SerializeField] private float groundcheckRadius;

    [Header("Components")]
    [SerializeField] private Rigidbody rb;

    [Header("UI")]
    [SerializeField] private Slider healthbarSlider;
    [SerializeField] private TextMeshProUGUI collectableCounterTMP;

    private InputManager inputManager;
    private CurrencyHandler healthHandler;
    private CurrencyHandler collectableHandler;
    private Vector2 inputDirection;

    public CurrencyHandler HealthHandler => healthHandler;
    public CurrencyHandler CollectableHandler => collectableHandler;

    private void Start()
    {
        inputManager = InputManager.instance;
        inputManager.OnDirectionalMovement += PlayerMove;
        inputManager.OnJumpPressed += PlayerJump;

        healthHandler = new CurrencyHandler(currentAmount: startHealth, maxAmount: startHealth);
        collectableHandler = new CurrencyHandler(0);

        healthbarSlider.maxValue = healthHandler.MaxValue;
        healthbarSlider.minValue = healthHandler.MinValue;
        healthbarSlider.value = healthHandler.Value;

        healthHandler.OnModified += (type, amount, current) => healthbarSlider.value = current;
        collectableHandler.OnModified += (type, amount, current) => collectableCounterTMP.text = current.ToString();
    }

    private void PlayerJump()
    {
        if (Physics.OverlapSphere(transform.position - Vector3.up * groundcheckOffset, groundcheckRadius, groundLayer).Length > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (inputDirection.magnitude > 0)
        {
            Vector3 moveDirection = transform.forward * inputDirection.y + transform.right * inputDirection.x;
            rb.MovePosition(transform.position + (moveDirection * movementSpeed * Time.deltaTime));
        }
    }

    private void PlayerMove(Vector2 obj) => inputDirection = obj;

    public void Interact(IInteractable collectable)
    {
        if (canCollectItems)
            collectable.Interact(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position - Vector3.up * groundcheckOffset, groundcheckRadius);
    }
}
