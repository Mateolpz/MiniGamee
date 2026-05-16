using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD principal")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public Slider energySlider;
    public TextMeshProUGUI endMessageText;

    [Header("Mensaje de calma")]
    public GameObject calmMessagePanel;
    public TextMeshProUGUI calmMessageText;

    [Header("Panel de pausa")]
    public GameObject pausePanel;

    [Header("Panel de Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    [Header("Timer")]
    public TextMeshProUGUI timerText;

    [Header("Mensajes positivos")]
    private string[] calmMessages = {
        "¡HAS ENCONTRADO TU CALMA!",
        "¡RESPIRA PROFUNDO, lO ESTAS LOGRANDO!",
        "¡MENTE CLARA, CORAZON TRANQUILO!",
        "¡ERES INCREIBLE, SIGUE ASI!",
        "¡LA CALMA ES TU SUPERPODER!"
    };

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Asegurarse que los paneles estén ocultos al inicio
        if (calmMessagePanel) calmMessagePanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);

        UpdateUI();
    }

    // --- ACTUALIZAR HUD ---
    public void UpdateUI()
    {
        if (scoreText)
            scoreText.text = "PUNTOS\n" + GameManager.Instance.score.ToString();

        if (comboText)
            comboText.text = "COMBO\nx" + GameManager.Instance.combo.ToString();

        if (energySlider)
        {
            energySlider.maxValue = GameManager.Instance.maxEnergy;
            energySlider.value = GameManager.Instance.energy;
        }
    }

    // --- MENSAJE DE CALMA ---
    public void ShowCalmMessage()
    {
        if (calmMessagePanel == null) return;

        // Mensaje aleatorio
        string msg = calmMessages[Random.Range(0, calmMessages.Length)];
        if (calmMessageText) calmMessageText.text = msg;

        calmMessagePanel.SetActive(true);
        Invoke(nameof(HideCalmMessage), 3f); // se oculta solo después de 3 segundos
    }

    void HideCalmMessage()
    {
        if (calmMessagePanel) calmMessagePanel.SetActive(false);
    }

    // --- PAUSA ---
    public void ShowPausePanel(bool show)
    {
        if (pausePanel) pausePanel.SetActive(show);
    }

    public void OnPauseButtonPressed()
    {
        GameManager.Instance.TogglePause();
    }

    // --- GAME OVER ---
    public void ShowGameOver(int finalScore, bool didWell)
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);

        if (finalScoreText)
            finalScoreText.text = "PUNTUACIÓN FINAL\n" + finalScore.ToString();

        // Mensaje según rendimiento
        if (endMessageText)
        {
            endMessageText.text = didWell
                ? "¡MUY BIEN! HAS ENCONTRADO TU CALMA."
                : "¡BUEN INTENTO! RESPIRA Y VUELVE A INTENTARLO.";
        }
    }

    // --- BOTONES ---
    public void OnRestartPressed()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnMenuPressed()
    {
        GameManager.Instance.GoToMenu();
    }

    public void UpdateTimer(float timeRemaining)
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        // Se pone rojo cuando quedan menos de 10 segundos
        timerText.color = timeRemaining <= 10f ? Color.red : Color.white;
    }
}