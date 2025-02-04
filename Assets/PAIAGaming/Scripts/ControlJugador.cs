using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    [Header("Movement Settings")]
    public float walkSpeed = 5.0f;   // Velocidad al caminar
    public float runSpeed = 8.0f;    // Velocidad al correr
    public float rotationSpeed = 200.0f;

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Obtiene referencias a los componentes necesarios
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Determina la dirección del movimiento
        Vector3 moveDirection = (transform.forward * vertical + transform.right * horizontal).normalized;

        // Verifica si el personaje está corriendo (Shift presionado)
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && moveDirection.magnitude > 0;
        bool isWalking = !isRunning && moveDirection.magnitude > 0;

        // Ajustar la velocidad según el estado
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Mueve al personaje con Rigidbody
        Vector3 targetPosition = rb.position + moveDirection * currentSpeed * Time.deltaTime;
        targetPosition.y = rb.position.y; // Evita cambios de altura inesperados
        rb.MovePosition(targetPosition);

        // Control de animaciones
        if (animator != null)
        {
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isWalking", isWalking);
        }
    }

    private void HandleShooting()
    {
        // Detectar si el botón izquierdo del ratón está presionado
        bool isShooting = Input.GetMouseButton(0); // Mouse0 = Clic izquierdo

        // Activar animación de disparo
        if (animator != null)
        {
            animator.SetBool("isShooting", isShooting);
        }
    }
}
