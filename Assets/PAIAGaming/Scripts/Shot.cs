using UnityEngine;

public class Shot : MonoBehaviour
{
    // Punto de spawn de la bala
    public Transform spawnPoint;

    // Prefab de la bala
    public GameObject bullet;

    // Fuerza de disparo
    public float shootForce = 1500f;

    // Intervalo entre disparos
    public float shootRate = 0.2f;

    // Contador de tiempo para controlar la cadencia de disparo
    private float shootRateTime = 0f;

    // Componente de audio para el sonido de disparo
    private AudioSource shotAudio;

    void Start()
    {
        // Obtener el componente de audio
        shotAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Cambio clave: usar GetButton en lugar de GetButtonDown
        // Esto hace que se dispare mientras se mantiene presionado
        if (Input.GetButton("Fire1"))
        {
            // Verificar si ha pasado suficiente tiempo desde el último disparo
            if (Time.time > shootRateTime)
            {
                // Reproducir sonido de disparo
                shotAudio.Play();

                // Instanciar la bala en la posición del punto de spawn
                GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

                // Aplicar fuerza a la bala
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shootForce);

                // Actualizar el tiempo para el próximo disparo
                shootRateTime = Time.time + shootRate;

                // Destruir la bala después de 2 segundos
                Destroy(newBullet, 2f);
            }
        }
    }
}