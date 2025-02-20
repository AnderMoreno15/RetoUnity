using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Referencia al transform del personaje
    public Vector3 offset; // Desplazamiento respecto al personaje

    private void Update()
    {
        if (player != null)
        {
            // Sigue al personaje con un desplazamiento
            transform.position = player.position + offset;
        }
    }
}