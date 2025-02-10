using UnityEngine;

public class Bullet : MonoBehaviour
{
    private AudioSource shotAudioClip; // Referencia al audio

    void Start()
    {
        // Obtener el AudioSource del objeto
        shotAudioClip = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) // Corregida la mayúscula en "OnCollisionEnter"
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            shotAudioClip.Play();
            Destroy(collision.gameObject); // Destruir el objeto con el tag "Target"
        }
    }
}
