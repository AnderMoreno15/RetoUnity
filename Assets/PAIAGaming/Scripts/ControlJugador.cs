using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator; // Referencia al componente Animator
    private Rigidbody rb;      // Referencia al componente Rigidbody

    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;        // Velocidad de movimiento
    public float rotationSpeed = 200.0f; // Velocidad de rotación

    [Header("Jump Settings")]
    public float jumpForce = 5.0f;       // Fuerza del salto
    private bool isGrounded = true;      // Verifica si el jugador está en el suelo

    [Header("Look Settings")]
    public float lookSpeedX = 2.0f; // Velocidad de rotación en el eje X
    public float lookSpeedY = 2.0f; // Velocidad de rotación en el eje Y
    private float rotationX = 0f;    // Rotación en el eje X
    private float rotationY = 0f;    // Rotación en el eje Y

    void Start()
    {
        // Ocultar y bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Obtener referencias a componentes necesarios
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
    }

    private void HandleMovement()
    {
        // Obtener entradas de movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Crear vector de movimiento en el espacio local
        Vector3 move = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (move.magnitude > 0) // Si el jugador se está moviendo
        {
            // Aplicar movimiento
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.Self);

            // Actualizar animación
            animator.SetBool("isRunning", true);
        }
        else
        {
            // Detener animación si no se mueve
            animator.SetBool("isRunning", false);
        }
    }

    private void HandleRotation()
    {
        // Obtener las entradas del mouse para rotación
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Ajustar la rotación en el eje Y (y rotar el jugador)
        rotationY += mouseX * lookSpeedX;

        // Limitar la rotación en el eje X (mirar arriba y abajo)
        rotationX -= mouseY * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f); // Limitar para no volverse demasiado loco

        // Aplicar la rotación
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }

    private void HandleJump()
    {
        // Salto si el jugador está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump"); // Disparar animación de salto
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Está en el aire
        }
    }

    // Verifica si el jugador está tocando el suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
