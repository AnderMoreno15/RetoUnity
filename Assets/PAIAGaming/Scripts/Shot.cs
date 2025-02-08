using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public float shootForce = 1500f;
    public float shootRate = 0.2f;

    private float shootRateTime = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shootRateTime)
            {
                GameObject newBullet; // Crear bala nueva
                newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation); // Instanciar la bala
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shootForce); // Aplicar fuerza a la bala
                shootRateTime = Time.time + shootRate; // Actualizar el tiempo de disparo
                Destroy(newBullet, 2f); // Destruir la bala en 2 segundos
            }
        }
    }
}
