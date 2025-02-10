using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public float shootForce = 1500f;
    public float shootRate = 0.2f;

    private float shootRateTime = 0f;
    AudioSource shotAudioClip;

    void Start()
    {
        shotAudioClip = GetComponent<AudioSource>(); // Asegúrate de asignar el componente AudioSource
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shotAudioClip.Play(); // Corregido el error aquí
            if (Time.time > shootRateTime)
            {
                GameObject newBullet; // Crear bala nueva
                newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation); // Instanciar la bala
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shootForce); // Aplicar spawnpoint y fuerza a la bala
                shootRateTime = Time.time + shootRate; // Actualizar el tiempo de disparo
                Destroy(newBullet, 2f); // Destruir la bala en 2 segundos
            }
        }
    }
}
