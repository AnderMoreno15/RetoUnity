using UnityEngine;
using System.Collections.Generic; // Importa el espacio de nombres para HashSet



public class MoverEntrePuntos : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 5f;

    private Transform puntoDestino;
    public bool hit = false;
  
    void Start()
    {
        // Inicialmente, el objeto se mueve hacia el punto B
        puntoDestino = puntoB;
    }

    void Update()
    {
        // Mueve el objeto hacia el punto destino
        transform.position = Vector3.MoveTowards(transform.position, puntoDestino.position, velocidad * Time.deltaTime);

        // Si el objeto llega al punto destino, cambia al punto opuesto
        if (transform.position == puntoDestino.position)
        {
            puntoDestino = puntoDestino == puntoB ? puntoA : puntoB;
        }
    }
        private void OnTriggerEnter(Collider other)
    {
        if (!hit && other.CompareTag("Bullet")) // Usa el tag "Bullet" y verifica si ya fue golpeado
        {
            hit = true; // Marca el target como golpeado
            GameManager.instance.cont++; // Incrementa el contador en el GameManager
        }
    }
}
// Script del GameManager (GameManager.cs)
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int cont = 0;
    public HashSet<GameObject> targetsGolpeados = new HashSet<GameObject>(); // Lista de targets golpeados

    private void Awake()
    {
        instance = this;
    }
}