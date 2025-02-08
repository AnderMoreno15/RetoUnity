using UnityEngine;

public class targets : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 5f;

    private Transform puntoDestino;
    public bool hit = false;

    void Start()
    {
        puntoDestino = puntoB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, puntoDestino.position, velocidad * Time.deltaTime);

        if (transform.position == puntoDestino.position)
        {
            puntoDestino = puntoDestino == puntoB ? puntoA : puntoB;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Esta condición es crucial: verifica si el GameManager ya ha registrado este target como golpeado.
        // Esto evita que se incremente el contador múltiples veces si el target tiene varios colliders o triggers.
        if (!GameManager.instance.targetsGolpeados.Contains(this.gameObject))
        {
            if (other.CompareTag("Bullet"))
            {
                GameManager.instance.cont++;
                GameManager.instance.targetsGolpeados.Add(this.gameObject); // Registra este target como golpeado
                hit = true;
            }
        }
    }
}

