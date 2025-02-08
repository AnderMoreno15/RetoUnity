using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OncollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
        }
    }
}
