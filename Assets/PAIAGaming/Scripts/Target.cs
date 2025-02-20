using UnityEngine;

public class Target : MonoBehaviour
{
    // Referencias a los puntos de movimiento
    public Transform puntoA;  // Primer punto de movimiento
    public Transform puntoB;  // Segundo punto de movimiento
    public float velocidad = 5f;  // Velocidad de movimiento

    // Punto de destino actual
    private Transform puntoDestino;
    
    // Bandera para saber si ya ha sido golpeado
    public bool hit = false;

    // Componente de audio para sonido de impacto
    private AudioSource hitAudio;

    // Inicialización
    void Start()
    {
        // Establecer punto de destino inicial
        puntoDestino = puntoB;

        // Obtener componente de audio (opcional)
        hitAudio = GetComponent<AudioSource>();
    }

    // Actualización en cada frame
    void Update()
    {
        // Mover el objeto entre puntos
        transform.position = Vector3.MoveTowards(
            transform.position,
            puntoDestino.position,
            velocidad * Time.deltaTime
        );

        // Cambiar de punto cuando se alcanza el destino
        if (transform.position == puntoDestino.position)
        {
            puntoDestino = (puntoDestino == puntoB) ? puntoA : puntoB;
        }
    }

    // Método para manejar colisiones
    private void OnTriggerEnter(Collider other)
    {
        if (!hit && other.CompareTag("Bullet"))
        {
            hit = true; // Marcar como golpeado

            // Incrementar contador del GameManager
            GameManager.instance.TargetGolpeado();

            // Reproducir sonido de impacto (si existe)
            if (hitAudio != null)
            {
                hitAudio.Play();
            }

            // Desactivar el objeto o deshabilitar el Collider
            GetComponent<Collider>().enabled = false; // Deshabilita el Collider
            // O bien: gameObject.SetActive(false); // Desactiva el objeto
        }
    }
}