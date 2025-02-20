using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int cont = 0; // Contador de objetivos golpeados
    public float elapsedTime = 0f;
    public bool gameEnded = false;

    // Referencias a los canvas
    public GameObject endScreenCanvas;
    public GameObject startScreenCanvas;
    public GameObject reticulaCanvas;
    
    // Referencias a textos UI
    public TMP_Text tiempoText;
    public TMP_Text contadorText;
    public UnityEngine.UI.Button restartButton;

    // Referencias al jugador
    public Transform player;
    private Vector3 initialPlayerPosition;

    // Referencias a los controladores
    public CameraController cameraController;
    public Shot shotController;
    public PlayerController playerController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Guardar la posición inicial del personaje
        if (player != null)
        {
            initialPlayerPosition = player.position;
        }

        // Inicializamos los canvas
        if (endScreenCanvas != null)
        {
            endScreenCanvas.SetActive(false);
        }
        if (reticulaCanvas != null)
        {
            reticulaCanvas.SetActive(false);
        }
        if (startScreenCanvas != null)
        {
            startScreenCanvas.SetActive(true);
        }

        // Inicializar el texto del contador
        if (contadorText != null)
        {
            contadorText.text = "0/8";
        }

        // Desactivar los scripts y pausar el tiempo al inicio
        cameraController.enabled = false;
        shotController.enabled = false;
        playerController.enabled = false;
        Time.timeScale = 0f;

        // Hacer visible el cursor al inicio
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (!gameEnded)
        {
            elapsedTime += Time.deltaTime;
        }

        if (cont >= 8 && !gameEnded)
        {
            gameEnded = true;
            EndGame();
        }
    }

    public void StartGame()
    {
        // Activar los scripts
        cameraController.enabled = true;
        shotController.enabled = true;
        playerController.enabled = true;

        // Iniciar el tiempo
        Time.timeScale = 1f;

        // Ocultar el cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Desactivar el canvas de inicio
        if (startScreenCanvas != null)
        {
            startScreenCanvas.SetActive(false);
        }
        if (reticulaCanvas != null)
        {
            reticulaCanvas.SetActive(true);
        }
    }

    public void EndGame()
    {
        gameEnded = true;

        if (endScreenCanvas != null)
        {
            endScreenCanvas.SetActive(true);

            if (tiempoText != null)
            {
                int minutos = Mathf.FloorToInt(elapsedTime / 60);
                int segundos = Mathf.FloorToInt(elapsedTime % 60);
                tiempoText.text = $"Tiempo: {minutos:00}:{segundos:00}\nREINICIAR";
                tiempoText.fontSize = 36;
            }
        }

        // Hacer visible el cursor del ratón
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f; // Pausar el juego
    }

    public void RestartGame()
    {
        // Reanudar el tiempo antes de reiniciar la escena
        Time.timeScale = 1f;
        
        // Recargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetPlayerPosition()
    {
        if (player != null)
        {
            player.position = initialPlayerPosition;
        }
    }

    public void TargetGolpeado()
    {
        cont++;

        // Actualizar el texto del contador
        if (contadorText != null)
        {
            contadorText.text = $"{cont}/8";
        }
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego..."); // Útil para probar en el editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}