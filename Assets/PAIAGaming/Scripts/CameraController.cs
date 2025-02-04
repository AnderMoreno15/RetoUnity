using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform cameraPivot;
    public Transform cameraTransform; // Referencia a la cámara real
    public float lookSpeedX = 2.0f;
    public float lookSpeedY = 2.0f;
    private float rotationX = 0f;

    [Header("Zoom Settings")]
    public Vector3 normalOffset = new Vector3(0, 1.5f, -4f); // Posición normal de la cámara
    public Vector3 aimOffset = new Vector3(0.5f, 1.7f, -2f); // Posición al apuntar
    public float zoomSpeed = 5f; // Velocidad de transición del zoom

    void Update()
    {
        HandleCameraRotation();
        HandleCameraZoom();
    }

    private void HandleCameraRotation()
    {
        // Obtener las entradas del ratón
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Rotación del personaje (solo en Y - izquierda/derecha)
        player.Rotate(Vector3.up * mouseX);

        // Rotación de la cámara (solo en X - arriba/abajo)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -30f, 20f); // Limitar la inclinación de la cámara

        // Aplicar la rotación al Camera Pivot
        cameraPivot.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    private void HandleCameraZoom()
    {
        // Determinar si el botón derecho del ratón está presionado
        Vector3 targetOffset = Input.GetMouseButton(1) ? aimOffset : normalOffset;

        // Interpolar la posición de la cámara suavemente
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetOffset, Time.deltaTime * zoomSpeed);
    }
}
