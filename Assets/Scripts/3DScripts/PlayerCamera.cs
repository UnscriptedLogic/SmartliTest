using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseSens = 100f;
    [SerializeField] private Vector2 rotationClamp = new Vector2(-90f, 90f);
    [SerializeField] private Transform horizontalRotate;
    [SerializeField] private Transform cameraParent;

    private float xRotation = 0f;
    private Vector2 mousePos;
    private InputManager inputManager;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inputManager = InputManager.instance;
        inputManager.OnMouseMoving += OnMouseMoving;
    }

    private void OnMouseMoving(Vector2 position, Vector2 delta)
    {
        mousePos = delta;
    }

    private void Update()
    {
        float mouseX = mousePos.x * mouseSens * Time.deltaTime;
        float mouseY = mousePos.y * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, rotationClamp.x, rotationClamp.y);
        cameraParent.localRotation = Quaternion.Euler(xRotation, 0, 0);
        horizontalRotate.Rotate(Vector3.up * mouseX);
    }
}