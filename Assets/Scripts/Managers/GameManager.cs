using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Puntos y Combo")]
    public int score = 0;
    public int combo = 1;
    private int orbsCollected = 0;
    private int orbsForCombo = 5; // cada 5 orbes sube el combo

    [Header("Energía")]
    public float energy = 0f;
    public float maxEnergy = 100f;
    public float energyPerOrb = 10f;
    public float energyLostOnHit = 20f;

    [Header("Estado del juego")]
    public bool isGameOver = false;
    public bool isPaused = false;

    [Header("Fase del escenario")]
    public int currentPhase = 1; // 1, 2 o 3 según puntos
    public int scoreForPhase2 = 200;
    public int scoreForPhase3 = 500;

    [Header("Timer")]
    public float gameDuration = 90f; // 90 segundos = 1:30 minutos
    private float timeRemaining;

    void Start()
    {
        timeRemaining = gameDuration;
    }

    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (isGameOver || isPaused) return;

        // Countdown
        timeRemaining -= Time.deltaTime;
        UIManager.Instance.UpdateTimer(timeRemaining);

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            GameOver();
        }

        CheckPhase();
    }

    // --- ORBES ---
    public void OrbCollected()
    {
        orbsCollected++;
        score += 10 * combo;
        energy = Mathf.Min(energy + energyPerOrb, maxEnergy);

        // Subir combo cada X orbes
        if (orbsCollected % orbsForCombo == 0)
            combo++;

        UIManager.Instance.UpdateUI();

        // Si la energía se llena, fase especial
        if (energy >= maxEnergy)
            TriggerCalmPhase();
    }

    // --- OBSTÁCULOS ---
    public void PlayerHit()
    {
        energy = Mathf.Max(energy - energyLostOnHit, 0f);
        combo = 1; // se rompe el combo
        UIManager.Instance.UpdateUI();

        if (energy <= 0f)
            GameOver();
    }

    // --- FASES ---
    void CheckPhase()
    {
        if (score >= scoreForPhase3 && currentPhase < 3)
        {
            currentPhase = 3;
            AudioManager.Instance.ChangePhase(3);
        }
        else if (score >= scoreForPhase2 && currentPhase < 2)
        {
            currentPhase = 2;
            AudioManager.Instance.ChangePhase(2);
        }
    }

    void TriggerCalmPhase()
    {
        // Aquí puedes activar partículas especiales, mensaje positivo, etc.
        Debug.Log("¡Has encontrado tu calma!");
        UIManager.Instance.ShowCalmMessage();
        energy = 0f; // reinicia la barra
    }

    // --- PAUSA ---
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        UIManager.Instance.ShowPausePanel(isPaused);
    }

    // --- GAME OVER ---
    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        // Determinar si fue bueno o malo
        bool didWell = score >= 300; // ajusta este umbral
        UIManager.Instance.ShowGameOver(score, didWell);
    }

    // --- REINICIAR ---
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}